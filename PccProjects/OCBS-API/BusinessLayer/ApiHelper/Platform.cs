using BusinessLayer.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ApiHelper
{

    public class Platform : IPlatform
    {
        private readonly IConfiguration _configuration;
        protected readonly HttpClient Http;

        public Platform(IConfiguration config)
        {
            _configuration = config ?? throw new ArgumentNullException(nameof(config));
        }
        public async Task<TReturn> GetAsync<TReturn>(string relativeUri, string token, string platformuserid)
        {
            try
            {
                var BaseRoute = _configuration["Platform:apiurl"];
                using (var client = new HttpClient())
                {
                    //var token = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOjE0MjA2LCJyb2xlIjoiT0NCUyIsImlhdCI6MTY0NjQ1OTMyNn0.IKiptZ2DznOfzURUpljWugcxtC4Q85bJpSYrhhVjQyc";
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    client.DefaultRequestHeaders.Add("x-pcc-secret-key", _configuration["Platform:secretkey"]);
                    client.DefaultRequestHeaders.Add("x-ocbs-id", platformuserid);
                    HttpResponseMessage res = await client.GetAsync($"{BaseRoute}/{relativeUri}");
                    if (res.IsSuccessStatusCode)
                    {
                        return await res.Content.ReadFromJsonAsync<TReturn>();
                    }

                    string msg = await res.Content.ReadAsStringAsync();
                    throw new Exception(msg);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

        public async Task<TReturn> PostAsync<TReturn, TParam>(string relativeUri, string token, string platformuserid, TParam param)
        {
            try
            {
                var BaseRoute = _configuration["Platform:apiurl"];
                using (var client = new HttpClient())
                {
                    //var token = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOjE0MjA2LCJyb2xlIjoiT0NCUyIsImlhdCI6MTY0NjQ1OTMyNn0.IKiptZ2DznOfzURUpljWugcxtC4Q85bJpSYrhhVjQyc";
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    client.DefaultRequestHeaders.Add("x-pcc-secret-key", _configuration["Platform:secretkey"]);
                    client.DefaultRequestHeaders.Add("x-ocbs-id", platformuserid);
                    HttpResponseMessage res = await client.PostAsJsonAsync($"{BaseRoute}/{relativeUri}", param);
                    if (res.IsSuccessStatusCode)
                    {
                        return await res.Content.ReadFromJsonAsync<TReturn>();
                    }

                    string msg = await res.Content.ReadAsStringAsync();
                    throw new Exception(msg);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
    }
}
