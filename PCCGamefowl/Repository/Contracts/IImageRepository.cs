using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DomainObject;

namespace Repository.Contracts
{
    public interface IImageRepository
    {
        Task<FileUpload> GetImage(Guid FileUploadID);
    }
}
