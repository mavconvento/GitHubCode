using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DomainObject;

namespace BussinessLayer.Contracts
{
    public interface IFileDownloadUpload
    {
        Task<string> FileUpload(DomainObject.FileUpload file);
        Task<string> UploadImage(DomainObject.FileUpload file);
    }
}
