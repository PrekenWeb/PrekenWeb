using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Data;
using Data.Identity;
using Data.Tables;

namespace Website.Tests.TestData
{
    [ExcludeFromCodeCoverage]
    public class TestDataProvider
    {
        public void Provision(IPrekenwebContext<Gebruiker> context)
        {
            var testgebruiker1 = context.Users.Add(TestGebruiker1);
            var testgebruiker2 = context.Users.Add(TestGebruiker2);

            var taalNederlands = context.Taals.Add(new Taal
            {
                Id = 1,
                Code = "nl",
                Omschrijving = "Nederlands"
            });
            var taalEngels = context.Taals.Add(new Taal
            {
                Id = 2,
                Code = "en",
                Omschrijving = "Engels"
            });

            var paginaIdentifiers = new[] { "boeken","contact", 
                "financiele-bijdrage","gegevens-aanvullen","gegevens-aanvullen-compleet","home-welkom","hulp","Inloggen","InschrijvenNieuwsbrief","iTunes-podcast",
                "links ","nieuwsbrief","preek-toevoegen","reactie-geven","Registreer","RegistreerSuccesvol","ResetWachtwoord","ResetWachtwoordBevestiging","verbetering-doorgeven",
                "WachtwoordVergeten","WachtwoordVergetenBevestiging","Wat-is-mijn-PrekenWeb","nieuwsbericht"};

            foreach (var paginaIdenifier in paginaIdentifiers)
            {
                context.Paginas.Add(new Pagina
                {
                    Aangemaakt = new DateTime(2015, 01, 01, 12, 0, 0),
                    Gebruiker_AangemaaktDoor = testgebruiker1,
                    Gepubliceerd = true,
                    Identifier = paginaIdenifier,
                    TonenOpHomepage = paginaIdenifier == "nieuwsbericht",
                    Bijgewerkt = new DateTime(2015, 01, 01, 12, 0, 0),
                    Gebruiker_BijgewerktDoor = testgebruiker1,
                    Teksts = new List<Tekst>
                    {
                        new Tekst {Kop = paginaIdenifier, Taal = taalNederlands, Tekst_ = paginaIdenifier},
                        new Tekst {Kop = paginaIdenifier, Taal = taalEngels, Tekst_ = paginaIdenifier}
                    }
                });
            }

            var preektypePreek = context.PreekTypes.Add(new PreekType { Id = 1, Omschrijving = "Preek" });
            var preektypeLezing = context.PreekTypes.Add(new PreekType { Id = 2, Omschrijving = "Lezing" });
            var preektypeLeesPreek = context.PreekTypes.Add(new PreekType { Id = 3, Omschrijving = "LeesPreek" });

            var boekOudeTestament = context.Boeks.Add(new Boek
            {
                Sortering = 0,
                Taal = taalNederlands,
                Boeknaam = "Oude Testament",
                ToonHoofdstukNummer = true,
                Afkorting = "OT"
            });

            var boekHoofdstukGenesis = new BoekHoofdstuk { Omschrijving = "Genesis", Sortering = 0 };
            var boekHoofdstukExodus = new BoekHoofdstuk { Omschrijving = "Exodus", Sortering = 1 };
            var boekHoofdstukLeviticus = new BoekHoofdstuk { Omschrijving = "Leviticus", Sortering = 2 };

            boekOudeTestament.BoekHoofdstuks.Add(boekHoofdstukGenesis);
            boekOudeTestament.BoekHoofdstuks.Add(boekHoofdstukExodus);
            boekOudeTestament.BoekHoofdstuks.Add(boekHoofdstukLeviticus);

            var genesis1V1 = new BoekHoofdstukTekst { Tekst = "In den beginne schiep God den hemel en de aarde.", Sortering = 0, BoekHoofdstuk = boekHoofdstukGenesis, Vers = 1 };
            var genesis1V2 = new BoekHoofdstukTekst { Tekst = "De aarde nu was woest en ledig, en duisternis was op den afgrond; en de Geest Gods zweefde op de wateren.", Sortering = 1, BoekHoofdstuk = boekHoofdstukGenesis, Vers = 2 };
            var genesis1V3 = new BoekHoofdstukTekst { Tekst = "En God zeide: Daar zij licht! en daar werd licht.", Sortering = 2, BoekHoofdstuk = boekHoofdstukGenesis, Vers = 3 };

            var leviticus1V1 = new BoekHoofdstukTekst { Tekst = "En de HEERE riep Mozes, en sprak tot hem uit de tent der samenkomst, zeggende:", Sortering = 0, BoekHoofdstuk = boekHoofdstukLeviticus, Vers = 1 };
            var leviticus1V2 = new BoekHoofdstukTekst { Tekst = "Spreek tot de kinderen Israels, en zeg tot hen: Als een mens uit u den HEERE een offerande zal offeren, gij zult uw offeranden offeren van het vee, van runderen en van schapen.", Sortering = 1, BoekHoofdstuk = boekHoofdstukLeviticus, Vers = 2 };
            var leviticus1V3 = new BoekHoofdstukTekst { Tekst = "Indien zijn offerande een brandoffer van runderen is, zo zal hij een volkomen mannetje offeren; aan de deur van de tent der samenkomst zal hij dat offeren, naar zijn welgevallen, voor het aangezicht des HEEREN.", Sortering = 2, BoekHoofdstuk = boekHoofdstukLeviticus, Vers = 3 };

            boekHoofdstukLeviticus.BoekHoofdstukTeksts.Add(leviticus1V1);
            boekHoofdstukLeviticus.BoekHoofdstukTeksts.Add(leviticus1V2);
            boekHoofdstukLeviticus.BoekHoofdstukTeksts.Add(leviticus1V3);

            boekHoofdstukGenesis.BoekHoofdstukTeksts.Add(genesis1V1);
            boekHoofdstukGenesis.BoekHoofdstukTeksts.Add(genesis1V2);
            boekHoofdstukGenesis.BoekHoofdstukTeksts.Add(genesis1V3);

            var gemeenteRotterdam = context.Gemeentes.Add(new Gemeente { Omschrijving = "Rotterdam" });
            var gemeenteAmsterdam = context.Gemeentes.Add(new Gemeente { Omschrijving = "Amsterdam" });

            var predikantJanssen = context.Predikants.Add(new Predikant { Taal = taalNederlands, Gemeente = "Rotterdam", Achternaam = "Janssen", Tussenvoegsels = "", Opmerking = "Opmerking", Voorletters = "J.", Titels = "Ds" });
            var predikantDeWit = context.Predikants.Add(new Predikant { Taal = taalNederlands, Gemeente = "Rotterdam", Achternaam = "Wit", Tussenvoegsels = "de", Opmerking = "Opmerking", Voorletters = "W. D.", Titels = "Ds" });

            var gebeurtenisPasen = context.Gebeurtenis.Add(new Gebeurtenis { Omschrijving = "Pasen", Sortering = 1, Taal = taalNederlands });

            var preek1 = context.Preeks.Add(new Preek
            {
                Bestandsnaam = "VisserRAM-Matth13v33(dl4)_21746.mp3",
                Gebruiker_AangemaaktDoor = testgebruiker1,
                BoekHoofdstuk = boekHoofdstukGenesis,
                BoekHoofdstukTekst_VersVanId = genesis1V1,
                BoekHoofdstukTekst_VersTotId = genesis1V3,
                Hoofdstuk = 1,
                Punt1 = "Punt1",
                Punt2 = "Punt2",
                Punt3 = "Punt3",
                Punt4 = "Punt4",
                Taal = taalNederlands,
                TaalId = taalNederlands.Id,
                PreekType = preektypePreek,
                PreekTypeId = preektypePreek.Id,
                Gemeente = gemeenteRotterdam,
                Informatie = "Informatie",
                Predikant = predikantJanssen,
                Gepubliceerd = true,
                VersOmschrijving = "1 tot 3",
                DatumAangemaakt = new DateTime(2015, 1, 1, 12, 0, 0),
                DatumBijgewerkt = new DateTime(2015, 1, 1, 13, 10, 0),
                Gebruiker_AangepastDoor = testgebruiker1,
                Duur = new TimeSpan(0, 1, 12, 22),
                AutomatischeTeksten = true,
                Gebeurtenis = gebeurtenisPasen
            });
            var preek2 = context.Preeks.Add(new Preek
            {
                Bestandsnaam = "JongsteCde-Leespreek-Openb12.4b-5_21745_2016-09-14_09-06-22.pdf",
                Gebruiker_AangemaaktDoor = testgebruiker2,
                BoekHoofdstuk = boekHoofdstukLeviticus,
                BoekHoofdstukTekst_VersVanId = leviticus1V1,
                BoekHoofdstukTekst_VersTotId = leviticus1V3,
                Hoofdstuk = 1,
                Punt1 = "Punt1",
                Punt2 = "Punt2",
                Punt3 = "Punt3",
                Punt4 = "Punt4",
                Taal = taalNederlands,
                TaalId = taalNederlands.Id,
                PreekType = preektypeLeesPreek,
                PreekTypeId = preektypeLeesPreek.Id,
                Gemeente = gemeenteAmsterdam,
                Informatie = "Informatie",
                Predikant = predikantDeWit,
                Gepubliceerd = true,
                VersOmschrijving = "1 tot 3",
                DatumAangemaakt = new DateTime(2015, 1, 2, 11, 0, 0),
                DatumBijgewerkt = new DateTime(2015, 1, 2, 16, 10, 0),
                Gebruiker_AangepastDoor = testgebruiker2,
                AutomatischeTeksten = true
            });

            var preek3 = context.Preeks.Add(new Preek
            {
                Bestandsnaam = "Brugge, A.A. - Ps 126 (En)_21747.pdf",
                Gebruiker_AangemaaktDoor = testgebruiker2,
                BoekHoofdstuk = boekHoofdstukLeviticus,
                BoekHoofdstukTekst_VersVanId = leviticus1V1,
                BoekHoofdstukTekst_VersTotId = leviticus1V3,
                Hoofdstuk = 1,
                Punt1 = "Punt1",
                Punt2 = "Punt2",
                Punt3 = "Punt3",
                Punt4 = "Punt4",
                Taal = taalNederlands,
                TaalId = taalNederlands.Id,
                PreekType = preektypeLeesPreek,
                PreekTypeId = preektypeLeesPreek.Id,
                Gemeente = gemeenteAmsterdam,
                Informatie = "Informatie",
                Predikant = predikantDeWit,
                Gepubliceerd = true,
                VersOmschrijving = "1 tot 3",
                DatumAangemaakt = DateTime.Now,
                DatumBijgewerkt = DateTime.Now,
                Gebruiker_AangepastDoor = testgebruiker2,
                AutomatischeTeksten = true
            });

            var afbeelding = context.Afbeeldings.Add(new Afbeelding { Bestandsnaam = "", ContentType = "", Omschrijving = "Testafbeelding" });
            var spotlight = context.Spotlights.Add(new Spotlight { Afbeelding = afbeelding, NieuwVenster = false, Taal = taalNederlands, Subtitel = "Subtitel", Sortering = 1, LinkTitel = "LinkTitel", Titel = "Titel", Url = "http://www.prekenweb.nl" });
            var nieuwsbriefNL = context.Mailings.Add(new Mailing { Omschrijving = "Nieuwsbrief", Taal = taalNederlands, MailChimpId = ""});
      
        }

        public static Gebruiker TestGebruiker1 = new Gebruiker
        {
            Id = 1,
            Naam = "Testgebruiker 1",
            Email = "test1@prekenweb.nl",
            UserName = "test1@prekenweb.nl",
            LockoutEnabled = false,
            // Wachtwoord = prekenweb 
        };

        public static Gebruiker TestGebruiker2 = new Gebruiker
        {
            Id = 2,
            Naam = "Testgebruiker 2",
            Email = "test2@prekenweb.nl",
            UserName = "test2@prekenweb.nl",
        };
    }
}
