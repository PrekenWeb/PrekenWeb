using System.Configuration;
using Prekenweb.Website.Areas.Mijn.Models;
using System;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Data.Identity;
using Data.Tables;
using Prekenweb.Website.Lib;
using System.Runtime.InteropServices.ComTypes;

namespace Prekenweb.Website.Areas.Mijn.Controllers
{
    [Authorize(Roles = "Spotlight")]
    public class AfbeeldingController : Controller
    {
        private readonly IPrekenwebContext<Gebruiker> _context;

        public AfbeeldingController(IPrekenwebContext<Gebruiker> context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View(new AfbeeldingIndexViewModel
            {
                Afbeeldingen = _context.Afbeeldings.OrderByDescending(g => g.Id).ToList()
            });
        }

        public ActionResult Verwijder(int id)
        {
            Afbeelding afbeelding = _context.Afbeeldings.Single(a => a.Id == id);
            var rootFolder = ConfigurationManager.AppSettings["AfbeeldingenFolder"];

            BlobStorageHelper.DeleteIfExists(Path.Combine(rootFolder, afbeelding.Bestandsnaam));
            BlobStorageHelper.DeleteIfExists(Path.Combine(rootFolder, $"Thumbnail_{afbeelding.Id}.jpg"));
            BlobStorageHelper.DeleteIfExists(Path.Combine(rootFolder, $"Homepage_{afbeelding.Id}.jpg"));

            OutputCacheHelpers.ClearOutputCaches(Response, Url);

            _context.Afbeeldings.Remove(afbeelding);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Bewerk(int id)
        {
            Afbeelding afbeelding = _context.Afbeeldings.Single(a => a.Id == id);
            ViewBag.AfbeeldingenFolder = ConfigurationManager.AppSettings["AfbeeldingenFolder"];
            return View(new AfbeeldingEditViewModel
            {
                Afbeelding = afbeelding
            });
        }

        [HttpPost]
        public ActionResult Bewerk(AfbeeldingEditViewModel viewModel)
        {
            ViewBag.AfbeeldingenFolder = ConfigurationManager.AppSettings["AfbeeldingenFolder"];
          
            if (!ModelState.IsValid) return View(viewModel);

            _context.Entry(viewModel.Afbeelding).State = EntityState.Modified;
            _context.SaveChanges();

            ModelState.Clear();

            string geuploadeAfbeeldingLocatie;

            try
            {
                geuploadeAfbeeldingLocatie = HandleUpload(viewModel.Bestand, viewModel.Afbeelding);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Bestand", ex.Message);
                return View(viewModel);
            }

            var afbeelding = _context.Afbeeldings.Single(a => a.Id == viewModel.Afbeelding.Id);

            if (!string.IsNullOrEmpty(geuploadeAfbeeldingLocatie))
            {
                HandleImage(geuploadeAfbeeldingLocatie, afbeelding.Id, false);
                if (!ModelState.IsValid) return View(viewModel);
            }

            OutputCacheHelpers.ClearOutputCaches(Response, Url);

            return View(new AfbeeldingEditViewModel
            {
                Afbeelding = afbeelding
            });
        }

        public void HandleImage(string geuploadeAfbeeldingLocatie, int afbeeldingId, bool isNieuw)
        {
            using (var image = GetImage(geuploadeAfbeeldingLocatie))
            {
                try
                {
                    if (image.Height >= 340 && image.Width >= 1024)
                    {
                        MaakHomepageImage(image, afbeeldingId);
                    }

                    MaakThumbnailImage(image, afbeeldingId);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Bestand", ex.Message);
                }
            }
        }

        public ActionResult Maak()
        {
            ViewBag.AfbeeldingenFolder = ConfigurationManager.AppSettings["AfbeeldingenFolder"];
            return View(new AfbeeldingEditViewModel
            {
                Afbeelding = new Afbeelding()
            });
        }

        [HttpPost]
        public ActionResult Maak(AfbeeldingEditViewModel viewModel)
        {
            if (viewModel.Bestand == null || !(viewModel.Bestand.ContentLength > 0)) ModelState.AddModelError("Bestand", "Geen bestand gekozen");

            ViewBag.AfbeeldingenFolder = ConfigurationManager.AppSettings["AfbeeldingenFolder"];

            if (!ModelState.IsValid || viewModel.Bestand == null) return View(viewModel);
            
            viewModel.Afbeelding.Bestandsnaam = viewModel.Bestand.FileName;
            viewModel.Afbeelding.ContentType = viewModel.Bestand.ContentType;
            _context.Afbeeldings.Add(viewModel.Afbeelding);
            _context.SaveChanges();

            ModelState.Clear();

            OutputCacheHelpers.ClearOutputCaches(Response, Url);

            string geuploadeAfbeeldingLocatie;

            try
            {
                geuploadeAfbeeldingLocatie = HandleUpload(viewModel.Bestand, viewModel.Afbeelding);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Bestand", ex.Message);
                return View(viewModel);
            }

            var afbeelding = _context.Afbeeldings.Single(a => a.Id == viewModel.Afbeelding.Id);

            if (!string.IsNullOrEmpty(geuploadeAfbeeldingLocatie))
            {
                HandleImage(geuploadeAfbeeldingLocatie, afbeelding.Id, true);
                if (!ModelState.IsValid) return View(viewModel);
            }
            else
            {
                ModelState.AddModelError("Bestand", "Geen afbeelding geupload, afbeelding aangemaakt maar zonden bestand!");
                return View(viewModel);
            }

            return RedirectToAction("Bewerk", new { viewModel.Afbeelding.Id });
        }

        public Image GetImage(string geuploadeAfbeeldingLocatie)
        {
            using (var memorystream = new MemoryStream())
            {
                BlobStorageHelper.Stream(geuploadeAfbeeldingLocatie, memorystream);
                return Image.FromStream(memorystream);
            }
        }

        public void MaakThumbnailImage(Image origineel, int afbeeldingId)
        {
            var rootFolder = ConfigurationManager.AppSettings["AfbeeldingenFolder"];
            var thumbnailBestandsnaam = Path.Combine(rootFolder, $"Thumbnail_{afbeeldingId}.jpg");

            using (var newImage = new Bitmap(100, 100))
            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                graphics.DrawImage(origineel, new Rectangle(0, 0, 100, 100));
                if (BlobStorageHelper.Exists(thumbnailBestandsnaam))
                {
                    BlobStorageHelper.DeleteIfExists(thumbnailBestandsnaam);
                }
                using (var memorystream = new MemoryStream())
                {
                    newImage.Save(memorystream, ImageFormat.Jpeg);
                    // Reset the stream position to the beginning
                    memorystream.Seek(0, SeekOrigin.Begin);
                    BlobStorageHelper.Upload(memorystream, thumbnailBestandsnaam);
                }
            }
        }

