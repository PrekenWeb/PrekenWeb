﻿using Prekenweb.Models;
using Prekenweb.Models.Identity;
using Prekenweb.Website.Areas.Mijn.Models;
using Prekenweb.Website.Controllers;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Prekenweb.Website.Areas.Mijn.Controllers
{
    [Authorize(Roles = "Stamgegevens")]
    public class PredikantController : ApplicationController
    {
        private readonly IPrekenwebContext<Gebruiker> _context;

        public PredikantController(IPrekenwebContext<Gebruiker> context)
        {
            _context = context;
        } 


        public ActionResult Index()
        {
            return View(new PredikantIndexViewModel
            {
                Predikanten = _context.Predikants.Where(p => p.TaalId == TaalId).OrderBy(p => p.Achternaam).ToList()
            });
        }

        public ActionResult Verwijder(int id)
        {
            var predikant = _context.Predikants.Single(p => p.Id == id);
            _context.Predikants.Remove(predikant);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Bewerk(int id)
        {
            var predikant = _context.Predikants.Single(p => p.Id == id);
            return View(new PredikantEditViewModel
            {
                Predikant = predikant
            });
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Bewerk(PredikantEditViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            
            _context.Entry(viewModel.Predikant).State = EntityState.Modified;
            _context.SaveChanges();

            return View(viewModel);
        }

        public ActionResult Maak()
        {
            return View(new PredikantEditViewModel
            {
                Predikant = new Predikant { Titels = "Ds.", TaalId = TaalId }
            });
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Maak(PredikantEditViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            
            _context.Predikants.Add(viewModel.Predikant);
            _context.SaveChanges();
            
            return RedirectToAction("Bewerk", new { Id = viewModel.Predikant.Id });
        }
    }
}
