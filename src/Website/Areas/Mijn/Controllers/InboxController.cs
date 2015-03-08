using System.Threading.Tasks;
using Hangfire;
using Prekenweb.Models;
using Prekenweb.Models.Identity;
using PrekenWeb.Security;
using Prekenweb.Website.Areas.Mijn.Models;
using Prekenweb.Website.Controllers;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using Prekenweb.Website.Lib;
using Prekenweb.Website.Hangfire;

namespace Prekenweb.Website.Areas.Mijn.Controllers
{
    [Authorize(Roles = "Inbox")]
    public class InboxController : ApplicationController
    {
        private readonly IPrekenwebContext<Gebruiker> _context;
        private readonly IPrekenWebUserManager _prekenWebUserManager;
        private readonly IHuidigeGebruiker _huidigeGebruiker;

        public InboxController(IPrekenwebContext<Gebruiker> context,
                               IPrekenWebUserManager prekenWebUserManager,
                               IHuidigeGebruiker huidigeGebruiker)
        {
            _context = context;
            _prekenWebUserManager = prekenWebUserManager;
            _huidigeGebruiker = huidigeGebruiker;
        }

        private const string MailTemplate = @"<div>
        <p style='font-family:arial; font-size:12px; color:#070045'>Beste  ,</p>

        <p style='font-family:arial; font-size:12px; color:#070045'>&nbsp;</p> 
        
        <p style='font-family:arial; font-size:12px; color:#070045'>Met vriendelijke groet namens PrekenWeb, </p>

        <p style='font-family:arial; font-size:12px; color:#070045'>{0}<br>
        <a href='mailto:info@prekenweb.nl'><span>info@prekenweb.nl</span></a> <a href='http://www.prekenweb.nl/'><span>www.prekenweb.nl</span></a></p>

        <p style='font-family:arial; font-size:12px; color:#070045'>Bestel hier het prachtige boek van ds.
        G.J. Baan, ‘Die dorst heeft, kome’ en steun hiermee PrekenWeb.nl:&nbsp; <a href='http://bestellen.prekenweb.nl/?boekid=2'><span>http://bestellen.prekenweb.nl/?boekid=2</span></a></p>

        <p style='font-family:arial; font-size:12px; color:#070045'></p>

        <p style='font-family:arial; font-size:12px; color:#070045'></p>

        <p style='font-family:arial; font-size:10px; color:#070045'><font size='1'><i>De informatie in dit e-mailbericht is
        vertrouwelijk. Als deze informatie niet voor u is bestemd, verzoekt
        PrekenWeb.nl u om contact op te nemen met de afzender en de gegevens te
        vernietigen. Het is in dat geval ook niet toegestaan om de gegevens te gebruiken,
        te kopiëren of te verstrekken aan derden.</i></font></p> 
        </div>
        ";

        public ActionResult Index()
        {
            return View(new InboxIndexViewModel
            {
                InboxIitems = _context.Inboxes.OrderByDescending(i => i.Id).ToList()
            });
        }
        public ActionResult Tonen(int id)
        {
            return View(new InboxEditViewModel
            {
                InboxItem = _context.Inboxes.Include(x => x.InboxOpvolgings).Single(i => i.Id == id)
            });
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Tonen(InboxEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(viewModel.InboxItem).State = EntityState.Modified;
                _context.SaveChanges();
            }
            viewModel.InboxItem.InboxOpvolgings = _context.InboxOpvolgings.Where(io => io.InboxId == viewModel.InboxItem.Id).ToList();
            return View(viewModel);
        }

        public async Task<ActionResult> OpvolgingToevoegen(int inboxId)
        {
            var inboxItem = _context.Inboxes.Single(i => i.Id == inboxId);
            var gebruikerId = _huidigeGebruiker.Id;
            var gebruiker = await _prekenWebUserManager.FindByIdAsync(gebruikerId);
            return View(new InboxOpvolgingToevoegenViewModel
            {
                DirectAfhandelen = true,
                InboxOpvolging = new InboxOpvolging
                {
                    InboxId = inboxId,
                    Aangemaakt = DateTime.Now,
                    GebruikerId = gebruiker.Id,
                    VerstuurAlsMail = true,
                    Onderwerp = string.Format("Re: {0}", inboxItem.Omschrijving),
                    Tekst = string.Format("{0}", string.Format(MailTemplate, gebruiker.Naam))
                },
                Inbox = inboxItem
            });
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult OpvolgingToevoegen(InboxOpvolgingToevoegenViewModel viewModel)
        {
            var inboxItem = _context.Inboxes.Single(i => i.Id == viewModel.InboxOpvolging.InboxId);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.InboxOpvolgings.Add(viewModel.InboxOpvolging);
                    if (viewModel.DirectAfhandelen) inboxItem.Afgehandeld = true;
                    _context.SaveChanges();

                    BackgroundJob.Enqueue<AchtergrondTaken>(x => x.InboxOpvolgingTaak(viewModel.InboxOpvolging.Id));
                }
                catch (DbEntityValidationException ex)
                {
                    throw new Exception(ex.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage);
                }
                return RedirectToAction("Tonen", new { Id = viewModel.InboxOpvolging.InboxId });
            }
            viewModel.Inbox = inboxItem;
            return View(viewModel);
        }

    }
}
