using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using PrekenWeb.Data;
using PrekenWeb.Data.Tables;
using PrekenWeb.Data.ViewModels;

namespace Prekenweb.Website.Lib
{
    public static class ComboboxDataSources
    { 
        public static IEnumerable<SelectListItem> GetGemeenteValues(  int? selectedGemeenteId, int? taalId)
        {
            var returnValues = new List<SelectListItem>();
            returnValues.Add(new SelectListItem()
            {
                Selected = !selectedGemeenteId.HasValue,
                Text = "",
                Value = ""
            });

            using (var context = new PrekenwebContext())
            {
                returnValues.AddRange(context.Gemeentes.OrderBy(g => g.Omschrijving).ToList().Select(g =>
                    new SelectListItem
                    {
                        Text = g.Omschrijving,
                        Value = g.Id.ToString(CultureInfo.InvariantCulture),
                        Selected = selectedGemeenteId.HasValue && selectedGemeenteId.Value == g.Id
                    }));
            }

            return returnValues;
        }

        public static IEnumerable<SelectListItem> GetSerieValues(int? selectedSerieId, int? taalId)
        {
            var returnValues = new List<SelectListItem>();
            returnValues.Add(new SelectListItem()
            {
                Selected = !selectedSerieId.HasValue,
                Text = "",
                Value = ""
            });

            using (var context = new PrekenwebContext())
            {
                returnValues.AddRange(context.Series.Where(s => s.TaalId == taalId || !taalId.HasValue).OrderBy(s => s.Omschrijving).ToList().Select(s =>
                    new SelectListItem
                    {
                        Text = s.Omschrijving,
                        Value = s.Id.ToString(CultureInfo.InvariantCulture),
                        Selected = selectedSerieId.HasValue && selectedSerieId.Value == s.Id
                    }));
            }

            return returnValues;
        }

        public static IEnumerable<SelectListItem> GetLezingCategorieValues(int? selectedLezingCategorieId)
        {
            var returnValues = new List<SelectListItem>();
            returnValues.Add(new SelectListItem()
            {
                Selected = !selectedLezingCategorieId.HasValue,
                Text = "",
                Value = ""
            });

            using (var context = new PrekenwebContext())
            {
                returnValues.AddRange(context.LezingCategories.OrderBy(lc => lc.Omschrijving).ToList().Select(lc =>
                    new SelectListItem
                    {
                        Text = lc.Omschrijving,
                        Value = lc.Id.ToString(),
                        Selected = selectedLezingCategorieId.HasValue && selectedLezingCategorieId.Value == lc.Id
                    }));
            }

            return returnValues;
        }

        public static IEnumerable<SelectListItem> GetAllMailings(ICollection<Mailing> selectedMailings, int taalId)
        {
            var returnValues = new List<SelectListItem>();
            returnValues.Add(new SelectListItem()
            {
                Selected = false,
                Text = "",
                Value = ""
            });

            using (var context = new PrekenwebContext())
            {
                returnValues.AddRange(context.Mailings/*.Where(x => x.TaalId == taalId)*/.ToList().OrderBy(p => p.Omschrijving).Select(r =>
                    new SelectListItem
                    {
                        Text = r.Omschrijving,
                        Value = r.Id.ToString(),
                        Selected = selectedMailings.Any(m => m.Id == r.Id)
                    }));
            }

            return returnValues;
        }

        public static IEnumerable<SelectListItem> GetPreektypeValues(int? selectedPreekTypeId)
        {
            var returnValues = new List<SelectListItem>();
            returnValues.Add(new SelectListItem()
            {
                Selected = !selectedPreekTypeId.HasValue,
                Text = "",
                Value = ""
            });

            using (var context = new PrekenwebContext())
            {
                returnValues.AddRange(context.PreekTypes.ToList().Select(p =>
                    new SelectListItem
                    {
                        Text = GetLocale(p.Id),
                        Value = p.Id.ToString(),
                        Selected = selectedPreekTypeId.HasValue && selectedPreekTypeId.Value == p.Id
                    }));
            }

            return returnValues;
        }

        private static string GetLocale(int preektypeid)
        {
            switch (preektypeid)
            {
                default:
                    //case (int)PreekTypeEnum.Peek:
                    return Resources.Resources.AudioPreek;
                case (int)PreekTypeEnum.Lezing:
                    return Resources.Resources.Lezing;
                case (int)PreekTypeEnum.LeesPreek:
                    return Resources.Resources.ReadingSermon;
            }
        }


        public static IEnumerable<SelectListItem> GetTaalValues(int? selectedTaalId)
        {
            var returnValues = new List<SelectListItem>();
            returnValues.Add(new SelectListItem()
            {
                Selected = !selectedTaalId.HasValue,
                Text = "",
                Value = ""
            });

            using (var context = new PrekenwebContext())
            {
                returnValues.AddRange(context.Taals.ToList().Select(t =>
                    new SelectListItem
                    {
                        Text = t.Omschrijving,
                        Value = t.Id.ToString(),
                        Selected = selectedTaalId.HasValue && selectedTaalId.Value == t.Id
                    }));
            }

            return returnValues;
        }

