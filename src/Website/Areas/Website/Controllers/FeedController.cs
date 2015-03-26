using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using Prekenweb.Attributes;
using Prekenweb.Models.Repository;
using Prekenweb.Website.Controllers;
using Prekenweb.Website.HtmlHelpers;
using Prekenweb.Website.Lib;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Xml;
using Prekenweb.Models.Services;
using Prekenweb.Website.Properties;

namespace Prekenweb.Website.Areas.Website.Controllers
{
    public class FeedController : ApplicationController
    {
        private readonly IPrekenRepository _prekenRepository;
        private readonly IZoekenRepository _zoekenRepository;
        private readonly IGebruikerRepository _gebruikerRepository;

        public FeedController(IPrekenRepository prekenRepository,
                              IZoekenRepository zoekenRepository,
                              IGebruikerRepository gebruikerRepository)
        {
            _prekenRepository = prekenRepository;
            _zoekenRepository = zoekenRepository;
            _gebruikerRepository = gebruikerRepository;

        }

        [OutputCache(Duration = 3600), Throttle(Seconds = 10)] // 1 uur
        public async Task<ActionResult> Twitter()
        {
            //TODO: Replace with TwitterService NuGet Package implementation

            var key = ConfigurationManager.AppSettings["TwitterCustomerKey"];
            var secret = ConfigurationManager.AppSettings["TwitterCustomerSecret"];

            var server = HttpContext.Server;
            var bearerToken = server.UrlEncode(key) + ":" + server.UrlEncode(secret);
            var b64Bearer = Convert.ToBase64String(Encoding.Default.GetBytes(bearerToken));
            using (var webClient = new WebClient())
            {
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
                webClient.Headers.Add("Authorization", "Basic " + b64Bearer);
                var tokenPayload = webClient.UploadString("https://api.twitter.com/oauth2/token", "grant_type=client_credentials");
                var rgx = new Regex("\"access_token\"\\s*:\\s*\"([^\"]*)\"");
                var accessToken = rgx.Match(tokenPayload).Groups[1].Value;
                webClient.Headers.Clear();
                webClient.Headers.Add("Authorization", "Bearer " + accessToken);

                const string url = "https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name=Prekenweb&count=4&exclude_replies=true&include_rts=false";
                var jsonSerializer = new JavaScriptSerializer();
                var data = await webClient.DownloadStringTaskAsync(url);
                dynamic tweets = jsonSerializer.DeserializeObject(data);
                return Json(tweets, JsonRequestBehavior.AllowGet);
            }
        }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times"), OutputCache(Duration = 86400)] // 24 uur
        // ReSharper disable once InconsistentNaming
        public async Task<ActionResult> ITunesPodcast()
        {
            var siteTitle = Resources.Resources.PrekenWebNL;
            var siteDescription = TaalId == 1 ? "Preken uit de Gereformeerde Gemeenten" : "This website offers audio and reading sermons from ministers of the 'Reformed Congregations' - denomination ('Gereformeerde Gemeenten') from different countries: the Netherlands, Canada, United States, Nigeria and New Zealand.";
            const string siteUrl = "http://www.prekenweb.nl";
            var authorName = Resources.Resources.PrekenWebNL;
            const string authorEmail = "info@prekenweb.nl";

            var settings = new XmlWriterSettings { Encoding = Encoding.UTF8 };
            using (var sw = new StringWriterWithEncoding(Encoding.UTF8))
            using (var writer = XmlWriter.Create(sw, settings))
            {
                const string itunesUri = "http://www.itunes.com/dtds/podcast-1.0.dtd";
                //string atomUri = "http://www.w3.org/2005/Atom";

                // Start document
                writer.WriteStartDocument();

                // Start rss
                writer.WriteStartElement("rss");
                //writer.WriteAttributeString("xmlns", "atom", null, atomUri);
                writer.WriteAttributeString("xmlns", "itunes", string.Empty, itunesUri);
                writer.WriteAttributeString("version", "2.0");


                // Start channel
                writer.WriteStartElement("channel");

                //writer.WriteStartElement("link", atomUri);
                //writer.WriteAttributeString("href", Url.ContentAbsolute(Url.Action("iTunesPodcast", "Home")));
                //writer.WriteAttributeString("rel", "self");
                //writer.WriteAttributeString("type", "application/rss+xml");
                //writer.WriteEndElement();

                writer.WriteElementString("title", siteTitle);
                writer.WriteElementString("description", siteDescription);
                writer.WriteElementString("link", siteUrl);
                writer.WriteElementString("language", TaalCode);
                writer.WriteElementString("copyright", HttpUtility.HtmlEncode(string.Format("Copyright {0}, {1}", DateTime.UtcNow.Year, authorName)));
                writer.WriteElementString("lastBuildDate", DateTime.UtcNow.ToString("r"));
                writer.WriteElementString("pubDate", DateTime.UtcNow.ToString("r"));
                writer.WriteElementString("webMaster", authorEmail);
                writer.WriteElementString("ttl", "60");

                //// Image 1
                writer.WriteStartElement("image", itunesUri);
                writer.WriteElementString("href", "http://www.prekenweb.nl/content/images/logo1400.jpg");
                writer.WriteEndElement();

                //// Image 2
                writer.WriteStartElement("image");
                writer.WriteElementString("url", "http://www.prekenweb.nl/content/images/logo1400.jpg");
                writer.WriteElementString("title", Resources.Resources.PrekenWebNL);
                writer.WriteElementString("link", "http://www.prekenweb.nl/");
                writer.WriteEndElement();

                //<image>
                //    <url>http://assets.libsyn.com/content/5834740.jpg</url>
                //    <title>Defining Words</title>
                //    <link>
                //        <![CDATA[http://definingwords.libsyn.com]]>
                //    </link>
                //</image>

                // Categories
                //writer.WriteElementString("Category", "Christianity"); 

                writer.WriteElementString("author", itunesUri, authorName);
                writer.WriteElementString("subtitle", itunesUri, siteDescription);
                writer.WriteElementString("summary", itunesUri, siteDescription);

                // Start itunes:owner
                writer.WriteStartElement("owner", itunesUri);

                writer.WriteElementString("name", itunesUri, authorName);
                writer.WriteElementString("email", itunesUri, authorEmail);

                // End  itunes:owner
                writer.WriteEndElement();

                writer.WriteElementString("explicit", itunesUri, "No");



                // First category
                // Start itunes:category

                //writer.WriteStartElement("category", itunesUri);
                //writer.WriteAttributeString("text", "Comedy");
                writer.WriteStartElement("category", itunesUri);
                writer.WriteAttributeString("text", "Religion & Spirituality");

                //Start itunes:category
                writer.WriteStartElement("category", itunesUri);
                writer.WriteAttributeString("text", "Christianity");
                //End itunes:category
                writer.WriteEndElement();

                // End itunes:category
                writer.WriteEndElement();

                foreach (var preek in await _prekenRepository.GetPrekenForItunesPodcast(TaalId))
                {

                    // Start podcast item
                    writer.WriteStartElement("item");

                    var showTitle = preek.GetPreekTitel();
                    showTitle = showTitle.Length > 255 ? showTitle.Substring(0, 255) : showTitle;
                    var showDescription = Regex.Replace(preek.GetPreekOmschrijvingITunes(), "<[^>]*(>|$)", string.Empty);
                    showDescription = showDescription.Length > 3950 ? showDescription.Substring(0, 3950) : showDescription;
                    showDescription = showDescription.Replace("  ", " ");

                    //showDescription = "";
                    writer.WriteElementString("title", showTitle);
                    writer.WriteElementString("link", Url.ContentAbsolute(Url.Action("Open", "Preek", new { preek.Id })));
                    writer.WriteElementString("guid", Url.ContentAbsolute(Url.Action("Open", "Preek", new { preek.Id })));
                    writer.WriteElementString("description", showDescription);

                    // Start enclosure 
                    writer.WriteStartElement("enclosure");

                    writer.WriteAttributeString("url", Url.ContentAbsolute(string.Format("~/Content/preken/{0}", preek.Bestandsnaam)));
                    writer.WriteAttributeString("length", preek.Bestandsgrootte.ToString());

                    writer.WriteAttributeString("type", preek.GetContentType());

                    // End enclosure
                    writer.WriteEndElement();
                    writer.WriteElementString("category", Resources.Resources.PrekenWeb);
                    if (preek.DatumAangemaakt.HasValue) writer.WriteElementString("pubDate", preek.DatumAangemaakt.Value.ToString("r"));
                    else if (preek.DatumPreek.HasValue) writer.WriteElementString("pubDate", preek.DatumPreek.Value.ToString("r"));
                    else writer.WriteElementString("pubDate", DateTime.Now.ToString("r"));

                    writer.WriteElementString("author", itunesUri, preek.PredikantId.HasValue ? preek.Predikant.VolledigeNaam : "PrekenWeb.nl");
                    writer.WriteElementString("explicit", itunesUri, "No");
                    writer.WriteElementString("subtitle", itunesUri, showTitle);
                    writer.WriteElementString("summary", itunesUri, showDescription);

                    if (preek.Duur != null)
                        writer.WriteElementString("duration", itunesUri, string.Format("{0:hh\\:mm\\:ss}", preek.Duur.Value));
                    writer.WriteElementString("keywords", itunesUri, Resources.Resources.PrekenWeb);

                    // Start itunes:image
                    writer.WriteStartElement("image", itunesUri);
                    writer.WriteElementString("href", "http://www.prekenweb.nl/content/images/logo1400.jpg");
                    writer.WriteEndElement();
                    //// End itunes:image

                    // End podcast item
                    writer.WriteEndElement();
                }

                // End channel
                writer.WriteEndElement();

                // End rss
                writer.WriteEndElement();

                // End document
                writer.WriteEndDocument();

                writer.Flush();
                var xml = sw.ToString();
                return Content(xml, "application/xml");
            }
        }

