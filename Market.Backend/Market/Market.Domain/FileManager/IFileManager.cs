using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.FileManager
{
    public interface IFileManager
    {
        Task<string> SaveImage(IFormFile image);
        FileStream ImageStream(string image);
        bool RemoveImage(string image);
    }
}
