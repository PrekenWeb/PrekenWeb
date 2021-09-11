namespace Prekenweb.Website.Areas.Mijn.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;

    using Data;
    using Data.Identity;
    using Data.Repositories;
    using Data.Tables;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Newtonsoft.Json;
    using Prekenweb.Website.Areas.Mijn.Models;
    using Prekenweb.Website.Areas.Website.Controllers;
    using Prekenweb.Website.Lib;
    using Prekenweb.Website.Lib.Identity;
    using Prekenweb.Website.Lib.MailTemplating;

    using PrekenWeb.Security;
    using reCAPTCHA.MVC;
    using TweetSharp;

    public class GebruikerController : Controller
    {
        private readonly IGebruikerRepository _gebruikerRepository;
        private readonly IMailingRepository _mailingRepository;
        private readonly ITekstRepository _tekstRepository;
        private readonly IPrekenwebContext<Gebruiker> _prekenwebContext;
        private readonly IPrekenWebUserManager _prekenWebUserManager;
        private readonly IHuidigeGebruiker _huidigeGebruiker;

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public GebruikerController(IGebruikerRepository gebruikerRepository,
                                   IMailingRepository mailingRepository,
                                   ITekstRepository tekstRepository,
                                   IPrekenwebContext<Gebruiker> context,
                                   IPrekenWebUserManager prekenWebUserManager,
                                   IHuidigeGebruiker huidigeGebruiker)
        {
            _gebruikerRepository = gebruikerRepository;
            _mailingRepository = mailingRepository;
            _tekstRepository = tekstRepository;
            _prekenwebContext = context;
            _prekenWebUserManager = prekenWebUserManager;
            _huidigeGebruiker = huidigeGebruiker;
        }

        #region Publiek: Inloggen, Uitloggen, Registreer

        [System.Web.Mvc.AllowAnonymous]
        public ActionResult Inloggen(string returnUrl)
        {
            return View(new AccountInloggen
            {
                ReturnUrl = returnUrl,
                Onthouden = true,
                TekstPagina = _tekstRepository.GetTekstPagina("Inloggen", TaalInfoHelper.FromRouteData(RouteData).Id)
            });
        }

        [System.Web.Mvc.HttpPost, ValidateAntiForgeryToken, System.Web.Mvc.AllowAnonymous]
        public async Task<ActionResult> Inloggen(AccountInloggen viewModel)
        {
            viewModel.TekstPagina = _tekstRepository.GetTekstPagina("Inloggen", TaalInfoHelper.FromRouteData(RouteData).Id);

            if (!ModelState.IsValid) return View(viewModel);

            var gebruiker = await _prekenWebUserManager.FindAsync(viewModel.Gebruikersnaam, viewModel.Wachtwoord);

            if (gebruiker == null)
            {
                ModelState.AddModelError("Gebruikersnaam", string.Format("Gebruikersnaam of wachtwoord onbekend! Vraag eventueel een nieuw wachtwoord op via '<a href='{0}'>wachtwoord vergeten</a>'", Url.Action("WachtwoordVergeten", new { gebruikersnaam = viewModel.Gebruikersnaam })));
                return View(viewModel);
            }

            await SignInAsync(gebruiker, viewModel.Onthouden);

            gebruiker.LaatstIngelogd = DateTime.Now;
            await _prekenWebUserManager.UpdateAsync(gebruiker);

            return RedirectAfterLogin(viewModel.ReturnUrl, gebruiker.Roles.Any());
        }

        public ActionResult Uitloggen()
        {
            AuthenticationManager.SignOut();

            HttpContext.Response.Cookies.Remove("Token");
            return View();
        }

        [System.Web.Mvc.AllowAnonymous]
        public ActionResult Registreer()
        {
            return View(new RegistreerViewModel
            {
                TekstPagina = _tekstRepository.GetTekstPagina("Registreer", TaalInfoHelper.FromRouteData(RouteData).Id)
            });
        }

        [System.Web.Mvc.Authorize]
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> GeopendePreken(int[] preekIds)
        {
            var gebruikerId = await _huidigeGebruiker.GetId(_prekenWebUserManager, User);
            var data = await _gebruikerRepository.GetPreekCookies(gebruikerId, preekIds);

            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var jsonResult = JsonConvert.SerializeObject(data, jss);
            return Content(jsonResult, "application/json");
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.HttpPost]
        [CaptchaValidator]
        public async Task<ActionResult> Registreer(RegistreerViewModel viewModel)
        {
            viewModel.TekstPagina = _tekstRepository.GetTekstPagina("Registreer", TaalInfoHelper.FromRouteData(RouteData).Id);

            if (!string.IsNullOrEmpty(viewModel.Email) && await _prekenWebUserManager.FindByEmailAsync(viewModel.Email) != null)
                ModelState.AddModelError("Email", string.Format(Resources.Resources.EmailNietBeschikbaar, Url.Action("WachtwoordVergeten", new { gebruikersnaam = viewModel.Email })));

            if(!string.IsNullOrEmpty(viewModel.Gebruikersnaam) &&
                await _prekenWebUserManager.FindByNameAsync(viewModel.Gebruikersnaam) != null)
                ModelState.AddModelError("Gebruikersnaam", Resources.Resources.GebruikerIsAlGeregistreerd);

            if (!ModelState.IsValid) return View(viewModel);

            var standaaardNieuwsbrief = (await _mailingRepository.GetAlleMailings()).First(x => x.TaalId == TaalInfoHelper.FromRouteData(RouteData).Id);
            var gebruiker = new Gebruiker
            {
                Email = viewModel.Email,
                UserName = viewModel.Email,
                Naam = viewModel.Naam,
                EmailConfirmed = true
            };

            var result = await _prekenWebUserManager.CreateAsync(gebruiker, viewModel.Wachtwoord);
            try
            {
                MailChimpController.Subscribe(gebruiker.Email, gebruiker.Naam, standaaardNieuwsbrief.MailChimpId);
                await _mailingRepository.NieuwsbriefToevoegenAanGebruiker(gebruiker.Id, standaaardNieuwsbrief.Id);
            }
            catch
            {
                Debug.Write("MailChimpController doet request naar MailChimp, laat Registreer actie hier niet op falen");
            }

            if (result.Succeeded)
            {
                await SignInAsync(gebruiker, false);
                return RedirectToAction("RegistreerSuccesvol");
            }

            AddIdentityResultErrorsToModelState(result);
            return View(viewModel);
        }

        public ActionResult RegistreerSuccesvol()
        {
            return View(new RegistreerSuccesvol
            {
                RegistreerSuccesvolTekst = _tekstRepository.GetTekstPagina("RegistreerSuccesvol", TaalInfoHelper.FromRouteData(RouteData).Id),
                WatIsMijnTekst = _tekstRepository.GetTekstPagina("Wat-is-mijn-PrekenWeb", TaalInfoHelper.FromRouteData(RouteData).Id)
            });
        }

        [System.Web.Mvc.AllowAnonymous, Throttle(Seconds = 2)]
        public async Task<JsonResult> EmailVrij(string email)
        {
            if (await _prekenWebUserManager.FindByEmailAsync(email) == null)
                return Json(true, JsonRequestBehavior.AllowGet);

            return Json(string.Format(Resources.Resources.EmailNietBeschikbaar, Url.Action("WachtwoordVergeten", new { gebruikersnaam = email })), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.AllowAnonymous, Throttle(Seconds = 2)]
        public async Task<JsonResult> GebruikersnaamVrij(string gebruikersnaam)
        {
            if (await _prekenWebUserManager.FindByNameAsync(gebruikersnaam) == null)
                return Json(true, JsonRequestBehavior.AllowGet);

            return Json(Resources.Resources.GebruikersnaamNietBeschikbaar, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> MijnGegevens()
        {
            return RedirectToAction("Bewerk", new { Id = await _huidigeGebruiker.GetId(_prekenWebUserManager, User) });
        }
        #endregion

        #region Helpers

        private ActionResult RedirectAfterLogin(string returnUrl, bool hasAnyRole)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);

            return Redirect(!hasAnyRole ? "~/" : Url.Action("Index", "Home"));
        }

        private async Task SignInAsync(Gebruiker gebruiker, bool onthouden)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = onthouden }, await gebruiker.GenerateUserIdentityAsync((PrekenWebUserManager)_prekenWebUserManager));
        }

        private void AddIdentityResultErrorsToModelState(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        #endregion

        #region Aanmaken & Bewerken

        [System.Web.Mvc.Authorize(Roles = "Gebruikers")]
        public ActionResult Index(GebruikerIndexViewModel viewmodel)
        {
            viewmodel.Gebruikers = _prekenWebUserManager.Users
                .Include(x => x.Roles)
                .Where(x =>
                    (x.Roles.Any() == viewmodel.AlleenBeheerders || !viewmodel.AlleenBeheerders)
                    && (x.Naam.Contains(viewmodel.Zoekterm) || viewmodel.Zoekterm == null || viewmodel.Zoekterm == "")
                    )
                .OrderByDescending(g => g.Roles.Count)
                        .ThenBy(g => g.LaatstIngelogd)
                        .ThenBy(g => g.Naam)
                        .ToList();
            return View(viewmodel);
        }

        [System.Web.Mvc.Authorize(Roles = "Gebruikers")]
        public async Task<ActionResult> Verwijder(int id)
        {
            var gebruiker = await _prekenWebUserManager.FindByIdAsync(id);

            gebruiker.Mailings.ToList().ForEach(m => gebruiker.Mailings.Remove(m));
            gebruiker.Roles.ToList().ForEach(r => gebruiker.Roles.Remove(r));

            var result = await _prekenWebUserManager.DeleteAsync(gebruiker);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(Environment.NewLine, result.Errors));
            }

            return RedirectToAction("Index");
        }

        [System.Web.Mvc.Authorize]
        public async Task<ActionResult> Bewerk(int id)
        {
            // jezelf aanpassen of de rol hebben om anderen aan te passen
            if (id != await _huidigeGebruiker.GetId(_prekenWebUserManager, User) && !User.IsInRole("Gebruikers")) throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));

            var gebruiker = await _prekenWebUserManager.FindByIdAsync(id);

            return View(new GebruikerEditViewModel
            {
                Gebruiker = gebruiker,
                Wachtwoord = string.Empty,
                WachtwoordCheck = string.Empty,
                SelectedRoles = (await _prekenWebUserManager.GetRolesAsync(gebruiker.Id)).ToArray()
            });
        }

        [System.Web.Mvc.HttpPost, System.Web.Mvc.Authorize]
        public async Task<ActionResult> Bewerk(GebruikerEditViewModel viewModel)
        {
            // jezelf aanpassen of de rol hebben om anderen aan te passen
            if (viewModel.Gebruiker.Id != await _huidigeGebruiker.GetId(_prekenWebUserManager, User) && !User.IsInRole("Gebruikers")) throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));

            if (!viewModel.WachtwoordAanpassen)
            {
                ModelState.Remove("WachtwoordCheck");
                ModelState.Remove("Wachtwoord");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(viewModel.Wachtwoord)) ModelState.AddModelError("Wachtwoord", Resources.Resources.VeldVerplicht);
                if (viewModel.WachtwoordCheck != viewModel.Wachtwoord) ModelState.AddModelError("WachtwoordCheck", Resources.Resources.TweeVerschillendeWachtwoord);
            }

            if (!ModelState.IsValid) return View(viewModel);

            var userToUpdate = await _prekenWebUserManager.FindByIdAsync(viewModel.Gebruiker.Id);
            if (viewModel.WachtwoordAanpassen)
            {
                IdentityResult wachtwoordAanpassenResultaat;
                if (string.IsNullOrEmpty(userToUpdate.PasswordHash))
                {
                    wachtwoordAanpassenResultaat = await _prekenWebUserManager.AddPasswordAsync(viewModel.Gebruiker.Id, viewModel.Wachtwoord);
                }
                else
                {
                    wachtwoordAanpassenResultaat = await _prekenWebUserManager.ChangePasswordAsync(viewModel.Gebruiker.Id, viewModel.HuidigWachtwoord, viewModel.Wachtwoord);
                }
                if (!wachtwoordAanpassenResultaat.Succeeded)
                {
                    AddIdentityResultErrorsToModelState(wachtwoordAanpassenResultaat);
                    return View(viewModel);
                }
                viewModel.WachtwoordAanpassen = false;
            }

            userToUpdate.Naam = viewModel.Gebruiker.Naam;
            userToUpdate.Email = viewModel.Gebruiker.Email;
            // other editable values

            var result = await _prekenWebUserManager.UpdateAsync(userToUpdate);
            if (!result.Succeeded)
            {
                AddIdentityResultErrorsToModelState(result);
                return View(viewModel);
            }

            viewModel.WachtwoordAanpassen = false;
            viewModel.Wachtwoord = string.Empty;
            viewModel.WachtwoordCheck = string.Empty;

            if (User.IsInRole("Gebruikers"))
            {
                //// remove all roles
                //foreach (var role in await _prekenWebUserManager.GetRolesAsync(viewModel.Gebruiker.Id))
                //{
                //    await _prekenWebUserManager.RemoveFromRoleAsync(viewModel.Gebruiker.Id, role);
                //}

                //// add selected roles
                //if (viewModel.SelectedRoles != null)
                //    foreach (var role in viewModel.SelectedRoles.Where(x => !string.IsNullOrEmpty(x)))
                //    {
                //        await _prekenWebUserManager.AddToRoleAsync(viewModel.Gebruiker.Id, role);
                //    }

                //todo: Geen idee hoe boventaande werkend te krijgen, vooralsnog maar onderstaande twee regels
                await _gebruikerRepository.RemoveAllRolesAsync(viewModel.Gebruiker.Id);
                await _gebruikerRepository.AddToRolesAsync(viewModel.Gebruiker.Id, viewModel.SelectedRoles.Where(x => !string.IsNullOrEmpty(x)).ToArray());

            }

            try
            {
                var mcListIdsWasSubscribedTo = viewModel.Gebruiker.Mailings.Select(x => x.MailChimpId).ToList();
                var mcListIdsIsNowSubscribedTo = (await _mailingRepository.GetAlleMailings()).Where(m => viewModel.SelectedNieuwsbrieven.Contains(m.Id)).Select(m => m.MailChimpId).ToList();

                await _mailingRepository.NieuwsbrievenOverschrijvenBijGebruiker(viewModel.Gebruiker.Id, viewModel.SelectedNieuwsbrieven);
                viewModel.SelectedNieuwsbrieven = null;

                MailChimpController.UnSubscribe(viewModel.Gebruiker.Email, viewModel.Gebruiker.Naam, mcListIdsWasSubscribedTo.Where(x => !mcListIdsIsNowSubscribedTo.Contains(x)).ToList());
                MailChimpController.Subscribe(viewModel.Gebruiker.Email, viewModel.Gebruiker.Naam, mcListIdsIsNowSubscribedTo.Where(x => !mcListIdsWasSubscribedTo.Contains(x)).ToList());
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("SelectedNieuwsbrieven", string.Format("Your profile changes are saved but there was an error (un)subscribing to mailing(s): {0}", ex.Message));
                return View(viewModel);
            }
            return View(viewModel);
        }

        [System.Web.Mvc.Authorize(Roles = "Gebruikers")]
        public ActionResult Maak()
        {
            return View(new GebruikerEditViewModel
            {
                WachtwoordAanpassen = true,
                Gebruiker = new Gebruiker(),
                Wachtwoord = string.Empty,
                WachtwoordCheck = string.Empty,
                SelectedRoles = new string[0]
            });
        }

        [System.Web.Mvc.HttpPost, System.Web.Mvc.Authorize(Roles = "Gebruikers")]
        public async Task<ActionResult> Maak(GebruikerEditViewModel viewModel)
        {
            if (viewModel.WachtwoordCheck != viewModel.Wachtwoord) ModelState.AddModelError("WachtwoordCheck", Resources.Resources.TweeVerschillendeWachtwoord);
            if (await _prekenWebUserManager.FindByEmailAsync(viewModel.Gebruiker.Email) != null) ModelState.AddModelError("Gebruiker.Email", Resources.Resources.EmailNietBeschikbaar);
            if (await _prekenWebUserManager.FindByNameAsync(viewModel.Gebruiker.UserName) != null) ModelState.AddModelError("Gebruiker.UserName", Resources.Resources.GebruikersnaamNietBeschikbaar);

            if (!ModelState.IsValid) return View(viewModel);

            var nieuweGebruiker = new Gebruiker
            {
                UserName = viewModel.Gebruiker.UserName,
                Naam = viewModel.Gebruiker.Naam,
                Email = viewModel.Gebruiker.Email,
                LaatstIngelogd = DateTime.Now,
                EmailConfirmed = true
            };
            var result = await _prekenWebUserManager.CreateAsync(nieuweGebruiker, viewModel.Wachtwoord);
            if (!result.Succeeded)
            {
                AddIdentityResultErrorsToModelState(result);
                return View(viewModel);
            }
            //if (viewModel.SelectedRoles != null)
            //    foreach (var role in viewModel.SelectedRoles)
            //    {
            //        await _prekenWebUserManager.AddToRoleAsync(nieuweGebruiker.Id, role);
            //    }
            //todo: Geen idee hoe boventaande werkend te krijgen (zorgt nl. voor een null value in de kolom gebruiker_id), vooralsnog maar onderstaande regel
            await _gebruikerRepository.AddToRolesAsync(nieuweGebruiker.Id, viewModel.SelectedRoles.Where(x => !string.IsNullOrEmpty(x)).ToArray());

            viewModel.SelectedRoles = (await _prekenWebUserManager.GetRolesAsync(nieuweGebruiker.Id)).ToArray();

            try
            {
                var mcListIdsIsNowSubscribedTo = (await _mailingRepository.GetAlleMailings()).Where(m => viewModel.SelectedNieuwsbrieven.Contains(m.Id)).Select(m => m.MailChimpId).ToList();
                await _mailingRepository.NieuwsbrievenOverschrijvenBijGebruiker(nieuweGebruiker.Id, viewModel.SelectedNieuwsbrieven);
                MailChimpController.Subscribe(nieuweGebruiker.Email, nieuweGebruiker.Naam, mcListIdsIsNowSubscribedTo);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Aanmaken gebruiker, kon niet inschrijven bij nieuwsbrief, ex: {0}", ex.Message));
            }

            return RedirectToAction("Bewerk", new { nieuweGebruiker.Id });
        }
        #endregion

        #region Wachtwoord vergeten
        [System.Web.Mvc.AllowAnonymous]
        public ActionResult WachtwoordVergeten(string gebruikersnaam)
        {
            var viewmodel = new WachtwoordVergeten
            {
                TekstPagina = _tekstRepository.GetTekstPagina("WachtwoordVergeten", TaalInfoHelper.FromRouteData(RouteData).Id),
                Gebruikersnaam = gebruikersnaam
            };
            return View(viewmodel);
        }

        [System.Web.Mvc.HttpPost, ValidateAntiForgeryToken, System.Web.Mvc.AllowAnonymous]
        public async Task<ActionResult> WachtwoordVergeten(WachtwoordVergeten viewModel)
        {
            viewModel.TekstPagina = _tekstRepository.GetTekstPagina("WachtwoordVergeten", TaalInfoHelper.FromRouteData(RouteData).Id);

            var gebruiker = await _prekenWebUserManager.FindByNameAsync(viewModel.Gebruikersnaam) ??
                            await _prekenWebUserManager.FindByEmailAsync(viewModel.Gebruikersnaam);

            if (gebruiker == null) ModelState.AddModelError("Gebruikersnaam", Resources.Resources.GebruikersnaamEmailOnbekend);
            else if (!(await _prekenWebUserManager.IsEmailConfirmedAsync(gebruiker.Id))) ModelState.AddModelError("Gebruikersnaam", Resources.Resources.EmailOnbevestigd);

            if (!ModelState.IsValid || gebruiker == null || Request.Url == null) return View(viewModel);

            var passwordResetToken = await _prekenWebUserManager.GeneratePasswordResetTokenAsync(gebruiker.Id);
            var callbackUrl = Url.Action("ResetWachtwoord", "Gebruiker", new { userId = gebruiker.Id, code = passwordResetToken }, Request.Url.Scheme);

            var mailTemplating = new MailTemplating(_gebruikerRepository, _prekenWebUserManager);
            var mailbody = await mailTemplating.GetWachtwoordVergetenMailBody(gebruiker.Id, Resources.Resources.WachtwoordVergetenEmailOnderwerp, callbackUrl);
            await _prekenWebUserManager.SendEmailAsync(gebruiker.Id, Resources.Resources.WachtwoordVergetenEmailOnderwerp, mailbody);
            return RedirectToAction("WachtwoordVergetenBevestiging");
        }


        [System.Web.Mvc.AllowAnonymous]
        public ActionResult WachtwoordVergetenBevestiging()
        {
            return View(_tekstRepository.GetTekstPagina("WachtwoordVergetenBevestiging", TaalInfoHelper.FromRouteData(RouteData).Id));
        }

        [System.Web.Mvc.AllowAnonymous]
        public ActionResult ResetWachtwoord(string code)
        {
            if (code == null) throw new ArgumentNullException("code");

            return View(new ResetWachtwoordViewModel
            {
                TekstPagina = _tekstRepository.GetTekstPagina("ResetWachtwoord", TaalInfoHelper.FromRouteData(RouteData).Id)
            });
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetWachtwoord(ResetWachtwoordViewModel viewModel)
        {
            viewModel.TekstPagina = _tekstRepository.GetTekstPagina("ResetWachtwoord", TaalInfoHelper.FromRouteData(RouteData).Id);

            var gebruiker = await _prekenWebUserManager.FindByNameAsync(viewModel.Gebruikersnaam) ??
                            await _prekenWebUserManager.FindByEmailAsync(viewModel.Gebruikersnaam);

            if (gebruiker == null) ModelState.AddModelError("Gebruikersnaam", Resources.Resources.GebruikersnaamEmailOnbekend);

            if (!ModelState.IsValid || gebruiker == null) return View(viewModel);

            var identityResetPasswordResult = await _prekenWebUserManager.ResetPasswordAsync(gebruiker.Id, viewModel.Code, viewModel.Wachtwoord);

            if (identityResetPasswordResult.Succeeded)
            {
                return RedirectToAction("ResetWachtwoordBevestiging", "Gebruiker");
            }
            AddIdentityResultErrorsToModelState(identityResetPasswordResult);

            return View(viewModel);
        }

        [System.Web.Mvc.AllowAnonymous]
        public ActionResult ResetWachtwoordBevestiging()
        {
            return View(_tekstRepository.GetTekstPagina("ResetWachtwoordBevestiging", TaalInfoHelper.FromRouteData(RouteData).Id));
        }

        #endregion
    }
}
