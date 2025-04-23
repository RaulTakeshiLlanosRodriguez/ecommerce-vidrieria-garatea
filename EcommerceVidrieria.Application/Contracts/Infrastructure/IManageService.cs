using EcommerceVidrieria.Application.Models.ImageManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Contracts.Infrastructure
{
    public interface IManageService
    {
        Task<ImageResponse> UploadImage(ImageData imageStream);
    }
}
