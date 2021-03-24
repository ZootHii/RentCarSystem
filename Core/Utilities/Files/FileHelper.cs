using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Files
{
    public static class FileHelper
    {
        // First create :
        // WebAPI -> Images -> Get (Directory)
        // WebAPI -> Images -> Post (Directory)
        // WebAPI -> Images -> logo.jpeg

        public const string defaultImageName = "logo.jpg";

        public static async Task WriteFormFileToImagesPost(IFormFile formFile, string imageName)
        {
            string filePath = $@"{"Images"}\{"Post"}\{imageName}";
            if (formFile.Length > 0)
            {
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }
            }
        }

        public static void DeleteImage(string imageName)
        {
            string filePathPost = $@"{"Images"}\{"Post"}\{imageName}";
            string filePathGet = $@"{"Images"}\{"Get"}\{imageName}";
            if (File.Exists(filePathPost))
            {
                File.Delete(filePathPost);
            }
            if (File.Exists(filePathGet))
            {
                File.Delete(filePathGet);
            }
        }

        public static byte[] GetDefaultImage()
        {
            var fileInfo = new FileInfo($@"{"Images"}\{defaultImageName}");
            byte[] imageBytes = new byte[fileInfo.Length];
            using (Stream fileStream = fileInfo.OpenRead())
            {
                fileStream.Read(imageBytes, 0, imageBytes.Length);
            }

            return imageBytes;
        }

        public static async Task WriteImageBytesToImagesGet(byte[] imageBytes, string imageName)
        {
            string filePath = $@"{"Images"}\{"Get"}\{imageName}";
            await File.WriteAllBytesAsync(filePath, imageBytes);
        }

        // TODO sıkıştır https://stackoverflow.com/questions/38383683/compressing-images-in-c-sharp-asp-net-core
        public static async Task<byte[]> ConvertFormFileToByteArray(IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();
                return imageBytes;
            }
        }

        public static string GetFormFileExtension(IFormFile formFile)
        {
            var fileInfo = new FileInfo(formFile.FileName);
            string fileExtension = fileInfo.Extension;
            return fileExtension;
        }

        public static string CreateImageNameWithExtension(IFormFile formFile)
        {
            string newFileName = Guid.NewGuid().ToString();
            string imageName = newFileName + GetFormFileExtension(formFile);
            return imageName;
        }
    }
}