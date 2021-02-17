using BussinessLayer.Contracts;
using DomainObject;
using Microsoft.Extensions.Configuration;
using Repository.Contracts;
using System;
using System.Data;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class RaceService : IRaceService
    {
        private readonly IRaceRepository _race;
        private readonly IConfiguration _configuration;

        public RaceService(IRaceRepository race, IConfiguration configuration)
        {
            _race = race ?? throw new ArgumentNullException(nameof(race));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<DataTable> GetBalance(string mobileNumber)
        {
            try
            {
                return await this._race.GetBalance(mobileNumber);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<DataSet> OnlineClocking(OnlineClocking online)
        {
            try
            {
                //string[] value = online.Keyword.Split(" ");
                //if (value.Length > 0)
                //{ 
                //    online.ClubName = value[0];
                //    online.Keyword = "CLOCK" +
                //}
                

                return await this._race.OnlineClocking(online);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<DataTable> GetBirdCategory(string dbName = "", string clubName = "")
        {
            try
            {
                return await this._race.GetBirdCategory(dbName, clubName);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<DataTable> GetGroupCategory(string dbName = "", string clubName = "")
        {
            try
            {
                return await this._race.GetGroupCategory(dbName, clubName);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<DataSet> GetRaceDetails(RaceFilter raceFilter)
        {
            try
            {
                return await this._race.GetRaceDetails(raceFilter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<DataTable> GetRaceEntry(RaceFilter raceFilter)
        {
            try
            {
                return await this._race.GetRaceEntry(raceFilter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<DataTable> GetRaceResult(RaceFilter raceFilter)
        {
            try
            {
                return await this._race.GetRaceResult(raceFilter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
