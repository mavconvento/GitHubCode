using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace BusinessImplementation
{
    public class Member
    {
    //    #region Public Methods
    //    public Boolean Save()
    //    {
    //        try
    //        {
    //            Boolean status = false;
    //            member = new DAL.Member();
    //            PopulateDataLayer();
    //            member.Save();
    //            MessageBox.Show("Member Record Save!", "Record Save");
    //            status = true;
    //            return status;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //    public Boolean MemberRingSave()
    //    {
    //        try
    //        {
    //            Boolean status = false;
    //            member = new DAL.Member();
    //            PopulateDataLayer();
    //            member.MemberRingSave();
    //            status = true;
    //            return status;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public Boolean ReaderRegistrationSave()
    //    {
    //        try
    //        {
    //            Boolean status = false;
    //            member = new DAL.Member();
    //            PopulateDataLayer();
    //            member.ReaderRegistrationSave();
    //            status = true;
    //            return status;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //    public Boolean RFIDRegisterSave()
    //    {
    //        try
    //        {
    //            Boolean status = false;
    //            member = new DAL.Member();
    //            PopulateDataLayer();
    //            member.RegisterRFIDSave();
    //            status = true;
    //            return status;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //    public void MemberDetailsSelectAll(DataGridView memberList)
    //    {
    //        try
    //        {
    //            member = new DAL.Member();
    //            PopulateDataLayer();
    //            memberList.DataSource = member.MemberDetailsSelectAll().Tables[0];
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //    public DataTable ExportEClockMasterlist()
    //    {
    //        try
    //        {
    //            member = new DAL.Member();
    //            PopulateDataLayer();
    //            DataTable dt = member.MemberDetailsSelectAll().Tables[0];
    //            return dt;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //    public DataSet MemberDetailsSearchByKey()
    //    {
    //        try
    //        {
    //            member = new DAL.Member();
    //            DataSet dataResult = new DataSet();
    //            PopulateDataLayer();
    //            dataResult = member.MemberDetailsSearchByKey();
    //            return dataResult;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }

    //    }
    //    public DataTable MemberRingSearchByKey()
    //    {
    //        try
    //        {
    //            member = new DAL.Member();
    //            DataTable dataResult = new DataTable();
    //            PopulateDataLayer();
    //            dataResult = member.MemberRingSearchByKey().Tables[0];
    //            return dataResult;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }

    //    }
    //    public Boolean MemberDetailsDelete()
    //    {
    //        try
    //        {
    //            Boolean status = false;
    //            member = new DAL.Member();
    //            PopulateDataLayer();
    //            member.MemberDetailsDelete();
    //            MessageBox.Show("Record Successfully Deleted!", "Delete Record");
    //            status = true;
    //            return status;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }

    //    }
    //    public Boolean MemberRingDelete()
    //    {
    //        try
    //        {
    //            Boolean status = false;
    //            member = new DAL.Member();
    //            PopulateDataLayer();
    //            member.MemberRingDelete();
    //            MessageBox.Show("Record Successfully Deleted!", "Delete Record");
    //            status = true;
    //            return status;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }

    //    }
    //    public DataSet GetMemberDistance()
    //    {
    //        try
    //        {
    //            member = new DAL.Member();
    //            DataSet dataResult = new DataSet();
    //            PopulateDataLayer();
    //            dataResult = member.GetMemberDistance();
    //            return dataResult;
    //        }
    //        catch (Exception ex)
    //        {

    //            throw ex;
    //        }
    //    }
    //    public DataTable GetMembetDistancePerReleasePoint()
    //    {
    //        try
    //        {
    //            member = new DAL.Member();
    //            DataTable dataResult = new DataTable();
    //            PopulateDataLayer();
    //            dataResult = member.GetMembetDistancePerReleasePoint().Tables[0];
    //            return dataResult;
    //        }
    //        catch (Exception ex)
    //        {

    //            throw ex;
    //        }
    //    }

    }
}