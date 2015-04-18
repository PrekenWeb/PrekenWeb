using System.Configuration;
using System.Threading.Tasks;
using Prekenweb.Models;
using Prekenweb.Models.Identity;
using PrekenWeb.Security;
using Prekenweb.Website.Areas.Mijn.Models;
using Prekenweb.Website.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prekenweb.Website.Lib.Identity;

namespace Prekenweb.Website.Areas.Mijn.Controllers
{
    [Authorize(Roles = "Pagina")]
    public class PaginaController : ApplicationController
    {
        private readonly IPrekenwebContext<Gebruiker> _context;
        private readonly IHuidigeGebruiker _huidigeGebruiker;
        private readonly IPrekenWebUserManager _prekenWebUserManager;

        public PaginaController(IPrekenwebContext<Gebruiker> context,
                                IHuidigeGebruiker huidigeGebruiker,
                                IPrekenWebUserManager prekenWebUserManager)
        {
            _context = context;
            _huidigeGebruiker = huidigeGebruiker;
            _prekenWebUserManager = prekenWebUserManager;
        }

        public ActionResult Index()
        {
            return View(new PaginaIndexViewModel
            {
                Paginas = _context.Paginas.OrderByDescending(p => p.Id).ToList()
            });
        }

        public ActionResult Verwijder(int id)
        {
            var pagina = _context.Paginas.Single(p => p.Id == id);

            _context.Paginas.Remove(pagina);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public async Task<ViewResult> Bewerk(int id)
        {
            var pagina = _context.Paginas.Single(p => p.Id == id);

            pagina.Bijgewerkt = DateTime.Now;
            pagina.BijgewerktDoor = await _huidigeGebruiker.GetId(_prekenWebUserManager, User);

            return View(new PaginaEditViewModel
            {
                Pagina = pagina,
                Teksten = GetPaginaTeksten(id)
            });
        }

        public List<Tekst> GetPaginaTeksten(int paginaId)
        {
            var teksten = new List<Tekst>();
            _context
                .Paginas
                .Single(p => p.Id == paginaId)
                .Teksts
                .ToList()
                .ForEach(teksten.Add);

            _context
                .Taals
                .ToList()
                .Where(taal =>
                    !teksten.Select(t => t.TaalId).Contains(taal.Id)
                )
                .ToList()
                .ForEach(t =>
                    teksten.Add(new Tekst
                    {
                        TaalId = t.Id,
                        Taal = t,
                        PaginaId = paginaId
                    })
                );
            return teksten;
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Bewerk(PaginaEditViewModel viewModel)
        {
            if (_context.Paginas.Any(p => p.Id != viewModel.Pagina.Id && p.Identifier == viewModel.Pagina.Identifier)) ModelState.AddModelError("Pagina.Identifier", "Deze is al in gebruik!");

            var i = 0;
            viewModel.Teksten.ToList().ForEach(t =>
                {
                    if (string.IsNullOrEmpty(t.Kop))
                    {
                        ModelState.Remove(string.Format("Teksten[{0}].Kop", i));
                        ModelState.Remove(string.Format("Teksten[{0}].Tekst1", i));
                        viewModel.Teksten.Remove(t);
                    }
                    i++;
                });

            if (ModelState.IsValid)
            {
                _context.Entry(viewModel.Pagina).State = EntityState.Modified;

                _context.Teksts.Where(t => t.PaginaId == viewModel.Pagina.Id).ToList().ForEach(t => _context.Teksts.Remove(t));
                _context.SaveChanges();

                viewModel.Teksten.ForEach(t => _context.Teksts.Add(t));
                _context.SaveChanges();

                ClearOutputCaches();
            }
            viewModel.Teksten = GetPaginaTeksten(viewModel.Pagina.Id);
            return View(viewModel);
        }

        public async Task<ViewResult> Maak()
        {
            return View(new PaginaEditViewModel
            {
                Pagina = new Pagina
                {
                    Gepubliceerd = true,
                    Bijgewerkt = DateTime.Now,
                    Aangemaakt = DateTime.Now,
                    BijgewerktDoor = await _huidigeGebruiker.GetId(_prekenWebUserManager, User),
                    AangemaaktDoor = await _huidigeGebruiker.GetId(_prekenWebUserManager, User),
                    TonenOpHomepage = false
                }
            });
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Maak(PaginaEditViewModel viewModel)
        {
            if (_context.Paginas.Any(p => p.Identifier == viewModel.Pagina.Identifier)) ModelState.AddModelError("Pagina.Identifier", "Deze is al in gebruik!");

            if (!ModelState.IsValid) return View(viewModel);

            _context.Paginas.Add(viewModel.Pagina);
            _context.SaveChanges();
            
            ClearOutputCaches();
            
            return RedirectToAction("Bewerk", new { viewModel.Pagina.Id });
        }

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase upload, string ckEditorFuncNum, string ckEditor, string langCode)
        {
            var relativePath = string.Format(@"{0}\UserUpload_{1}", ConfigurationManager.AppSettings["AfbeeldingenFolder"], upload.FileName);
            var savedFileLocation = Server.MapPath(relativePath);
            upload.SaveAs(savedFileLocation);

            var url = Url.Content(relativePath);
             
            const string message = "Afbeelding succesvol opgeslagen";
             
            var output = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + ckEditorFuncNum + ", \"" + url + "\", \"" + message + "\");</script></body></html>";
            return Content(output);
        }

    }
}
