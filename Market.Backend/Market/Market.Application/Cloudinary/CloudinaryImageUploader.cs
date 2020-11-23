using System.IO;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Market.Domain.Abstract;

namespace Market.Application.Cloudinary
{
    public class CloudinaryImageUploader : IImageUploader<ImageUploadResult>
    {
        private const string CloudName = "dnvzblymb";
        private const string ApiKey = "247968345686149";
        private const string ApiSecret = "juLhuud83o42qh79GD1XJK5KIY8";
        private readonly CloudinaryDotNet.Cloudinary _cloudinary;
        private readonly Account _account;
        public CloudinaryImageUploader()
        {
            _account = new Account() { ApiKey = ApiKey, ApiSecret = ApiSecret, Cloud = CloudName };
            _cloudinary = new CloudinaryDotNet.Cloudinary(_account);
        }

        
        public async Task<ImageUploadResult> Upload(string uniqueName, byte[] image, int? width, int? height)
        {
            //todo fix 
            //using var stream = new MemoryStream();
            //    stream.Write(image, 0, image.Length); ;
            //    ImageUploadParams @params=new ImageUploadParams();
            //    if (width != null || height != null)
            //        @params = new ImageUploadParams()
            //        {
            //            File = new FileDescription(uniqueName,stream),
            //            PublicId = uniqueName
            //        };
            //     @params.Transformation = new Transformation();
            //    if (width != null)
            //        @params.Transformation.Crop("scale").Width(width);
            //    if (height != null)
            //        @params.Transformation.Crop("scale").Height(height);
            //ImageUploadResult mainUploadImage = await _cloudinary.UploadAsync(@params);
            //return mainUploadImage;

            Stream stream = new MemoryStream(image);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(uniqueName, stream),
                PublicId = uniqueName,
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult;
        }


        public async Task<object> DeleteByTag(string publicId)
        {
           return await _cloudinary.DeleteResourcesAsync(publicId);
        }
    }
}
