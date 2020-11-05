using System;
using Application.Interfaces;
using Application.Photos;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Photos
{
    public class PhotoAccessor : IPhotoAccessor
    {
        private readonly Cloudinary _cloudinary;
        public PhotoAccessor(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(acc);
        }

        public PhotoUploadResult AddPhoto(IFormFile file)
        {
            var uploudResult = new ImageUploadResult();

            if(file.Length > 0) {
                using (var stream = file.OpenReadStream()){
                    var uploadParams = new ImageUploadParams{
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                    };
                    uploudResult = _cloudinary.Upload(uploadParams);
                }
            }

            if(uploudResult.Error != null) {
                throw new Exception(uploudResult.Error.Message);
            }
            return new PhotoUploadResult{
                PublicId = uploudResult.PublicId,
                Url = uploudResult.Url.AbsoluteUri
            };
        }

        public string DeletePhoto(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = _cloudinary.Destroy(deleteParams);

            return result.Result == "ok" ? result.Result : null;
        }
    }
}