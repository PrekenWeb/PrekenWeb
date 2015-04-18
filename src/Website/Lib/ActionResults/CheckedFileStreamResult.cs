using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prekenweb.Models;

namespace Prekenweb.Website.Lib.ActionResults
{
    public class MonitoredPreekFileResult : FilePathResult
    {
        public MonitoredPreekFileResult(string fileName, string contentType, int preekId)
            : base(fileName, contentType)
        {
            DownloadCompleted = false;
            PreekId = preekId;
        }

        public bool DownloadCompleted { get; set; }
        public int PreekId { get; set; }

        protected override void WriteFile(HttpResponseBase response)
        {
            var outputStream = response.OutputStream;
            using (var fileStrem = new FileStream(FileName, FileMode.Open))
            {
                var buffer = new byte[_bufferSize];
                var count = fileStrem.Read(buffer, 0, _bufferSize);
                while (count != 0 && response.IsClientConnected)
                {
                    outputStream.Write(buffer, 0, count);
                    count = fileStrem.Read(buffer, 0, _bufferSize);
                }
                if (response.IsClientConnected)
                    using (PrekenwebContext context = new PrekenwebContext())
                    {
                        context.Preeks.Single(p => p.Id == PreekId).AantalKeerGedownload++;
                        context.SaveChanges();
                    }
                DownloadCompleted = response.IsClientConnected;
            }
        }

        private const int _bufferSize = 0x1000;
    }
}