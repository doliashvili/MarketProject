using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Abstract
{
    public interface IImageUploader<T> where T : class
    {
        Task<T> Upload(string uniqueName, byte[] image, int? width = null, int? height = null);
    }
}
