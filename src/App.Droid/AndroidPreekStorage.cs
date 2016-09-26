using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using App.Droid;
using App.Shared;
using PCLStorage;
using Xamarin.Forms; 

[assembly: Dependency(typeof(AndroidPreekStorage))]
namespace App.Droid
{
    public class AndroidPreekStorage : IPreekStorage
    {
        public async Task<string> DownloadPreek(int id, string fileName)
        {
            var ext = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path, "Preken");
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