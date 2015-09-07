using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PrekenWeb.Data.Repositories;
using PrekenWeb.Data.Tables;
using PrekenWeb.Data.ViewModels;

namespace PrekenWeb.Data.Services
{
    public class ZoekService
    {
        private readonly IZoekenRepository _zoekenRepository;
        private readonly IGebruikerRepository _gebruikerRepository;

        public ZoekService(IZoekenRepository zoekenRepository, IGebruikerRepository gebruikerRepository)
        {
            _zoekenRepository = zoekenRepository;
            _gebruikerRepository = gebruikerRepository;
        }

        /// <summary>
        /// Voert een zoekopdracht uit en retourneert een instantie van Zoekresultaat
        /// </summary>
        /// <param name="zoekOpdracht"></param>
        /// <param name="take"></param>
        /// <param name="tekstVeldenVullen">Zorgt er voor dat alle tekstvelden op de Zoekopdracht worden gevuld met de tekstwaarden die behoren aan de opgegeven ID's, als bijvoorbeeld predikantId wordt gevuld wordt ook het string veld Predikant gevuld met die tekstwaarde</param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public async Task<Zoekresultaat> ZoekOpdrachtUitvoeren(ZoekOpdracht zoekOpdracht, int skip = 0, int take = int.MaxValue, bool tekstVeldenVullen = false)
        {
            if (tekstVeldenVullen)
                zoekOpdracht = await TekstVeldenVullen(zoekOpdracht);

            var query = _zoekenRepository.GetPrekenQueryable(zoekOpdracht.TaalId, zoekOpdracht.PreekTypeIds);

            query = whereToepassen(zoekOpdracht, query);
            query = zoektermToepassen(zoekOpdracht, query);
            query = sorteringToepassen(zoekOpdracht, query);

            var zoekResultaat = new Zoekresultaat
            {
                ZoekOpdracht = zoekOpdracht,
                AantalResultaten = await query.CountAsync()
            };

            var preken = await query
                .Skip(skip)
                .Take(take)
                .ToListAsync();
            
            zoekResultaat.Items = preken.
                Select(p => new ZoekresultaatItem
                {
                    Preek = p,
                    ResultaatReden = ResultaatReden.Predikant
                });

            return zoekResultaat;
        }

