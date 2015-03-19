using Prekenweb.Models;
using Prekenweb.Models.Identity;
using Prekenweb.Website.Controllers;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;
using Prekenweb.Website.Lib;

namespace Prekenweb.Website.Areas.Website.Controllers
{
    public class LegacyRoutingController : ApplicationController
    {
        private readonly IPrekenwebContext<Gebruiker> _context;

        public LegacyRoutingController(IPrekenwebContext<Gebruiker> context)
        {
            _context = context;
        } 

        public ActionResult LegacyRouting(string queryString)
        {
            try
            {
                var qs = new NameValueCollection();

                if (!string.IsNullOrEmpty(queryString)) qs.Add(queryString, queryString);
                qs.Add(Request.QueryString);

                if (qs.Count == 0) return RedirectToHome();

                switch (qs[0].ToLower())
                {
                    case "":
                    case "/":
                    case "home":
                        return RedirectToHome();

                    case "zoeken":
                        return RedirectToActionPermanent("Index", "Zoeken", new { Area = "Website", Culture = "nl" });

                    case "contact":
                        return RedirectToActionPermanent("Contact", "Prekenweb", new { Area = "Website", Culture = "nl" });

                    case "hulp":
                        return RedirectToActionPermanent("Pagina", "Prekenweb", new { Area = "Website", pagina = "hulp", Culture = "nl" });

                    case "links":
                        return RedirectToActionPermanent("Pagina", "Prekenweb", new { Area = "Website", pagina = "links", Culture = "nl" });

                    case "boeken":
                        return RedirectToActionPermanent("Pagina", "Prekenweb", new { Area = "Website", pagina = "boeken", Culture = "nl" });

                    case "laatst-toegevoegde-preken":
                        return RedirectToActionPermanent("Index", "Zoeken", new { Area = "Website", Laatste = true, Culture = "nl" });

                    case "preken-per-voorganger":
                        if (string.IsNullOrEmpty(qs["pid"]))
                        {
                            return RedirectToActionPermanent("Predikant", "Zoeken", new { Area = "Website", Culture = "nl" });
                        }
                        else
                        {
                            var oudId = int.Parse(qs["pid"]);
                            var predikant = _context.Predikants.SingleOrDefault(p => p.OudId == oudId);
                            if (predikant == null) return HttpNotFound();
                            return RedirectToActionPermanent("Index", "Zoeken", new { Area = "Website", PredikantId = predikant.Id, Culture = "nl" });
                        }

                    case "preken-per-bijbelboek":
                        if (string.IsNullOrEmpty(qs["book"]) && string.IsNullOrEmpty(qs["cat"]))
                        {
                            return RedirectToActionPermanent("Boek", "Zoeken", new { Area = "Website", culture = "nl" });
                        }
                        else
                        {
                            var oudId = string.IsNullOrEmpty(qs["book"]) ? int.Parse(qs["cat"]) : int.Parse(qs["book"]);
                            switch (oudId)
                            {
                                case 73:
                                case 85:
                                case 118:
                                case 187:
                                case 311:
                                    return RedirectToActionPermanent("Boek", "Zoeken", new { Area = "Website", Culture = "nl" });
                                default:
                                    var boekHoofdstuk = _context.BoekHoofdstuks.SingleOrDefault(bh => bh.OudId == oudId);
                                    if (boekHoofdstuk == null) return HttpNotFound();
                                    return RedirectToActionPermanent("Index", "Zoeken", new { Area = "Website", BoekhoofdstukId = boekHoofdstuk.Id, Culture = "nl" });
                            }
                        }
                    case "themas":
                        if (string.IsNullOrEmpty(qs["theme"]))
                        {
                            return RedirectToActionPermanent("Gelegenheid", "Zoeken", new { Area = "Website", Culture = "nl" });
                        }
                        else
                        {
                            var oudId = int.Parse(qs["theme"]);
                            var gebeurtenis = _context.Gebeurtenis.SingleOrDefault(g => g.OudId == oudId);
                            if (gebeurtenis == null) return HttpNotFound();
                            return RedirectToActionPermanent("Index", "Zoeken", new { Area = "Website", GebeurtenisId = gebeurtenis.Id, Culture = "nl" });
                        }

                    case "lezingen":
                        if (string.IsNullOrEmpty(qs["lz"]))
                        {
                            return RedirectToActionPermanent("Index", "Zoeken", new { Area = "Website", PreekTypeId = 2, Culture = "nl" });
                        }
                        else
                        {
                            var oudId = int.Parse(qs["lz"]);
                            var lezingCategorie = _context.LezingCategories.SingleOrDefault(lc => lc.OudId == oudId);
                            if (lezingCategorie == null) return HttpNotFound();
                            return RedirectToActionPermanent("Index", "Zoeken", new { Area = "Website", PreekTypeId = 2, LezingCategorieId = lezingCategorie.Id, Culture = "nl" });
                        }


                    case "leespreken":
                        if (string.IsNullOrEmpty(qs["pid"]))
                        {
                            return RedirectToActionPermanent("Index", "Zoeken", new { Area = "Website", PreekTypeId = 3, Culture = "nl" });
                        }
                        else
                        {
                            var oudId = int.Parse(qs["pid"]);
                            var predikant = _context.Predikants.SingleOrDefault(p => p.OudId == oudId);
                            if (predikant == null) return HttpNotFound();
                            return RedirectToActionPermanent("Index", "Zoeken", new { Area = "Website", PreekTypeId = 3, PredikantId = predikant.Id, Culture = "nl" });
                        }


                    case "english":
                        if (string.IsNullOrEmpty(qs["pid"]))
                        {
                            return RedirectToActionPermanent("Index", "Zoeken", new { Area = "Website", Culture = "en" });
                        }
                        else
                        {
                            var oudId = int.Parse(qs["pid"]);
                            var predikant = _context.Predikants.SingleOrDefault(p => p.OudId == oudId);
                            if (predikant == null) return HttpNotFound();
                            return RedirectToActionPermanent("Index", "Zoeken", new { Area = "Website", PredikantId = predikant.Id, Culture = "en" });
                        }

                    case "com_content":
                        if (string.IsNullOrEmpty(qs["serid"])) return HttpNotFound();
                        var id = int.Parse(qs["serid"]);
                        var preek = _context.Preeks.SingleOrDefault(p => p.OudId == id);
                        var taal = "nl";
                        if (preek == null) return HttpNotFound();
                        if (preek.TaalId == 2) taal = "en";
                        return RedirectToActionPermanent("Open", "Preek", new { Area = "Website", Id = preek.Id, Culture = taal });

                    default:
                        if (string.IsNullOrEmpty(qs["file"])) return HttpNotFound();
                        var bestandsnaam = qs["file"].ToLower().Trim();
                        var preek2 = _context.Preeks.SingleOrDefault(p => p.Bestandsnaam.ToLower().Trim() == bestandsnaam);
                        if (preek2 == null) return HttpNotFound();
                        return Redirect(string.Format("~/Content/preken/{0}", preek2.Bestandsnaam));
                        //else return RedirectToActionPermanent("Download", "Preek", new { Area = "Website", Id = preek2.Id, inline = false });
                }
                //return RedirectToHome();
            }
            catch //(Exception ex)
            {
                return RedirectToHome();
            }
        }

        public ActionResult RedirectToHome()
        {
            return RedirectToActionPermanent("Index", "Home", new { Area = "Website" });
        }
    }
}
