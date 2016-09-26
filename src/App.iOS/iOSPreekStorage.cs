 
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using App.iOS;
using App.Shared;
using PCLStorage;
using Xamarin.Forms;

[assembly: Dependency(typeof(iOSPreekStorage))]
namespace App.iOS
{
    public class iOSPreekStorage : IPreekStorage
    {
        public async Task<string> DownloadPreek(int id, string fileName)
        {
            var ext = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic), "PrekenWeb");
            var rootfolder = FileSystem.Current.LocalStorage;
            var appfolder = await rootfolder.CreateFolderAsync(ext, CreationCollisionOption.OpenIfExists);
            var dbfolder = await appfolder.CreateFolderAsync("Db", CreationCollisionOption.OpenIfExists);
            var file = await dbfolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

            using (var client = new HttpClient())
            using (var response = await client.GetAsync($"http://test.prekenweb.nl/nl/Preek/Download/{id}?inline=False", HttpCompletionOption.ResponseHeadersRead))
            using (var fileHandler = await file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
            {
                var dataBuffer = await response.Content.ReadAsByteArrayAsync();
                await fileHandler.WriteAsync(dataBuffer, 0, dataBuffer.Length);
            }
            return file.Path;
        }
    }
}
