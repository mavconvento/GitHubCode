using System;
using Repository.Database;
using System.Threading.Tasks;
using System.Linq;
using Repository.Contracts;
using DomainObject;
using DomainObject.AppServices;
using Repository.Helper;
//using Newtonsoft.Json;

namespace Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MavcPigeonDBContext _dbcontext;

        public AccountRepository(MavcPigeonDBContext dBContext)
        {
            _dbcontext = dBContext ?? throw new ArgumentNullException(nameof(dBContext));
        }

        public async Task<User> Authenticate(string userName, string password)
        {
            using (var transaction = await _dbcontext.Database.BeginTransactionAsync())
            {
                try
                {
                    var userdetails = _dbcontext.User.Where(x => x.UserName.Equals(userName) && x.Password.Equals(password)).FirstOrDefault();

                    //remove password this will be exposed in ui
                    userdetails.Password = null;

                    transaction.Commit();
                    return userdetails;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<string> Update(Profile profile)
        {
            using (var transaction = await _dbcontext.Database.BeginTransactionAsync())
            {
                try
                {
                    var user = _dbcontext.User.Where(x => x.UserID == profile.UserID).FirstOrDefault();

                    //return if not exists
                    if (user == null)
                    {
                        transaction.Rollback();
                        return null;
                    }


                    Guid fileUploadID = Guid.NewGuid();
                    if (profile.Image != null)
                    {
                        if (user.FileUploadID != null)
                        {
                            var fileUpload = _dbcontext.FileUploads.Where(x => x.FileUploadID == user.FileUploadID).FirstOrDefault();
                            fileUploadID = fileUpload.FileUploadID;
                            fileUpload.Data = Helper.ImageUpload.GetImageAsBytes(profile.Image);

                            _dbcontext.Attach(fileUpload);
                            _dbcontext.Entry(fileUpload).Property("Data").IsModified = true;
                        }
                        else
                        {
                            FileUpload file = new FileUpload
                            {
                                FileUploadID = fileUploadID,
                                Data = Helper.ImageUpload.GetImageAsBytes(profile.Image),
                                FileName = profile.Image.FileName,
                                FileType = profile.Image.ContentType
                            };
                            _dbcontext.Add(file);
                        }



                        user.FileUploadID = fileUploadID;
                    }

                    user.LoftName = profile.LoftName;

                    _dbcontext.Attach(user);
                    _dbcontext.Entry(user).Property("LoftName").IsModified = true;
                    _dbcontext.Entry(user).Property("FileUploadID").IsModified = true;

                    _dbcontext.SaveChanges();
                    transaction.Commit();

                    return fileUploadID.ToString();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }

            }
        }

        public async Task<string> Insert(User user)
        {
            using (var transaction = await _dbcontext.Database.BeginTransactionAsync())
            {
                try
                {
                    var exists = _dbcontext.User.Where(x => x.UserName == user.UserName).FirstOrDefault();

                    if (exists == null)
                    {
                        user.UserID = Guid.NewGuid();
                        user.DateCreated = DateTime.Now;
                        _dbcontext.Add(user);
                        _dbcontext.SaveChanges();
                    }
                    else
                    {
                        //transaction.Rollback();
                        throw new CustomException("Email Address already exists");
                    }

                    transaction.Commit();
                    return null;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }

            }
        }
    }
}
