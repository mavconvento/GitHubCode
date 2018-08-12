using System;
using System.Collections.Generic;

using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

using MAVCPigeonClockingMobileApps.Constants;
using LWT.Common.DAL;
using LWT.Common;


namespace MAVCPigeonClockingMobileApps.DAL
{
	public class ToolsDAL : BaseDAL
	{
		#region Variables
		private static Database database = LWTDatabase.GetInstance().GetDatabase(CommonConstants.DATABASE_NAME);
		#endregion Variables

		public static void CustomerFileNoteSave(int CustID, string Note, bool SystemNote )
		{
			try
			{
				DbCommand DbCommand = database.GetStoredProcCommand("lmsCustomerFileNoteAdd");

				database.AddInParameter(DbCommand, "@InstallationID", DbType.Int32, System.Web.HttpContext.Current.Application["installationid"]);
				database.AddInParameter(DbCommand, "@UserID", DbType.Int32, -2);
				database.AddInParameter(DbCommand, "@CustomerID", DbType.Int32, CustID);
				database.AddInParameter(DbCommand, "@Category", DbType.Int32, 36);	//customer notes for portal
				database.AddInParameter(DbCommand, "@Notes", DbType.String, Note);
				database.AddInParameter(DbCommand, "@Status", DbType.Int32, DBNull.Value);
				database.AddInParameter(DbCommand, "@SystemNote", DbType.Boolean, SystemNote);

				InternalExecuteNonQuery(database, DbCommand, null);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataSet GetLookUpItems(Int64 userID, Int64 lookupCategoryID, Int64 lookupID, string Value)
		{
			try
			{				
				DbCommand DbCommand = database.GetStoredProcCommand("shrLookUpItemGetBySearch");

				database.AddInParameter(DbCommand, "@InstallationID", DbType.Int32, System.Web.HttpContext.Current.Application["installationid"]);
				database.AddInParameter(DbCommand, "@UserID", DbType.Int32, -2);
				database.AddInParameter(DbCommand, "@CategoryID", DbType.Int32, lookupCategoryID);
				database.AddInParameter(DbCommand, "@LookupItemID", DbType.Int32, lookupID);
				database.AddInParameter(DbCommand, "@Value", DbType.String, Value);
				
				return InternalExecuteDataSet(database, DbCommand, null);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataSet GetClubList(string userID)
		{
			try
			{
                DbCommand DbCommand = database.GetStoredProcCommand("ClubSelectAll");
                database.AddInParameter(DbCommand, "@UserName", DbType.String, userID);
                //database.AddInParameter(DbCommand, "@NotExpired", DbType.Int32, userID);
                return InternalExecuteDataSet(database, DbCommand, null);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable GetSuburbs(string State, string Suburb)
		{
			try
			{
				DbCommand DbCommand = database.GetStoredProcCommand("lmsCustomerPortalSuburbsGet");
				database.AddInParameter(DbCommand, "@State", DbType.String, State);
				database.AddInParameter(DbCommand, "@Suburb", DbType.String, Suburb);

				return InternalExecuteDataSet(database, DbCommand, null).Tables[0];
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}