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
        
        public const string defaultImageName = "logo.jpeg";
        public static readonly string newFileName = Guid.NewGuid().ToString();
        
        public static async Task WriteFormFileToImagesPost(IFormFile formFile, string imageName)
        {
            string filePath = $@"{"Images"}\{"Post"}\{imageName}";
            if (formFile.Length > 0) {
                using (Stream fileStream = new FileStream(filePath, FileMode.Create)) {
                    await formFile.CopyToAsync(fileStream);
                }
            }
        }

        public static byte[] GetDefaultImage()
        {
            var fileInfo = new FileInfo($@"{"Images"}\{defaultImageName}");
            byte[] bytes = new byte[fileInfo.Length];
            using (var fs = fileInfo.OpenRead())
            {
                fs.Read(bytes, 0, bytes.Length);
            }

            return bytes;
        }
        
        public static async Task WriteImageBytesToImagesGet(byte[] imageBytes, string imageName)
        {
            string filePath = $@"{"Images"}\{"Get"}\{imageName}";
            await File.WriteAllBytesAsync(filePath, imageBytes);
        }
        
        public static async Task<byte[]> ConvertFormFileToByteArray(IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                byte[] bytes = memoryStream.ToArray();
                return bytes;
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
            string imageName = newFileName + GetFormFileExtension(formFile);
            return imageName;
        }
    }
}