        private IQueryable<Preek> sorteringToepassen(ZoekOpdracht zoekOpdracht, IQueryable<Preek> query)
        {
            switch (zoekOpdracht.SorteerOp)
            {
                case SorteerOp.Predikant:
                    query = (zoekOpdracht.SorteerVolgorde == SorteerVolgorde.DESC) ? query.OrderByDescending(q => q.Predikant.Achternaam) : query.OrderBy(q => q.Predikant.Achternaam);
                    break;

                case SorteerOp.Boek:
                    query = (zoekOpdracht.SorteerVolgorde == SorteerVolgorde.DESC)
                        ? query.OrderByDescending(q => q.BoekHoofdstuk.Boek.Sortering).ThenByDescending(q => q.BoekHoofdstuk.Sortering).ThenBy(q => q.Hoofdstuk)
                        : query.OrderBy(q => q.BoekHoofdstuk.Boek.Sortering).ThenBy(q => q.BoekHoofdstuk.Sortering).ThenByDescending(q => q.Hoofdstuk);
                    break;

                case SorteerOp.Hoofdstuk:
                    query = (zoekOpdracht.SorteerVolgorde == SorteerVolgorde.DESC) ? query.OrderByDescending(q => q.Hoofdstuk) : query.OrderBy(q => q.Hoofdstuk);
                    break;
                //case SorteerOp.Vers:
                //    query = (zoekOpdracht.SorteerVolgorde == SorteerVolgorde.DESC) ? query.OrderByDescending(q => q.Vers) : query.OrderBy(q => q.Vers);
                //    break;
                case SorteerOp.Gebeurtenis:
                    query = (zoekOpdracht.SorteerVolgorde == SorteerVolgorde.DESC) ? query.OrderByDescending(q => q.Gebeurtenis.Omschrijving) : query.OrderBy(q => q.Gebeurtenis.Omschrijving);
                    break;

                case SorteerOp.Gemeente:
                    query = (zoekOpdracht.SorteerVolgorde == SorteerVolgorde.DESC) ? query.OrderByDescending(q => q.Gemeente.Omschrijving) : query.OrderBy(q => q.Gemeente.Omschrijving);
                    break;

                case SorteerOp.Serie:
                    query = (zoekOpdracht.SorteerVolgorde == SorteerVolgorde.DESC) ? query.OrderByDescending(q => q.Serie.Omschrijving) : query.OrderBy(q => q.Serie.Omschrijving);
                    break;

                case SorteerOp.LezingCategorie:
                    query = (zoekOpdracht.SorteerVolgorde == SorteerVolgorde.DESC) ? query.OrderByDescending(q => q.LezingCategorie.Omschrijving) : query.OrderBy(q => q.LezingCategorie.Omschrijving);
                    break;

                case SorteerOp.Datum:
                    query = (zoekOpdracht.SorteerVolgorde == SorteerVolgorde.DESC) ? query.OrderBy(q => q.DatumAangemaakt) : query.OrderByDescending(q => q.DatumAangemaakt);
                    break;

                default:
                    if (zoekOpdracht.BoekHoofdstukId.HasValue)
                    {
                        zoekOpdracht.SorteerOp = SorteerOp.Hoofdstuk;
                        zoekOpdracht.SorteerVolgorde = SorteerVolgorde.DESC;
                        query = query.OrderBy(zr => zr.Hoofdstuk);
                    }
                    else
                    {
                        zoekOpdracht.SorteerOp = SorteerOp.Datum;
                        zoekOpdracht.SorteerVolgorde = (zoekOpdracht.SorteerVolgorde == SorteerVolgorde.DESC) ? SorteerVolgorde.DESC : SorteerVolgorde.ASC;
                        query = (zoekOpdracht.SorteerVolgorde == SorteerVolgorde.DESC) ? query.OrderBy(q => q.Id) : query.OrderByDescending(q => q.DatumAangemaakt);
                    }
                    break;
            }
            return query;
        }

        private IQueryable<Preek> zoektermToepassen(ZoekOpdracht zoekOpdracht, IQueryable<Preek> query)
        {
            if (!string.IsNullOrEmpty(zoekOpdracht.Zoekterm)) query = query.Where(p =>
                        p.ThemaOmschrijving.Contains(zoekOpdracht.Zoekterm)
                        || p.Punt1.Contains(zoekOpdracht.Zoekterm)
                        || p.Punt2.Contains(zoekOpdracht.Zoekterm)
                        || p.Punt3.Contains(zoekOpdracht.Zoekterm)
                        || p.Punt4.Contains(zoekOpdracht.Zoekterm)
                        || p.Punt5.Contains(zoekOpdracht.Zoekterm)
                        || p.BijbeltekstOmschrijving.Contains(zoekOpdracht.Zoekterm)
                        || p.Predikant.Achternaam.Contains(zoekOpdracht.Zoekterm)
                        || p.BoekHoofdstuk.Omschrijving.Contains(zoekOpdracht.Zoekterm)
                        || p.BoekHoofdstuk.Boek.Boeknaam.Contains(zoekOpdracht.Zoekterm)
                        || p.Gebeurtenis.Omschrijving.Contains(zoekOpdracht.Zoekterm)
                        || p.Serie.Omschrijving.Contains(zoekOpdracht.Zoekterm)
                        || p.LezingOmschrijving.Contains(zoekOpdracht.Zoekterm)
                        || p.Informatie.Contains(zoekOpdracht.Zoekterm)
                        );
            return query;
        }

