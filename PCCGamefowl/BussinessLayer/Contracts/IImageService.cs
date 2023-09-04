using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DomainObject;

namespace BussinessLayer.Contracts
{
    public interface IImageService
    {
        Task<FileUpload> GetImage(Guid FileUploadID);

    }
}
