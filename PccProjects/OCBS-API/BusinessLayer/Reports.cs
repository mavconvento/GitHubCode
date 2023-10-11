using BusinessLayer.ApiHelper;
using BusinessLayer.Contracts;
using DomainObject;
using Microsoft.Extensions.Configuration;
using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using DomainObject.PlatformObject;
using Newtonsoft.Json;
using System.Data;
using DomainObject.DatabaseObject;

namespace BusinessLayer
{
    public class Reports : IReports
    {
        private readonly IConfiguration _configuration;
        private readonly IPlatform _platform;
        private readonly IReportRepository _reportRepository;
        public Reports(IConfiguration config, IPlatform platform, IReportRepository reportRepository)
        {
            _configuration = config ?? throw new ArgumentNullException(nameof(config));
            _platform = platform ?? throw new ArgumentNullException(nameof(platform));
            _reportRepository = reportRepository ?? throw new ArgumentNullException(nameof(reportRepository));
        }

        public async Task<DomainObject.DatabaseObject.BettingReport> BettingReportByFightNo(Int64 eventId, Int64 fightno)
        {
            try
            {
                return await _reportRepository.BettingReportByFightNo(eventId, fightno);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }    
        }

        public async Task<List<BettingReport>> BettingReportSummary(long eventid, long userid)
        {
            try
            {
                return await _reportRepository.BettingReportSummary(eventid, userid);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
