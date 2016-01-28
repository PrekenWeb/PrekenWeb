using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using CaptchaMvc.Attributes;
using PrekenWeb.Data;
using PrekenWeb.Data.Identity;
using PrekenWeb.Data.Repositories;
using PrekenWeb.Data.Tables;
using Prekenweb.Models;
using Prekenweb.Website.Controllers;
using Prekenweb.Website.Lib.ActionResults;
using Prekenweb.Website.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web.Mvc;

namespace Prekenweb.Website.Areas.Website.Controllers
{
    public class PrekenwebController : ApplicationController
    {
        private readonly IPrekenwebContext<Gebruiker> _context;
        private readonly ITekstRepository _tekstRepository;

        public PrekenwebController(IPrekenwebContext<Gebruiker> context,
                                   ITekstRepository tekstRepository )
        {
            _context = context;
            _tekstRepository = tekstRepository;
            ViewBag.Taalkeuze = true;
        }

        public ActionResult Footer()
        {
            return PartialView();
        }

        public ActionResult _CKEditor()
        {
            return PartialView();
        }

        public ActionResult _Menu()
        {
            return PartialView();
        }

        public ActionResult Login()
        {
            if (Request.Url != null && Request.Url.Host.Contains("sermonweb"))
                return RedirectToAction("Inloggen", "Gebruiker", new { Area = "Mijn", Culture = "en" });

            return RedirectToAction("Inloggen", "Gebruiker", new { Area = "Mijn", Culture = "nl" });
        }

        public ActionResult MijnHome()
        {
            if (Request.Url != null && Request.Url.Host.Contains("sermonweb"))
                return RedirectToAction("Index", "Home", new { Area = "Mijn", Culture = "en" });

            return RedirectToAction("Index", "Home", new { Area = "Mijn", Culture = "nl" });
        }

        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#", Justification="Controller.Redirect only accepts a string based Uri")]
        public ActionResult WisselTaal(string taal, string returnUrl)
        {
            var rootFolder = Url.Content("~/");
            if (string.IsNullOrWhiteSpace(returnUrl)) returnUrl = "/";

            var applicationUrl = (returnUrl.Substring(0, rootFolder.Length) == rootFolder) ? returnUrl.Substring(rootFolder.Length) : returnUrl;
            if (Enum.GetNames(typeof(Culture)).Any(culture => applicationUrl.StartsWith(culture)))
            {
                applicationUrl = applicationUrl.Substring(Enum.GetNames(typeof(Culture)).First(culture => applicationUrl.StartsWith(culture)).Length);
            }
            if (Request.Url != null && !Request.Url.Host.EndsWith("localhost"))
            {
                switch (taal)
                {
                    case "en":
                        return Redirect("http://www.sermonweb.org" + rootFolder + taal + applicationUrl);
                    default:
                    //case "nl":
                        return Redirect("http://www.prekenweb.nl" + rootFolder + taal + applicationUrl);
                }
            }
            return Redirect(rootFolder + taal + applicationUrl);
        }

        public ActionResult Pagina(string pagina)
        {
            if (!_context.Paginas.Any(p => p.Identifier == pagina)) return HttpNotFound();
            return View(_tekstRepository.GetTekstPagina(pagina, TaalId));
        }
        public ActionResult PartialPagina(string pagina)
        {
            if (!_context.Paginas.Any(p => p.Identifier == pagina)) return HttpNotFound();
            return PartialView("Pagina", _tekstRepository.GetTekstPagina(pagina,TaalId));
        }

        public ActionResult UMoetInloggen(InlogReden reden)
        {
            return PartialView("UMoetInloggen", new UMoetInloggen
            {
                ReturnURL = Request.UrlReferrer != null ? Request.UrlReferrer.LocalPath : "",
                InlogReden = reden
            });
        }

        public ActionResult Contact(bool? beta)
        {
            return View(new Contact
            {
                TekstPagina = _tekstRepository.GetTekstPagina("contact", TaalId),
                Verzonden = false,
                Tekst = beta.HasValue && beta.Value ? "Layout: \r\n\r\nZoekmogelijkheden: \r\n\r\nPreek luisteren/lezen: \r\n\r\nProblemen?: \r\n\r\nVerbeterpunten:\r\n\r\n" : ""
            });
        }