        [OutputCache(Duration = 3600, VaryByParam = "*", Location = OutputCacheLocation.Server)] // 1 uur
        public async Task<ActionResult> RssFeed(Guid id)
        {
            var zoekOpdracht = await _zoekenRepository.GetZoekopdrachtById(id);
            if (zoekOpdracht == null) return HttpNotFound();

            var siteTitle = Resources.Resources.PrekenWebNL;
            var siteDescription = TaalId == 1 ? "Preken uit de Gereformeerde Gemeenten" : "This website offers audio and reading sermons from ministers of the 'Reformed Congregations' - denomination ('Gereformeerde Gemeenten') from different countries: the Netherlands, Canada, United States, Nigeria and New Zealand.";
            var siteUrl = "http://www.prekenweb.nl";
            var authorName = Resources.Resources.PrekenWebNL;
            var authorEmail = "info@prekenweb.nl";

            var settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            using (var sw = new StringWriterWithEncoding(Encoding.UTF8))
            using (var writer = XmlWriter.Create(sw, settings))
            {
                writer.WriteStartDocument();

                writer.WriteStartElement("rss");
                writer.WriteAttributeString("version", "2.0");
                writer.WriteStartElement("channel");

                writer.WriteElementString("title", siteTitle);
                writer.WriteElementString("description", siteDescription);
                writer.WriteElementString("link", siteUrl);
                writer.WriteElementString("language", TaalCode);
                writer.WriteElementString("copyright", HttpUtility.HtmlEncode(string.Format("Copyright {0}, {1}", DateTime.UtcNow.Year, authorName)));
                writer.WriteElementString("lastBuildDate", DateTime.UtcNow.ToString("r"));
                writer.WriteElementString("pubDate", DateTime.UtcNow.ToString("r"));
                writer.WriteElementString("webMaster", authorEmail);
                writer.WriteElementString("ttl", "60");

                //// Image 2
                writer.WriteStartElement("image");
                writer.WriteElementString("url", "http://www.prekenweb.nl/content/images/logo1400.jpg");
                writer.WriteElementString("title", Resources.Resources.PrekenWebNL);
                writer.WriteElementString("link", "http://www.prekenweb.nl/");
                writer.WriteEndElement();

                var zoekService = new ZoekService(_zoekenRepository, _gebruikerRepository);
                var zoekResultaat = await zoekService.ZoekOpdrachtUitvoeren(zoekOpdracht);

                foreach (var zoekResultaatItem in zoekResultaat.Items.Where(x => x.Preek.Bestandsnaam != null))
                {

                    writer.WriteStartElement("item"); // Start podcast item

                    var showTitle = zoekResultaatItem.Preek.GetPreekTitel();
                    if (zoekResultaatItem.Preek.PreekTypeId != 1) // audiopreek
                        showTitle += string.Format(" ({0})", zoekResultaatItem.Preek.PreekType.Omschrijving).ToLower();
                    showTitle = showTitle.Length > 255 ? showTitle.Substring(0, 255) : showTitle;
                    var showDescription = Regex.Replace(zoekResultaatItem.Preek.GetPreekOmschrijvingITunes(), "<[^>]*(>|$)", string.Empty);
                    showDescription = showDescription.Length > 3950 ? showDescription.Substring(0, 3950) : showDescription;
                    showDescription = showDescription.Replace("  ", " ");

                    writer.WriteElementString("title", showTitle);
                    writer.WriteElementString("link", Url.ContentAbsolute(Url.Action("Open", "Preek", new { zoekResultaatItem.Preek.Id })));
                    writer.WriteElementString("guid", Url.ContentAbsolute(Url.Action("Open", "Preek", new { zoekResultaatItem.Preek.Id })));
                    writer.WriteElementString("description", showDescription);

                    writer.WriteStartElement("enclosure");// Start enclosure 
                    writer.WriteAttributeString("url", Url.ContentAbsolute(string.Format("~/Content/preken/{0}", zoekResultaatItem.Preek.Bestandsnaam)));
                    writer.WriteAttributeString("length", zoekResultaatItem.Preek.Bestandsgrootte.ToString());
                    writer.WriteAttributeString("type", zoekResultaatItem.Preek.GetContentType());
                    writer.WriteEndElement(); // End enclosure

                    writer.WriteElementString("category", Resources.Resources.PrekenWeb);
                    if (zoekResultaatItem.Preek.DatumAangemaakt.HasValue) writer.WriteElementString("pubDate", zoekResultaatItem.Preek.DatumAangemaakt.Value.ToString("r"));
                    else if (zoekResultaatItem.Preek.DatumPreek.HasValue) writer.WriteElementString("pubDate", zoekResultaatItem.Preek.DatumPreek.Value.ToString("r"));
                    else writer.WriteElementString("pubDate", DateTime.Now.ToString("r"));

                    writer.WriteEndElement(); // End podcast item
                }

                // End channel
                writer.WriteEndElement();

                // End rss
                writer.WriteEndElement();

                // End document
                writer.WriteEndDocument();

                writer.Flush();
                var xml = sw.ToString();
                return Content(xml, "application/xml");
            }
        }
    }
}
