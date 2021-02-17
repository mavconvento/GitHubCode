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
using System.Data;

namespace BussinessLayer
{
    public class AccountService : IAccount
    {
        private readonly IAccountRepository _account;
        private readonly IOTPCodeRepository _otpCode;
        private readonly IConfiguration _configuration;

        public AccountService(IAccountRepository accountRepository, IConfiguration configuration, IOTPCodeRepository oTPCodeRepository)
        {
            _account = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _otpCode = oTPCodeRepository ?? throw new ArgumentNullException(nameof(oTPCodeRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(480),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User> Authenticate(string username, string password)
        {
            try
            {
                var user = await _account.Authenticate(username, password);

                if (user != null)
                {
                    var tokenString = GenerateJSONWebToken(user);
                    user.Token = tokenString;
                }

                return user;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Task<string> Delete()
        {
            throw new NotImplementedException();
        }

        public Task<List<AccountService>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<AccountService> GetUser(string userID)
        {
            throw new NotImplementedException();
        }

        public async Task<DataTable> Update(Profile profile)
        {
            try
            {
                return await _account.Update(profile);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<DataSet> GetVideo(string type)
        {
            try
            {
                return await _account.GetVideo(type);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<string> LinkMobileNumber(LinkMobile linkMobile)
        {
            try
            {
                if (linkMobile.Action == "OTPValidation")
                {
                    return await _otpCode.ValidateOTPCode(linkMobile);
                }
                else
                {
                    var result = await _otpCode.GetOtpCode(linkMobile);
                    var resultMesage = "";

                    if (result != "")
                        resultMesage = "Success";

                    else
                        resultMesage = "Error during sending OTPCode.";

                    //var message = new StringBuilder();
                    
                    //message.Append("Your OTP is " + linkMobile.OtpCode + ". ");
                    //message.Append("Valid for 5 mins. Do not disclose One-Time Password.");

                    ////sendotp
                    //Helper.Helper helper = new Helper.Helper(_configuration);
                    //var otpSendStatus = await helper.itexmo(linkMobile.MobileNumber, message.ToString());

                    //if (otpSendStatus.ToString() == "0")

                    return resultMesage;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<DataSet> GetLinkMobileNumber(string userid)
        {
            try
            {
                return await _account.GetLinkMobileNumber(userid);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<string> Insert(User user)
        {
            try
            {

                return await _account.Insert(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<DataSet> Pasaload(string mobileNumberFrom, string mobileNumberTo, string Amount)
        {
            try
            {

                return await _account.Pasaload(mobileNumberFrom, mobileNumberTo, Amount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DataSet> LoadMavcCard(string ClubName, string mobileNumber, string keyword, string DBName)
        {
            try
            {
                return await _account.LoadMavcCard(ClubName, mobileNumber, keyword, DBName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DataSet> Unreg(string ClubName, string mobileNumber, string keyword, string DBName, string UserID)
        {
            try
            {
                return await _account.Unreg(ClubName, mobileNumber, keyword, DBName, UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> SetAsPrimary(string mobileNumber, string UserID)
        {
            try
            {
                return await _account.SetAsPrimary(mobileNumber, UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DataSet> GetMobileListByEmail(string email)
        {
            try
            {
                return await _account.GetMobileListByEmail(email);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public async Task<string> ValidateMobileNumber(string email, string mobile)
        {
            try
            {
                string resultMesage = "";
                var result = await _account.ValidateMobileNumber(email, mobile);

                if (result.Tables.Count > 0)
                {
                    if (result.Tables[0].Rows.Count > 0)
                         resultMesage = "Success";
                    else
                        resultMesage = "Error during sending password.";
                }

                return resultMesage;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public async Task<DataSet> GetMemberCoordinates(string memberidno, string clubname, string dbname)
        {
            try
            {
                return await this._account.GetMemberCoordinates(memberidno, clubname, dbname);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