        [HttpPost, CaptchaVerify("Captcha is not valid")]
        public ActionResult Contact(Contact viewModel)
        {
            viewModel.TekstPagina = _tekstRepository.GetTekstPagina("Contact", TaalId);

            if (ModelState.IsValid)
            {
                var inbox = new Inbox
                {
                    Afgehandeld = false,
                    VanNaam = viewModel.Naam,
                    VanEmail = viewModel.Email,
                    Inhoud = string.Format("{0}", viewModel.Tekst).Replace(Environment.NewLine, "<br/>"),
                    Omschrijving = viewModel.Onderwerp,
                    InboxTypeId = _context.InboxTypes.Single(it => it.Omschrijving == "Contactformulier").Id,
                    Aangemaakt = DateTime.Now
                };
                _context.Inboxes.Add(inbox);
                _context.SaveChanges();
                viewModel.Verzonden = true;
            }
            return View(viewModel);
        }

        public ActionResult PreekToevoegen()
        {
            return View(new PreekToevoegen
            {
                TekstPagina = _tekstRepository.GetTekstPagina("preek-toevoegen",TaalId),
                Verzonden = false
            });
        }

        [HttpPost, CaptchaVerify("Captcha is not valid")]
        public ActionResult PreekToevoegen(PreekToevoegen viewModel)
        {
            viewModel.TekstPagina = _tekstRepository.GetTekstPagina("preek-toevoegen",TaalId);

            if (ModelState.IsValid)
            {
                var inbox = new Inbox
                {
                    Afgehandeld = false,
                    VanNaam = viewModel.Naam,
                    VanEmail = viewModel.Email,
                    Inhoud = string.Format("<b>Telefoon:</b>{1}<br/><b>Tekst</b>:<br/>{0}", string.Format("{0}", viewModel.Tekst).Replace(Environment.NewLine, "<br/>"), viewModel.Telefoon),
                    Omschrijving = string.Format("{0} wil een preek toevoegen", viewModel.Naam),
                    InboxTypeId = _context.InboxTypes.Single(it => it.Omschrijving == "Preek toevoegen").Id,
                    Aangemaakt = DateTime.Now
                };
                _context.Inboxes.Add(inbox);
                _context.SaveChanges();
                viewModel.Verzonden = true;
            }
            return View(viewModel);
        }

        [OutputCache(Duration = 3600)] // 1 uur
        public RssActionResult Rss()
        {
            var feed = new SyndicationFeed();

            var items = _context
                .Preeks
                .Include(x => x.Predikant)
                .Where(x => x.TaalId == TaalId && x.Gepubliceerd)
                .OrderByDescending(x => x.DatumAangemaakt)
                .Take(10)
                .ToList()
                .Select(x =>
                {
                    var i = new SyndicationItem
                    {
                        Title = new TextSyndicationContent(x.GetBoekhoofdstukOmschrijving()),
                        PublishDate = x.DatumAangemaakt.HasValue ? new DateTimeOffset(x.DatumAangemaakt.Value) : new DateTimeOffset(),
                        LastUpdatedTime = x.DatumBijgewerkt.HasValue ? new DateTimeOffset(x.DatumBijgewerkt.Value) : new DateTimeOffset(),
                        BaseUri = new Uri(Url.Action("Open", "Preek", new { x.Id }), UriKind.Relative),

                    };
                    i.Authors.Add(new SyndicationPerson { Name = x.Predikant.VolledigeNaam });
                    //i.Links.Add(new SyndicationLink(new Uri("http://nu.nl"),"self","asd", "text/html", 1000));
                    return i;
                });

            feed.Items = items;

            return new RssActionResult
            {
                Feed = feed
            };
        }

        [OutputCache(Duration = 86400)] // 1 dag
        public ActionResult Nieuwsbrief()
        {
            var mailChimpListId = _context.Mailings.First(x => x.TaalId == TaalId).MailChimpId;
            var nieuwsbrieven = MailChimpController.GetSendCampains(mailChimpListId);

            var viewmodel = new Nieuwsbrief
            {
                TekstPagina = _tekstRepository.GetTekstPagina("nieuwsbrief",TaalId),
                Nieuwsbrieven = nieuwsbrieven
            };

            return View(viewmodel);
        }

        //public ActionResult Boek()
        //{
        //    return View(new BoekViewModel
        //    {
        //        TekstPagina = _tekstRepository.GetTekstPagina("boeken",TaalId),
        //        Verzonden = false,
        //        Aantal = 1
        //    });
        //}

