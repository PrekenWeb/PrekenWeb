using System.Configuration;
using MailChimp;
using MailChimp.Campaigns;
using MailChimp.Errors;
using MailChimp.Helper;
using PrekenWeb.Data.Repositories;
using MailChimp.Lists;
using PrekenWeb.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Web.Mvc;
using Prekenweb.Website.Areas.Website.Models;

namespace Prekenweb.Website.Areas.Website.Controllers
{
    public class MailChimpController : Controller
    {
        private readonly IMailingRepository _mailingRepository;
        private readonly IPrekenWebUserManager _prekenWebUserManager;

        public MailChimpController(IMailingRepository mailingRepository, 
                                   IPrekenWebUserManager _prekenWebUserManager)
        {
            _mailingRepository = mailingRepository;
            this._prekenWebUserManager = _prekenWebUserManager;
        }

        public async Task<ActionResult> WebHook(MailChimpPost mailChimpPost)
        {
            const string logfile = @"C:\inetpub\wwwroot\Productie\test.txt";
            //string logfile = @"C:\Projecten - prive\Prekenweb\Prekenweb\Website\test.txt";
            if (Request.Form == null && mailChimpPost == null) return null;
            if (Request.Form != null)
                Request.Form.AllKeys.ToList().ForEach(x =>
                {
                    System.IO.File.AppendAllText(logfile, string.Format(" > {0}:{1}", x, Request.Form[x]));
                    System.IO.File.AppendAllText(logfile, "\r\n");

                });
            if (mailChimpPost == null) return null;
            
            System.IO.File.AppendAllText(logfile, mailChimpPost.type);
            System.IO.File.AppendAllText(logfile, "\r\n");
            System.IO.File.AppendAllText(logfile, string.Format("{0}", mailChimpPost.fired_at));
            System.IO.File.AppendAllText(logfile, "\r\n");
            
            if (mailChimpPost.data != null)
            {
                System.IO.File.AppendAllText(logfile, mailChimpPost.data[MailChimpDataFields.email.ToString()]);
                System.IO.File.AppendAllText(logfile, "\r\n");
                System.IO.File.AppendAllText(logfile, mailChimpPost.data[MailChimpDataFields.list_id.ToString()]);
                System.IO.File.AppendAllText(logfile, "\r\n");
            }

            if (mailChimpPost.type == "unsubscribe")
            {
                var email = mailChimpPost.data[MailChimpDataFields.email.ToString()];
                var gebruiker = await _prekenWebUserManager.FindByEmailAsync(email);
                if (gebruiker == null) return null;

                System.IO.File.AppendAllText(logfile, "Gebruiker gevonden \r\n");
                var listId = mailChimpPost.data[MailChimpDataFields.list_id.ToString()];
                var mailing = (await _mailingRepository.GetAlleMailings()).FirstOrDefault(x => x.MailChimpId == listId);
                System.IO.File.AppendAllText(logfile, "Mailing gevonden\r\n");
                if (mailing != null) await _mailingRepository.NieuwsbriefOntkoppelen(gebruiker.Id, mailing.Id);
                System.IO.File.AppendAllText(logfile, "Opgeslagen\r\n");
            }

            if (mailChimpPost.type == "subscribe")
            {
                var email = mailChimpPost.data[MailChimpDataFields.email.ToString()];
                var gebruiker = await _prekenWebUserManager.FindByEmailAsync(email);
                if (gebruiker == null) return null;

                System.IO.File.AppendAllText(logfile, "Gebruiker gevonden \r\n");
                var listId = mailChimpPost.data[MailChimpDataFields.list_id.ToString()];
                var mailing = (await _mailingRepository.GetAlleMailings()).FirstOrDefault(x => x.MailChimpId == listId);
                System.IO.File.AppendAllText(logfile, "Mailing gevonden\r\n");
                if (mailing != null) await _mailingRepository.NieuwsbriefToevoegenAanGebruiker(gebruiker.Id, mailing.Id);
                System.IO.File.AppendAllText(logfile, "Opgeslagen\r\n");
            }
            return null;
        }

        public static void UnSubscribe(string email, string naam, string listId)
        {
            var mc = new MailChimpManager(ConfigurationManager.AppSettings["MailChimpAPIKey"]);
            mc.Unsubscribe(listId, new EmailParameter { Email = email }, false, false, false);
        }

        public static void UnSubscribe(string email, string naam, List<string> listIds)
        {
            foreach (var listId in listIds) UnSubscribe(email, naam, listId);
        }

        public static void Subscribe(string email, string naam, string listId)
        { 
            var naamMergeVar = new NaamMergeVar {NAAM = naam};

            var mc = new MailChimpManager(ConfigurationManager.AppSettings["MailChimpAPIKey"]);
            try
            {
                mc.Subscribe(listId,
                    new EmailParameter {Email = email},
                    mergeVars: naamMergeVar,
                    updateExisting: false,
                    sendWelcome: false,
                    doubleOptIn: false);
            } 
            catch (MailChimpAPIException ex)
            {
                if (ex.MailChimpAPIError.Name != "List_AlreadySubscribed") throw; // allready subscribed is ok
            }
        }

        public static List<MailChimpNieuwsbrief> GetSendCampains(string listId)
        {
            var mc = new MailChimpManager(ConfigurationManager.AppSettings["MailChimpAPIKey"]);

            var campaigns = mc.GetCampaigns(new CampaignFilter { ListId = listId }, limit: 4);
            return campaigns.Data.Select(x => new MailChimpNieuwsbrief
            {
                ArchiveUrl = new Uri(x.ArchiveUrl), Datum = DateTime.Parse(x.SendTime), Titel = x.Title
            }).ToList();
        }

        public static void Subscribe(string email, string naam, List<string> listIds)
        {
            foreach (var listId in listIds) Subscribe(email, naam, listId);

        }

        public class MailChimpPost
        {
            public string type { get; set; }
            public DateTime fired_at { get; set; }
            public Dictionary<string, string> data { get; private set; }
        }
        public enum MailChimpDataFields
        {
            action,
            reason,
            id,
            list_id,
            email,
            email_type,
            mergesEMAIL,
            mergesFNAME,
            mergesLNAME,
            mergesINTERESTS,
            ip_opt,
            campaign_id,
        }

        [DataContract]
        public class NaamMergeVar : MergeVar
        {
            [DataMember(Name = "NAAM")]
            public string NAAM { get; set; }
        }

    }
}
