using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;


namespace ConflwtratorAdmin.Helper
{
    public class BlobStorageHelper
    {
        const string accountName = "";
        const string key = "";

        public static async Task<string> GetImageUrl(string imageFile)
        {
            var storageAccount = new CloudStorageAccount(new StorageCredentials(accountName, key), true);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("conflwtrator");
            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions()
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });
            string imageName = null;
            if (imageFile != null)
            {
                imageName= Guid.NewGuid().ToString() + "-" + Path.GetFileName(imageFile);

                CloudBlockBlob cloudBlockBlob = container.GetBlockBlobReference(imageName);
                cloudBlockBlob.Properties.ContentType = "image/jpg";
                using (Stream file = File.OpenRead(imageFile))
                {
                    await cloudBlockBlob.UploadFromStreamAsync(file);
                }
                return cloudBlockBlob.Uri.ToString();
            }
            return imageName;
        }
    }
}

