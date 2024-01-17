using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using Prekenweb.Website.Lib;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml;
using Data.Repositories;
using Data.Services;
using Prekenweb.Website.Lib.HtmlHelpers;
using Newtonsoft.Json;
using TweetSharp;

namespace Prekenweb.Website.Areas.Website.Controllers
{
    public class FeedController : Controller
    {
        private readonly IPrekenRepository _prekenRepository;
        private readonly IZoekenRepository _zoekenRepository;

        public FeedController(IPrekenRepository prekenRepository,
                              IZoekenRepository zoekenRepository)
        {
            _prekenRepository = prekenRepository;
            _zoekenRepository = zoekenRepository;

        }

        [OutputCache(Duration = 3600)] // 1 uur
        public ActionResult Twitter()
        {
            var customerKey = ConfigurationManager.AppSettings["TwitterCustomerKey"];
            var customerSecret = ConfigurationManager.AppSettings["TwitterCustomerSecret"];
            var token = ConfigurationManager.AppSettings["TwitterToken"];
            var tokenSecret = ConfigurationManager.AppSettings["TwitterTokenSecret"];

            // Fix om de connectie met twitter via TLS1.1 of TLS1.2 te laten lopen, via SSL3 gaat het niet goed.
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            var service = new TwitterService(customerKey, customerSecret, token, tokenSecret);
            var tweets = service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions { Count = 4 });

            return Json(tweets?.Select(ReplaceShortUrls), JsonRequestBehavior.AllowGet);
        }

        private string ReplaceShortUrls(TwitterStatus tweet)
        {
            var result = tweet.TextAsHtml;
            foreach (var url in tweet.Entities.Urls)
            {
                result = result.Replace(url.Value, url.ExpandedValue);
            }
            return result;
        }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times"), OutputCache(Duration = 86400)] // 24 uur
        // ReSharper disable once InconsistentNaming
        public async Task<ActionResult> ITunesPodcast()
        {
            var siteTitle = Resources.Resources.PrekenWebNL;
            var siteDescription = TaalInfoHelper.FromRouteData(RouteData).Id == 1 ? "Preken uit de Gereformeerde Gemeenten" : "This website offers audio and reading sermons from ministers of the 'Reformed Congregations' - denomination ('Gereformeerde Gemeenten') from different countries: the Netherlands, Canada, United States, Nigeria and New Zealand.";
            const string siteUrl = "https://www.prekenweb.nl";
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
                var taalInfo = TaalInfoHelper.FromRouteData(RouteData);
                writer.WriteElementString("language", taalInfo.CultureInfo.TextInfo.CultureName);
                writer.WriteElementString("copyright", HttpUtility.HtmlEncode(string.Format("Copyright {0}, {1}", DateTime.UtcNow.Year, authorName)));
                writer.WriteElementString("lastBuildDate", DateTime.UtcNow.ToString("r"));
                writer.WriteElementString("pubDate", DateTime.UtcNow.ToString("r"));
                writer.WriteElementString("webMaster", authorEmail);
                writer.WriteElementString("ttl", "60");

                //// Image 1
                writer.WriteStartElement("image", itunesUri);
                writer.WriteElementString("href", "https://www.prekenweb.nl/content/images/logo1400.jpg");
                writer.WriteEndElement();

                //// Image 2
                writer.WriteStartElement("image");
                writer.WriteElementString("url", "https://www.prekenweb.nl/content/images/logo1400.jpg");
                writer.WriteElementString("title", Resources.Resources.PrekenWebNL);
                writer.WriteElementString("link", "https://www.prekenweb.nl/");
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

                foreach (var preek in await _prekenRepository.GetPrekenForItunesPodcast(TaalInfoHelper.FromRouteData(RouteData).Id))
                {
                    var minister = preek.PredikantId.HasValue ? preek.Predikant : null;
                    if (minister != null && minister.HideFromPodcast)
                        continue;

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

                    writer.WriteAttributeString("url", Url.ContentAbsolute(string.Format("{0}/{1}/{2}", ConfigurationManager.AppSettings["AzureBlobStorageBaseUrl"], ConfigurationManager.AppSettings["PrekenFolder"], preek.Bestandsnaam)));
                    writer.WriteAttributeString("length", preek.Bestandsgrootte.ToString());

                    writer.WriteAttributeString("type", preek.GetContentType());

                    // End enclosure
                    writer.WriteEndElement();
                    writer.WriteElementString("category", Resources.Resources.PrekenWeb);
                    if(preek.DatumGepubliceerd.HasValue) writer.WriteElementString("pubDate", preek.DatumGepubliceerd.Value.ToString("r"));
                    else if (preek.DatumAangemaakt.HasValue) writer.WriteElementString("pubDate", preek.DatumAangemaakt.Value.ToString("r"));
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
                    writer.WriteElementString("href", "https://www.prekenweb.nl/content/images/logo1400.jpg");
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
            var siteDescription = TaalInfoHelper.FromRouteData(RouteData).Id == 1 ? "Preken uit de Gereformeerde Gemeenten" : "This website offers audio and reading sermons from ministers of the 'Reformed Congregations' - denomination ('Gereformeerde Gemeenten') from different countries: the Netherlands, Canada, United States, Nigeria and New Zealand.";
            var siteUrl = "https://www.prekenweb.nl";
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
                var taalInfo = TaalInfoHelper.FromRouteData(RouteData);
                writer.WriteElementString("language", taalInfo.CultureInfo.TextInfo.CultureName);
                writer.WriteElementString("copyright", HttpUtility.HtmlEncode(string.Format("Copyright {0}, {1}", DateTime.UtcNow.Year, authorName)));
                writer.WriteElementString("lastBuildDate", DateTime.UtcNow.ToString("r"));
                writer.WriteElementString("pubDate", DateTime.UtcNow.ToString("r"));
                writer.WriteElementString("webMaster", authorEmail);
                writer.WriteElementString("ttl", "60");

                //// Image 2
                writer.WriteStartElement("image");
                writer.WriteElementString("url", "https://www.prekenweb.nl/content/images/logo1400.jpg");
                writer.WriteElementString("title", Resources.Resources.PrekenWebNL);
                writer.WriteElementString("link", "https://www.prekenweb.nl/");
                writer.WriteEndElement();

                var zoekService = new ZoekService(_zoekenRepository);
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
                    writer.WriteAttributeString("url", Url.ContentAbsolute(string.Format("{0}/{1}/{2}", ConfigurationManager.AppSettings["AzureBlobStorageBaseUrl"], ConfigurationManager.AppSettings["PrekenFolder"], zoekResultaatItem.Preek.Bestandsnaam)));
                    writer.WriteAttributeString("length", zoekResultaatItem.Preek.Bestandsgrootte.ToString());
                    writer.WriteAttributeString("type", zoekResultaatItem.Preek.GetContentType());
                    writer.WriteEndElement(); // End enclosure

                    writer.WriteElementString("category", Resources.Resources.PrekenWeb);
                    if (zoekResultaatItem.Preek.DatumGepubliceerd.HasValue) writer.WriteElementString("pubDate", zoekResultaatItem.Preek.DatumGepubliceerd.Value.ToString("r"));
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
