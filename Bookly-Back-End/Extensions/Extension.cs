using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Microsoft.AspNetCore.Http;

namespace Bookly_Back_End.Extensions
{
    public static class Extension
    {
        
        public static bool IsOkay(this IFormFile file, int mb)
        {
            return file.Length < mb * 1024 * 1024 && file.ContentType.Contains("image/");
        }

    }
}