        private IQueryable<Preek> whereToepassen(ZoekOpdracht zoekOpdracht, IQueryable<Preek> query)
        {
            if (!string.IsNullOrEmpty(zoekOpdracht.Predikant)) query = query.Where(p => (((p.Predikant.Titels ?? "") + " " + (p.Predikant.Voorletters ?? "")).Trim() + " " + ((p.Predikant.Tussenvoegsels ?? "") + " " + (p.Predikant.Achternaam ?? "")).Trim()).Contains(zoekOpdracht.Predikant));
            if (!string.IsNullOrEmpty(zoekOpdracht.BoekHoofdstuk)) query = query.Where(p => p.BoekHoofdstuk.Omschrijving.Contains(zoekOpdracht.BoekHoofdstuk));
            if (!string.IsNullOrEmpty(zoekOpdracht.Boek)) query = query.Where(p => p.BoekHoofdstuk.Boek.Boeknaam.Contains(zoekOpdracht.Boek));
            if (!string.IsNullOrEmpty(zoekOpdracht.Gebeurtenis)) query = query.Where(p => p.Gebeurtenis.Omschrijving.Contains(zoekOpdracht.Gebeurtenis));
            if (!string.IsNullOrEmpty(zoekOpdracht.Gemeente)) query = query.Where(p => p.Gemeente.Omschrijving.Contains(zoekOpdracht.Gemeente));
            if (!string.IsNullOrEmpty(zoekOpdracht.LezingCategorie)) query = query.Where(p => p.LezingCategorie.Omschrijving.Contains(zoekOpdracht.LezingCategorie));
            if (!string.IsNullOrEmpty(zoekOpdracht.Serie)) query = query.Where(p => p.Serie.Omschrijving.Contains(zoekOpdracht.Serie));
            if (zoekOpdracht.Hoofdstuk.HasValue) query = query.Where(p => zoekOpdracht.Hoofdstuk.HasValue && p.Hoofdstuk == zoekOpdracht.Hoofdstuk);
            return query;
        }

        private async Task<ZoekOpdracht> TekstVeldenVullen(ZoekOpdracht zoekOpdracht)
        {
            if (zoekOpdracht.PredikantId.HasValue)
            {
                var predikant = await _zoekenRepository.GetPredikantById(zoekOpdracht.PredikantId.Value, zoekOpdracht.TaalId);
                if (predikant != null) zoekOpdracht.Predikant = predikant.VolledigeNaam;
            }
            if (zoekOpdracht.GebeurtenisId.HasValue)
            {
                var gebeurtenis = await _zoekenRepository.GetGebeurtenisById(zoekOpdracht.GebeurtenisId.Value,zoekOpdracht.TaalId);
                if (gebeurtenis != null) zoekOpdracht.Gebeurtenis = gebeurtenis.Omschrijving;

            }
            if (zoekOpdracht.BoekId.HasValue)
            {
                zoekOpdracht.Boek = (await _zoekenRepository.GetBoekById(zoekOpdracht.BoekId.Value,zoekOpdracht.TaalId)).Boeknaam;
            }
            if (zoekOpdracht.BoekHoofdstukId.HasValue)
            {
                var hoofdstuk = await _zoekenRepository.GetBoekhoofdstukById(zoekOpdracht.BoekHoofdstukId.Value);
                zoekOpdracht.BoekHoofdstuk = hoofdstuk.Omschrijving;
            }
            if (zoekOpdracht.SerieId.HasValue)
            {
                var serie = await _zoekenRepository.GetSerieById(zoekOpdracht.SerieId.Value,zoekOpdracht.TaalId);
                if (serie != null) zoekOpdracht.Serie = serie.Omschrijving;

            }
            if (zoekOpdracht.GemeenteId.HasValue)
            {
                zoekOpdracht.Gemeente = (await _zoekenRepository.GetGemeenteById(zoekOpdracht.GemeenteId.Value)).Omschrijving;
            }
            if (zoekOpdracht.LezingCategorieId.HasValue)
            {
                zoekOpdracht.LezingCategorie = (await _zoekenRepository.GetLezingCategorieId(zoekOpdracht.LezingCategorieId.Value)).Omschrijving;
            }
            return zoekOpdracht;
        }
    }
}