using System;
using Repository.Database;
using System.Threading.Tasks;
using System.Linq;
using Repository.Contracts;
using DomainObject;
using DomainObject.AppServices;
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

        public async Task<User> Authenticate(string userName,string password)
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

        public async Task<string> Update(User user)
        {
            using (var transaction = await _dbcontext.Database.BeginTransactionAsync())
            {
                try
                {
                    var exists = _dbcontext.User.Where(x => x.UserName == user.UserName).FirstOrDefault();

                    if (exists == null)
                    {
                        user.DateCreated = DateTime.Now;
                        _dbcontext.Add(user);
                        _dbcontext.SaveChanges();
                    }
                    else
                    {

                    }

                    transaction.Commit();
                    return null;
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
            using (var transaction  = await _dbcontext.Database.BeginTransactionAsync())
            {
                try
                {
                    var exists = _dbcontext.User.Where(x => x.UserName == user.UserName).FirstOrDefault();

                    if (exists == null)
                    {
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
