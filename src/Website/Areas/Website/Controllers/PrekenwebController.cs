using System.Diagnostics.CodeAnalysis;
//using CaptchaMvc.Attributes;
using PrekenWeb.Data;
using PrekenWeb.Data.Identity;
using PrekenWeb.Data.Repositories;
using PrekenWeb.Data.Tables;
using Prekenweb.Website.Lib.ActionResults;
using System;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using Prekenweb.Website.Areas.Website.Models;
using Prekenweb.Website.Lib;

namespace Prekenweb.Website.Areas.Website.Controllers
{
    using BotDetect.Web.Mvc;

    public class PrekenwebController : Controller
    {
        private readonly IPrekenwebContext<Gebruiker> _context;
        private readonly ITekstRepository _tekstRepository;

        public PrekenwebController(IPrekenwebContext<Gebruiker> context,
                                   ITekstRepository tekstRepository )
        {
            _context = context;
            _tekstRepository = tekstRepository; 
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
 

        public ActionResult Pagina(string pagina)
        {
            if (!_context.Paginas.Any(p => p.Identifier == pagina)) return HttpNotFound();
            return View(_tekstRepository.GetTekstPagina(pagina, TaalInfoHelper.FromRouteData(RouteData).Id));
        }
        public ActionResult PartialPagina(string pagina)
        {
            if (!_context.Paginas.Any(p => p.Identifier == pagina)) return HttpNotFound();
            return PartialView("Pagina", _tekstRepository.GetTekstPagina(pagina, TaalInfoHelper.FromRouteData(RouteData).Id));
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
                TekstPagina = _tekstRepository.GetTekstPagina("contact", TaalInfoHelper.FromRouteData(RouteData).Id),
                Verzonden = false,
                Tekst = beta.HasValue && beta.Value ? "Layout: \r\n\r\nZoekmogelijkheden: \r\n\r\nPreek luisteren/lezen: \r\n\r\nProblemen?: \r\n\r\nVerbeterpunten:\r\n\r\n" : ""
            });
        }

        [HttpPost/*, CaptchaVerify("Captcha is not valid")*/, CaptchaValidation("CaptchaCode", "Captcha", "Incorrecte CAPTCHA code.")]
        public ActionResult Contact(Contact viewModel)
        {
            viewModel.TekstPagina = _tekstRepository.GetTekstPagina("Contact", TaalInfoHelper.FromRouteData(RouteData).Id);

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
                TekstPagina = _tekstRepository.GetTekstPagina("preek-toevoegen", TaalInfoHelper.FromRouteData(RouteData).Id),
                Verzonden = false
            });
        }

        [HttpPost/*, CaptchaVerify("Captcha is not valid")*/, CaptchaValidation("CaptchaCode", "Captcha", "Incorrecte CAPTCHA code.")]
        public ActionResult PreekToevoegen(PreekToevoegen viewModel)
        {
            viewModel.TekstPagina = _tekstRepository.GetTekstPagina("preek-toevoegen", TaalInfoHelper.FromRouteData(RouteData).Id);

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
            var taalId = TaalInfoHelper.FromRouteData(RouteData).Id;

            var items = _context
                .Preeks
                .Include(x => x.Predikant)
                .Where(x => x.TaalId == taalId && x.Gepubliceerd)
                .OrderByDescending(x => x.DatumGepubliceerd)
                .Take(10)
                .ToList()
                .Select(x =>
                {
                    var i = new SyndicationItem
                    {
                        Title = new TextSyndicationContent(x.GetBoekhoofdstukOmschrijving()),
                        PublishDate = x.DatumGepubliceerd.HasValue 
                            ? new DateTimeOffset(x.DatumGepubliceerd.Value)
                            : x.DatumAangemaakt.HasValue 
                                ? new DateTimeOffset(x.DatumAangemaakt.Value) : new DateTimeOffset(),
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
            var taalId = TaalInfoHelper.FromRouteData(RouteData).Id;
            var mailChimpListId = _context.Mailings.First(x => x.TaalId == taalId).MailChimpId;
            var nieuwsbrieven = MailChimpController.GetSendCampains(mailChimpListId);

            var viewmodel = new Nieuwsbrief
            {
                TekstPagina = _tekstRepository.GetTekstPagina("nieuwsbrief", TaalInfoHelper.FromRouteData(RouteData).Id),
                Nieuwsbrieven = nieuwsbrieven
            };

            return View(viewmodel);
        }
    }
}
