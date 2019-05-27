using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Eclock.BIZ
{
    public class ImportData
    {
        #region Variable
        DAL.ImportData DalImportData;
        #endregion

        #region Properties
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        #endregion

        #region Public Methods
        public DataSet GetMemberList()
        {
            try
            {
                DalImportData = new DAL.ImportData();
                return DalImportData.GetMemberList(this);
            }
            catch (Exception ex)
            {
       
                throw ex;
            }
        }
        public DataSet GetRegisterRFID()
        {
            try
            {
                DalImportData = new DAL.ImportData();
                return DalImportData.GetRegisterRFID(this);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataSet GetRegisterBandNumberWithRFID()
        {
            try
            {
                DalImportData = new DAL.ImportData();
                return DalImportData.GetRegisterBandNumberWithRFID(this);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Private Methods

        #endregion
    }

}
