using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessLayer.Contracts;
using Microsoft.Extensions.Configuration;
using Repository.Contracts;
using DomainObject;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BussinessLayer.Helper
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _image;
        private readonly IConfiguration _configuration;

        public ImageService(IImageRepository imageRepository, IConfiguration configuration)
        {
            _image = imageRepository ?? throw new ArgumentNullException(nameof(imageRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<FileUpload> GetImage(Guid FileUploadID)
        {
            try
            {
                var user = await _image.GetImage(FileUploadID);

                return user;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public byte[] GetImageAsBytes(IFormFile file)
        {
            //new memorystream
            using (var ms = new MemoryStream())
            {

                //copy bytes to memorystream
                file.CopyTo(ms);

                return ms.ToArray();
            }
        }
    }
}