        //[HttpPost, CaptchaVerify("Captcha is not valid")]
        //public ActionResult Boek(BoekViewModel viewModel)
        //{
        //    viewModel.TekstPagina = _tekstRepository.GetTekstPagina("boeken", TaalId);
        //    if (viewModel.VerzendMethode == BoekViewModel.VerzendMethodes.Ophalen && string.IsNullOrWhiteSpace(viewModel.OphaalLocatie)) ModelState.AddModelError("OphaalLocatie", "Kies een ophaallocatie");

        //    if (ModelState.IsValid)
        //    {
        //        var inbox = new Inbox
        //        {
        //            Afgehandeld = false,
        //            VanNaam = viewModel.Naam,
        //            VanEmail = viewModel.Email,
        //            AanNaam = "PrekenWeb - Boeken",
        //            AanEmail = "boeken@prekenweb.nl",
        //            Inhoud = string.Format(
        //                @"
        //                <b>Nummer:</b>[[[ordernummer]]]
        //                <br/><b>Aanhef:</b>{0}
        //                <br/><b>Naam:</b>{1}
        //                <br/><b>Email:</b>{2}
        //                <br/><b>Postcode:</b>{3}
        //                <br/><b>Woonplaats:</b>{4}
        //                <br/><b>Straat:</b>{5}
        //                <br/><b>Huisnummer:</b>{6}
        //                <br/><b>Boek:</b>{7}
        //                <br/><b>Aantal:</b>{8}
        //                <br/><b>Verzendmethode:</b>{9}
        //                <br/><b>Ophalen in:</b>{10}
        //                <br/><b>Tekst</b>:
        //                <br/>{11}",
        //                         viewModel.Aanhef.ToString(),
        //                         viewModel.Naam,
        //                         viewModel.Email,
        //                         viewModel.Postcode,
        //                         viewModel.Woonplaats,
        //                         viewModel.Straat,
        //                         viewModel.Huisnummer,
        //                         viewModel.Boeken.Single(x => x.Value == viewModel.BoekId.ToString(CultureInfo.InvariantCulture)).Text,
        //                         viewModel.Aantal,
        //                         viewModel.VerzendMethode.ToString(),
        //                         viewModel.OphaalLocatie,
        //                         string.Format("{0}", viewModel.Tekst).Replace(Environment.NewLine, "<br/>")
        //                         ),
        //            Omschrijving = string.Format("{0} plaatst een bestelling voor boek {1}", viewModel.Naam, viewModel.Boeken.Single(x => x.Value == viewModel.BoekId.ToString(CultureInfo.InvariantCulture)).Text),
        //            InboxTypeId = _context.InboxTypes.Single(it => it.Omschrijving == "Boekbestelling").Id,
        //            Aangemaakt = DateTime.Now
        //        };
        //        _context.Inboxes.Add(inbox);
        //        _context.SaveChanges();

        //        inbox.Inhoud = inbox.Inhoud.Replace("[[[ordernummer]]]", inbox.Id.ToString(CultureInfo.InvariantCulture));
        //        _context.SaveChanges();

        //        inbox.InboxOpvolgings.Add(new InboxOpvolging
        //        {
        //            Aangemaakt = DateTime.Now,
        //            Onderwerp = string.Format("Ontvangsbevestiging bestelling {0}", viewModel.Boeken.Single(x => x.Value == viewModel.BoekId.ToString(CultureInfo.InvariantCulture)).Text),
        //            VerstuurAlsMail = true,
        //            Tekst = string.Format(@"
        //                Geachte {0} {1},<br/><br/>

        //                Hartelijk dank voor uw bestelling. U heeft {2} boek(en) besteld en er voor gekozen {3}.<br/><br/>

        //                De totaalprijs hiervoor bedraagt {4}.<br/><br/>

        //                Wilt u dit geld onder vermelding van ordernummer {5} overmaken op girorekening INGBNL2A NL78INGB0003022979 t.n.v. PrekenWeb te Hendrik-Ido-Ambacht?<br/><br/>

        //                Nadat wij het geld ontvangen hebben, zullen wij u het boekje toezenden. Als u gekozen heeft het boekje op te halen bij een van de distributiepunten krijgt u een e-mail met informatie waar en wanneer het boekje opgehaald kan worden.<br/><br/>

        //                Vriendelijke groeten,<br/><br/>

        //                PrekenWeb",
        //                viewModel.Aanhef.ToString(),
        //                viewModel.Naam,
        //                viewModel.Aantal,
        //                viewModel.VerzendMethode.ToString(),
        //                viewModel.Prijs,
        //                 inbox.Id
        //            )
        //        });
        //        _context.SaveChanges();
        //        viewModel.Verzonden = true;
        //    }
        //    return View(viewModel);
        //}
    }
}
