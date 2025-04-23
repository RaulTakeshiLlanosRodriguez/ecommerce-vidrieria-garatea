using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using EcommerceVidrieria.Application.Contracts.Infrastructure;
using EcommerceVidrieria.Application.Models.ImageManagment;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Infrastructure.ImageCloudinary
{
    public class ManageImageService : IManageService
    {
        public CloudinarySettings _cloudinarySettings { get; }

        public ManageImageService(IOptions<CloudinarySettings> cloudinarySettings)
        {
            _cloudinarySettings = cloudinarySettings.Value;
        }

        public async Task<ImageResponse> UploadImage(ImageData imageStream)
        {
            var account = new Account(
                _cloudinarySettings.CloudName,
                _cloudinarySettings.ApiKey,
                _cloudinarySettings.ApiSecret
            );

            var cloudinary = new Cloudinary(account);
            var uploadImage = new ImageUploadParams()
            {
                File = new FileDescription(imageStream.Name, imageStream.ImageStream)
            };

            var uploadResult = await cloudinary.UploadAsync(uploadImage);
            if (uploadResult.StatusCode == HttpStatusCode.OK)
            {
                return new ImageResponse
                {
                    PublicId = uploadResult.PublicId,
                    Url = uploadResult.Url.ToString()
                };
            }
            throw new Exception("No se pudo guardar la imagen");
        }
    }
}