        public static IEnumerable<SelectListItem> GetGebeurtenisValues(int? selectedGebeurtenisId, int? taalId)
        {
            var returnValues = new List<SelectListItem>();
            returnValues.Add(new SelectListItem()
            {
                Selected = !selectedGebeurtenisId.HasValue,
                Text = "",
                Value = ""
            });

            using (var context = new PrekenwebContext())
            {
                returnValues.AddRange(context.Gebeurtenis.Where(g => g.TaalId == taalId || !taalId.HasValue).OrderBy(g => g.Omschrijving).ToList().Select(g =>
                    new SelectListItem
                    {
                        Text = g.Omschrijving,
                        Value = g.Id.ToString(CultureInfo.InvariantCulture),
                        Selected = selectedGebeurtenisId.HasValue && selectedGebeurtenisId.Value == g.Id
                    }));
            }

            return returnValues;
        }

        public static IEnumerable<SelectListItem> GetBoekhoofdstukValues(int? selectedBoekHoodstukId, int? taalId)
        {
            var returnValues = new List<SelectListItem>();
            returnValues.Add(new SelectListItem()
            {
                Selected = !selectedBoekHoodstukId.HasValue,
                Text = "",
                Value = ""
            });

            using (var context = new PrekenwebContext())
            {
                returnValues.AddRange(
                    context.BoekHoofdstuks
                    .Include(x => x.Boek)
                    .Include(x => x.Boek.Taal)
                    .Where(pt => pt.Boek.TaalId == taalId || !taalId.HasValue)
                    .OrderBy(bh => bh.Boek.Taal.Omschrijving)
                    .ThenByDescending(bh => bh.Boek.Sortering)
                    .ThenByDescending(bh => bh.Sortering)
                    .ToList()
                    .Select(p =>
                        new SelectListItem
                        {
                            Text = String.Format("{0}", p.Omschrijving),
                            Value = p.Id.ToString(CultureInfo.InvariantCulture),
                            Selected = selectedBoekHoodstukId.HasValue && selectedBoekHoodstukId.Value == p.Id
                        }
                    )
                );
            }

            return returnValues;
        }


        public static IEnumerable<SelectListItem> GetAfbeeldingValues(int? selectedAfbeeldingId)
        {
            var returnValues = new List<SelectListItem>();
            returnValues.Add(new SelectListItem()
            {
                Selected = !selectedAfbeeldingId.HasValue,
                Text = "",
                Value = ""
            });

            using (var context = new PrekenwebContext())
            {
                returnValues.AddRange(context.Afbeeldings.ToList().OrderByDescending(p => p.Id).Select(p =>
                    new SelectListItem
                    {
                        Text = p.Omschrijving,
                        Value = p.Id.ToString(CultureInfo.InvariantCulture),
                        Selected = selectedAfbeeldingId.HasValue && selectedAfbeeldingId.Value == p.Id
                    }));
            }

            return returnValues;
        }


        public static IEnumerable<SelectListItem> GetUserValues(int? selectedUserId)
        {
            List<SelectListItem> returnValues = new List<SelectListItem>();
            returnValues.Add(new SelectListItem()
            {
                Selected = selectedUserId.HasValue,
                Text = "",
                Value = ""
            });

            using (PrekenwebContext context = new PrekenwebContext())
            {
                returnValues.AddRange(context.Users.ToList().Select(t =>
                    new SelectListItem
                    {
                        Text = t.Naam,
                        Value = t.Id.ToString(),
                        Selected = selectedUserId == t.Id
                    }));
            }

            return returnValues;
        }


        public static IEnumerable<SelectListItem> GetPredikantValues(int? selectedPredikantId, int? taalId)
        {
            var returnValues = new List<SelectListItem>();
            returnValues.Add(new SelectListItem()
            {
                Selected = !selectedPredikantId.HasValue,
                Text = "",
                Value = ""
            });

            using (var context = new PrekenwebContext())
            {
                returnValues.AddRange(
                    context.Predikants
                        .Include(x => x.Taal)
                        .Where(p => p.TaalId == taalId || !taalId.HasValue)
                        .ToList()
                        .OrderBy(p => p.Taal.Omschrijving)
                        .ThenBy(p => p.Achternaam)
                        .Select(p =>
                            new SelectListItem
                            {
                                Text = String.Format("{0}", p.VolledigeNaam),
                                Value = p.Id.ToString(),
                                Selected = selectedPredikantId.HasValue && selectedPredikantId.Value == p.Id
                            }
                        ));
            }

            return returnValues;
        }
    }
}