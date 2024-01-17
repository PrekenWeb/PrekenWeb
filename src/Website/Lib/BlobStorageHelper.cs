using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace Prekenweb.Website.Lib
{
    public static class BlobStorageHelper
    {
        private static readonly string ContainerName = "prekenweb";

        public static BlobClient GetBlobClient(string fileName)
        {
            var connectionString = ConfigurationManager.AppSettings["AzureBlobStorageConnectionString"];

            var blobServiceClient = new BlobServiceClient(connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

            return blobContainerClient.GetBlobClient(fileName);
        }

        public static void Upload(MemoryStream stream, string fileName)
        {
            var connectionString = ConfigurationManager.AppSettings["AzureBlobStorageConnectionString"];

            var blobServiceClient = new BlobServiceClient(connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

            var blobClient = blobContainerClient.GetBlobClient(fileName);

            blobClient.Upload(stream, true);
        }

        public static void Upload(HttpPostedFileBase fileBase, string fileName)
        {
            var connectionString = ConfigurationManager.AppSettings["AzureBlobStorageConnectionString"];

            var blobServiceClient = new BlobServiceClient(connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

            var blobClient = blobContainerClient.GetBlobClient(fileName);

            using (var stream = fileBase.InputStream)
            {
                blobClient.Upload(stream, true);
            }
        }

        public static string HandleSermonUpload(HttpPostedFileBase uploadedPreek, int preekId, string rootFolder, string oudeBestandsnaam)
        {
            var connectionString = ConfigurationManager.AppSettings["AzureBlobStorageConnectionString"];

            var blobServiceClient = new BlobServiceClient(connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

            var nieuweBestandsnaam = oudeBestandsnaam;
            if (uploadedPreek == null || uploadedPreek.ContentLength <= 0) return nieuweBestandsnaam;

            nieuweBestandsnaam = $"{Path.GetFileNameWithoutExtension(uploadedPreek.FileName)}_{preekId}{Path.GetExtension(uploadedPreek.FileName)}";

            var blobClient = blobContainerClient.GetBlobClient(Path.Combine(rootFolder, nieuweBestandsnaam));

            if (nieuweBestandsnaam == oudeBestandsnaam || blobClient.Exists())
            {
                nieuweBestandsnaam = $"{Path.GetFileNameWithoutExtension(uploadedPreek.FileName)}_{preekId}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}{Path.GetExtension(uploadedPreek.FileName)}";
                blobClient = blobContainerClient.GetBlobClient(Path.Combine(rootFolder, nieuweBestandsnaam));
            }

            using (var stream = uploadedPreek.InputStream)
            {
                blobClient.Upload(stream, true);
            }

            return nieuweBestandsnaam;
        }

        public static Stream OpenRead(string fileName)
        {
            var connectionString = ConfigurationManager.AppSettings["AzureBlobStorageConnectionString"];

            var blobServiceClient = new BlobServiceClient(connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

            var blobClient = blobContainerClient.GetBlobClient(fileName);
            return blobClient.OpenRead();
        }

        public static Stream OpenRead(string fileName, BlobOpenReadOptions blobOpenReadOptions)
        {
            var connectionString = ConfigurationManager.AppSettings["AzureBlobStorageConnectionString"];

            var blobServiceClient = new BlobServiceClient(connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

            var blobClient = blobContainerClient.GetBlobClient(fileName);
            return blobClient.OpenRead(blobOpenReadOptions);
        }

        public static bool Exists(string fileName)
        {
            var connectionString = ConfigurationManager.AppSettings["AzureBlobStorageConnectionString"];

            var blobServiceClient = new BlobServiceClient(connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

            var blobClient = blobContainerClient.GetBlobClient(fileName);
            return blobClient.Exists();
        }

        public static byte[] Content(string fileName)
        {
            var connectionString = ConfigurationManager.AppSettings["AzureBlobStorageConnectionString"];

            var blobServiceClient = new BlobServiceClient(connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

            var blobClient = blobContainerClient.GetBlobClient(fileName);

            using (MemoryStream ms = new MemoryStream())
            {
                blobClient.DownloadTo(ms);
                return ms.ToArray();
            }
        }

        public static BlobProperties GetProperties(string fileName)
        {
            var connectionString = ConfigurationManager.AppSettings["AzureBlobStorageConnectionString"];

            var blobServiceClient = new BlobServiceClient(connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

            var blobClient = blobContainerClient.GetBlobClient(fileName);

            return blobClient.GetProperties();
        }

        public static void Stream(string fileName, MemoryStream ms)
        {
            var connectionString = ConfigurationManager.AppSettings["AzureBlobStorageConnectionString"];

            var blobServiceClient = new BlobServiceClient(connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

            var blobClient = blobContainerClient.GetBlobClient(fileName);

            blobClient.DownloadTo(ms);
            ms.Seek(0, SeekOrigin.Begin);
        }

        public static void DeleteIfExists(string fileName)
        {
            var connectionString = ConfigurationManager.AppSettings["AzureBlobStorageConnectionString"];

            var blobServiceClient = new BlobServiceClient(connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

            var blobClient = blobContainerClient.GetBlobClient(fileName);
            blobClient.DeleteIfExists();
        }
    }
}