        public void MaakHomepageImage(Image origineel, int afbeeldingId)
        {
            var rootFolder = ConfigurationManager.AppSettings["AfbeeldingenFolder"];
            var thumbnailBestandsnaam = Path.Combine(rootFolder, $"Homepage_{afbeeldingId}.jpg");
            double height = 340.0;
            double width = origineel.Width;

            width = (height / origineel.Height) * width;

            if (width < 1024.0)
            {
                width = 1024.0;
                height = (1024.0 / origineel.Width) * origineel.Height;
            }

            using (var newImage = new Bitmap(Convert.ToInt32(width), Convert.ToInt32(height)))
            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.DrawImage(origineel, new Rectangle(0, 0, Convert.ToInt32(width), Convert.ToInt32(height)));
                if (BlobStorageHelper.Exists(thumbnailBestandsnaam))
                {
                    BlobStorageHelper.DeleteIfExists(thumbnailBestandsnaam);
                }
                using (var memorystream = new MemoryStream())
                {
                    newImage.Save(memorystream, ImageFormat.Jpeg);
                    // Reset the stream position to the beginning
                    memorystream.Seek(0, SeekOrigin.Begin);
                    BlobStorageHelper.Upload(memorystream, thumbnailBestandsnaam);
                }
            }
        }


        private string HandleUpload(HttpPostedFileBase uploadedAfbeelding, Afbeelding afbeelding)
        {
            if (uploadedAfbeelding == null || uploadedAfbeelding.ContentLength <= 0) return string.Empty;
            
            var rootFolder = ConfigurationManager.AppSettings["AfbeeldingenFolder"];

            var nieuweBestandsnaam =
                string.Format(
                    "{0}_{1}{2}",
                    Path.GetFileNameWithoutExtension(uploadedAfbeelding.FileName),
                    afbeelding.Id,
                    Path.GetExtension(uploadedAfbeelding.FileName)
                    );

            var oudeBestandsnaam = afbeelding.Bestandsnaam;

            if (nieuweBestandsnaam == oudeBestandsnaam || BlobStorageHelper.Exists(Path.Combine(rootFolder, nieuweBestandsnaam)))
                nieuweBestandsnaam = string.Format(
                    "{0}_{1}_{2:yyyy-MM-dd_hh-mm-ss}{3}",
                    Path.GetFileNameWithoutExtension(uploadedAfbeelding.FileName),
                    afbeelding.Id,
                    DateTime.Now,
                    Path.GetExtension(uploadedAfbeelding.FileName)
                    );

            try
            {
                BlobStorageHelper.Upload(uploadedAfbeelding, Path.Combine(rootFolder, nieuweBestandsnaam));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Cannot save image: {0}", ex.Message));
            }

            afbeelding.Bestandsnaam = nieuweBestandsnaam;
            afbeelding.ContentType = afbeelding.ContentType;
            _context.Entry(afbeelding).State = EntityState.Modified;
            _context.SaveChanges();

            try
            {
                if (!string.IsNullOrEmpty(oudeBestandsnaam))
                {
                    BlobStorageHelper.DeleteIfExists(Path.Combine(rootFolder, oudeBestandsnaam));
                    BlobStorageHelper.DeleteIfExists(Path.Combine(rootFolder, $"Thumbnail_{afbeelding.Id}.jpg"));
                    BlobStorageHelper.DeleteIfExists(Path.Combine(rootFolder, $"Homepage_{afbeelding.Id}.jpg"));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Could not remove the old image, probably because it did not exist, new image uploaded successfully {0}", ex.Message));
            }

            return Path.Combine(rootFolder, nieuweBestandsnaam);
        }
    }
}
