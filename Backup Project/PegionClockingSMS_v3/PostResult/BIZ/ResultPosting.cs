using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace PostResult.BIZ
{
    class ResultPosting
    {
        #region Constant
        #endregion

        #region Variable
        DAL.ResultPosting raceResult;
        #endregion

        #region Properties
        #endregion

        #region Public Methods
        public DataSet RaceResult(string Club)
        {
            DataSet dataResult = new DataSet();
            try
            {
                raceResult = new DAL.ResultPosting();
                
                PopulateDataLayer();
                dataResult = raceResult.RaceResult(Club);
            }
            catch //(Exception ex)
            {
                //throw ex;
            }
            return dataResult;
        }
        public DataSet GetClubList()
        {
            DataSet dataResult = new DataSet();
            try
            {
                raceResult = new DAL.ResultPosting();

                PopulateDataLayer();
                dataResult = raceResult.GetClubList();
            }
            catch //(Exception ex)
            {
                //throw ex;
            }
            return dataResult;
        }
        #endregion

        #region Private Methods
        private void PopulateDataLayer()
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
