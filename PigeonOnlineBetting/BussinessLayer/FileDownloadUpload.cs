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
using System.Net.Http.Headers;
using System.Net.Http;
using System.IO;

namespace BussinessLayer
{
    public class FileDownloadUpload : IFileDownloadUpload
    {

        public async Task<string> FileUpload(DomainObject.FileUpload file)
        {
            return await Task.FromResult("ok");
            throw new NotImplementedException();
        }

       public async Task<string> UploadImage(DomainObject.FileUpload file)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:55587/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE2MDkzNjcxMDEsImlzcyI6Im1hdmNwaWdlb24uY29tIiwiYXVkIjoibWF2Y3BpZ2Vvbi5jb20ifQ.0LuXap5r8gLT3v3gIQordMATBcMq2hbNnnS7BAjft78");

                    byte[] data;
                    using (var br = new BinaryReader(file.File.OpenReadStream()))
                    {
                        data = br.ReadBytes((int)file.File.OpenReadStream().Length);
                    }
                    ByteArrayContent bytes = new ByteArrayContent(data);
                    MultipartFormDataContent multiContent = new MultipartFormDataContent();
                    multiContent.Add(bytes, "file", file.File.FileName);
                    multiContent.Add(new StringContent(file.FileUploadID.ToString()), "Id");
                    multiContent.Add(new StringContent(file.FileName), "fileName");

                    HttpResponseMessage response = await client.PostAsync("api/file/UploadProfile", multiContent);
                    //if (response.IsSuccessStatusCode)
                    //{

                    //}
                    //else
                    //{
                    //    Console.WriteLine("Internal server Error");
                    //}
                }

                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
