using System;
using Repository.Database;
using System.Threading.Tasks;
using System.Linq;
using Repository.Contracts;
using DomainObject;
using DomainObject.AppServices;
using Repository.Helper;
namespace Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly MavcPigeonDBContext _dbcontext;

        public ImageRepository(MavcPigeonDBContext dBContext)
        {
            _dbcontext = dBContext ?? throw new ArgumentNullException(nameof(dBContext));
        }

        public async Task<FileUpload> GetImage(Guid fileUploadID)
        {
            using (var transaction = await _dbcontext.Database.BeginTransactionAsync())
            {
                try
                {
                    var image = _dbcontext.FileUploads.Where(x => x.FileUploadID == fileUploadID).FirstOrDefault();

                    transaction.Commit();
                    return image;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
