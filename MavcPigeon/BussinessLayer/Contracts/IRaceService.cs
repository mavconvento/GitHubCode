using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using DomainObject;

namespace BussinessLayer.Contracts
{
    public interface IRaceService
    {
        Task<DataTable> GetRaceResult(RaceFilter raceFilter);
        Task<DataSet> GetRaceDetails(RaceFilter raceFilter);
        Task<DataTable> GetBirdCategory(string dbName = "", string clubName = "");
        Task<DataTable> GetGroupCategory(string dbName = "", string clubName = "");
        Task<DataTable> GetRaceEntry(RaceFilter raceFilter);
        Task<DataTable> GetBalance(string mobileNumber);
        Task<DataSet> OnlineClocking(OnlineClocking online);
        Task<DataSet> QRCodeClocking(string qrcode);
        Task<DataTable> GetLocation(RaceFilter raceFilter);
        Task<DataSet> TopigeonTrainingSave(TopigeonTraining value);
        Task<DataSet> GetTopigeonTraining(TopigeonTraining value);
        Task<DataTable> GetTrainingResult(TopigeonTraining value);
    }
}
