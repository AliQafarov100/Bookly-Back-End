using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Bookly_Back_End.Utilities
{
    public static class FileExtesion
    {
        public static async Task<string> FileCreate(this IFormFile file,string root,string folder)
        {
            string fileName = Guid.NewGuid() + file.FileName;
            string filePath = Path.Combine(root, folder);
            string fullPath = Path.Combine(filePath, fileName);

            try
            {
                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch(Exception)
            {
                throw new FileLoadException();
            }
            

            return fileName;
        }

        public static bool IsOkay(this IFormFile file,int mb)
        {
            return file.Length < mb * 1024 * 1024 && file.ContentType.Contains("image/");
        }

        public static void FileDelete(string root, string folder, string image)
        {
            string path = root + folder + image;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
