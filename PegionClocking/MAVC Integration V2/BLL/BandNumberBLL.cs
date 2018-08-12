using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MAVC_IntegrationV2.BLL
{
    public class BandNumberBLL
    {
        DAL.BandNumberDAL dal;
        public void GetDetails(string fileNoteID, string accountID, string action)
        {
            try
            {
                DataSet dtResult = new DataSet();
                dal = new DAL.BandNumberDAL();

                if (action == "Delete")
                {
                    DeleteDetails(accountID);
                }
                else
                {
                    dtResult = dal.GetDetails(accountID);
                    SaveDetails(dtResult, action);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteDetails(string accountID)
        {
            try
            {
                dal = new DAL.BandNumberDAL();
                dal.DeleteAccount(accountID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void SaveDetails(DataSet dtResult,string action)
        {
            try
            {
                dal = new DAL.BandNumberDAL();
                dal.SaveDetails(dtResult.Tables[0], action);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateFileNote(string fileNoteID)
        {
            try
            {
                dal = new DAL.BandNumberDAL();
                dal.UpdateFileNotes(fileNoteID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
