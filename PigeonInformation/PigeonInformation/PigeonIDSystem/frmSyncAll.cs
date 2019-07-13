using Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;

namespace PigeonIDSystem
{
    public partial class frmSyncAll : Form
    {
        public String ClubName { get; set; }
        public DataTable DataList { get; set; }
        public String ActionType { get; set; }
        public DateTime DateRelease { get; set; }
        public String ActionTypeDescription { get; set; }

        public frmSyncAll()
        {
            InitializeComponent();
            dtList.DoubleClick += new EventHandler(grid_DoubleClick);
        }

        private void SyncAll_Load(object sender, EventArgs e)
        {
            this.label1.Text = ActionTypeDescription + " Sync to Database Error Logs";
            string pathSyncApplication = AppDomain.CurrentDomain.BaseDirectory + "SyncApplication";
            if (ActionType == "ENTRYDB")
            {
                string filepath = pathSyncApplication + "\\entrylogs.txt";
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                ReadEntryLogs();
            }
            else if (ActionType == "RESULTDB")
            {
               
                string filepath = pathSyncApplication + "\\resultlogs.txt";
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                ReadResultLogs();
            }
            
        }

        private void grid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataGridView datagrid = this.dtList;
                int index;
                //Int64 PigeonID;
                string MemberID = "";
                String RFID = "";
                String OldRemarks = "";
                if (datagrid.RowCount > 0)
                {
                    index = datagrid.CurrentRow.Index;
                    RFID = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[2].Value);
                    MemberID = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[1].Value);
                    OldRemarks = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[4].Value);
                    if (RFID != "")
                    {
                        if ((string)datagrid.CurrentCell.Value.ToString() == "RETRY")
                        {
                            if (ActionType  == "ENTRYDB")
                            {
                                RetryEntry(RFID, MemberID, OldRemarks);
                            }
                            else if (ActionType == "RESULTDB")
                            {
                                RetryResult(RFID, MemberID, OldRemarks);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void RetryEntry(string RFID,string MemberID, string OldRemarks)
        {
            try
            {
                string path = ReadText.ReadFilePath("datapath");
                string dateString = this.DateRelease.Year.ToString() + this.DateRelease.Month.ToString().PadLeft(2, '0') + this.DateRelease.Day.ToString().PadLeft(2, '0');

                string entryDirectory = path + "entry\\" + dateString;

                string pathSyncApplication = AppDomain.CurrentDomain.BaseDirectory + "SyncApplication";
                string EntryLogs = pathSyncApplication + "\\entrylogs.txt";

                string bandedFileName = path + "PigeonDetails\\" + MemberID + "\\" + RFID + ".txt";
                if (File.Exists(bandedFileName))
                {
                    string[] pigeondetails = ReadText.ReadTextFile(bandedFileName);
                    BusinessLayer.EclockEntryBLL entryBll = new EclockEntryBLL();
                    DomainObjects.Entry entryObject = new DomainObjects.Entry();
                    entryObject.Clubname = ClubName;
                    entryObject.MemberIDNo = MemberID;
                    entryObject.ReleaseDate = this.DateRelease;
                    entryObject.RingNumber = pigeondetails[0];
                    entryObject.RaceCategoryName = pigeondetails[2];
                    entryObject.RaceCategoryGroupName = "EClock";
                    entryObject.RFID = RFID;

                    DataSet dtResult = new DataSet();
                    dtResult = entryBll.EclockEntrySave(entryObject);

                    if (dtResult.Tables.Count > 0)
                    {
                        if (dtResult.Tables[0].Rows.Count > 0)
                        {
                            String Remarks = dtResult.Tables[0].Rows[0]["Remarks"].ToString();

                            if (Remarks.ToUpper() != "SUCCESS")
                            {
                                String LogContents = MemberID + "|" + RFID + "|" + pigeondetails[0] + "|" + Remarks + "|";
                                string removeFromLogsPath = pathSyncApplication + "\\entrylogs.txt";
                                if (!CheckRemarks(LogContents, removeFromLogsPath))
                                {
                                    if (File.Exists(EntryLogs))
                                    {
                                        using (StreamWriter sw = File.AppendText(EntryLogs))
                                        {
                                            sw.WriteLine(LogContents);
                                        }
                                    }
                                    else
                                    {
                                        using (StreamWriter sw = File.CreateText(EntryLogs))
                                        {
                                            sw.WriteLine(LogContents);
                                        }
                                    }
                                }

                                MessageBox.Show("Error detected.", "Error");
                            }
                            else
                            {
                                string removeFromLogsPath = pathSyncApplication + "\\entrylogs.txt";
                                String removeFromLogs = MemberID + "|" + RFID + "|" + pigeondetails[0] + "|" + OldRemarks + "|";
                                UpdateLogs(removeFromLogs, removeFromLogsPath);
                                MessageBox.Show("Eclock entry sync to database.", "Eclock Sync");
                            }
                        }

                    }

                }
                ReadEntryLogs();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void RetryResult(string RFID, string MemberID, string OldRemarks)
        {
            try
            {
                string path = ReadText.ReadFilePath("datapath");
                string pathSyncApplication = AppDomain.CurrentDomain.BaseDirectory + "SyncApplication";

                string dateString = this.DateRelease.Year.ToString() + this.DateRelease.Month.ToString().PadLeft(2, '0') + this.DateRelease.Day.ToString().PadLeft(2, '0');

                string resultDirectory = path + "result\\" + dateString;
                string resultLogs = pathSyncApplication + "\\resultlogs.txt";

                string resultFileName = resultDirectory + "\\" + MemberID + "\\" + RFID + ".txt";
                if (File.Exists(resultFileName))
                {
                    string[] resultdetails = ReadText.ReadTextFile(resultFileName);
                    BusinessLayer.ResultBLL entryBll = new ResultBLL();

                    //sample ECLOCK 0001 15204188 19/07/05 07:48:18
                    String ResultStringFormat = "ECLOCK " + resultdetails[0] + " " + resultdetails[1] + " " + resultdetails[3] + " " + resultdetails[4];
                    DomainObjects.Result dObject = new DomainObjects.Result
                    {
                        ClubName = ClubName,
                        SMSContent = ResultStringFormat,
                        ActionFrom = "E-Clock Apps"
                    };

                    DataSet dtResult = new DataSet();
                    dtResult = entryBll.EclockResultSave(dObject);

                    if (dtResult.Tables.Count > 0)
                    {
                        if (dtResult.Tables[0].Rows.Count > 0)
                        {
                            String Remarks = dtResult.Tables[0].Rows[0]["Remarks"].ToString();

                            if (Remarks.ToUpper() != "SUCCESS")
                            {
                                String LogContents = MemberID + "|" + RFID + "|" + resultdetails[3] + "|" + Remarks + "|";
                                string removeFromLogsPath = pathSyncApplication + "\\resultlogs.txt";
                                if (!CheckRemarks(LogContents, removeFromLogsPath))
                                {
                                    if (File.Exists(resultLogs))
                                    {
                                        using (StreamWriter sw = File.AppendText(resultLogs))
                                        {
                                            sw.WriteLine(LogContents);
                                        }
                                    }
                                    else
                                    {
                                        using (StreamWriter sw = File.CreateText(resultLogs))
                                        {
                                            sw.WriteLine(LogContents);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //string pathSyncApplication = AppDomain.CurrentDomain.BaseDirectory + "SyncApplication";
                                string removeFromLogsPath = pathSyncApplication + "\\resultlogs.txt";

                                String removeFromLogs = MemberID + "|" + RFID + "|" + resultdetails[3] + "|" + OldRemarks + "|";
                                UpdateLogs(removeFromLogs, removeFromLogsPath);
                                MessageBox.Show("Eclock entry sync to database.", "Eclock Sync");
                            }
                        }
                    }
                }
                ReadEntryLogs();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void ReadEntryLogs()
        {
            try
            {
                DataTable entrylistlogs = new DataTable();

                DataColumn dc1 = new DataColumn();
                dc1.ColumnName = "Retry";

                DataColumn dc2 = new DataColumn();
                dc2.ColumnName = "MemberIDNo";

                DataColumn dc3 = new DataColumn();
                dc3.ColumnName = "TagID";

                DataColumn dc4 = new DataColumn();
                dc4.ColumnName = "Ring";

                DataColumn dc5 = new DataColumn();
                dc5.ColumnName = "Remarks";

                entrylistlogs.Columns.Add(dc1);
                entrylistlogs.Columns.Add(dc2);
                entrylistlogs.Columns.Add(dc3);
                entrylistlogs.Columns.Add(dc4);
                entrylistlogs.Columns.Add(dc5);

                //string path = ReadText.ReadFilePath("datapath");
                //string dateString = this.DateRelease.Year.ToString() + this.DateRelease.Month.ToString().PadLeft(2, '0') + this.DateRelease.Day.ToString().PadLeft(2, '0');

                string pathSyncApplication = AppDomain.CurrentDomain.BaseDirectory + "SyncApplication";
                string filepath = pathSyncApplication + "\\entrylogs.txt";

                if (File.Exists(filepath))
                {
                    string[] pigeonCollection = ReadText.ReadTextFile(filepath);

                    foreach (string item in pigeonCollection)
                    {
                        string[] value = item.Split('|');
                        DataRow dr = entrylistlogs.NewRow();
                        dr["RETRY"] = "RETRY";
                        dr["MemberIDNo"] = value[0].ToString();
                        dr["TagID"] = value[1].ToString();
                        dr["Ring"] = value[2].ToString();
                        dr["Remarks"] = value[3].ToString();
                        entrylistlogs.Rows.Add(dr);
                    }

                }

                dtList.DataSource = entrylistlogs;
                //lblcount.Text = "Total Birds: " + entrylistlogs.Rows.Count.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void ReadResultLogs()
        {
            try
            {
                DataTable entrylistlogs = new DataTable();

                DataColumn dc1 = new DataColumn();
                dc1.ColumnName = "Retry";

                DataColumn dc2 = new DataColumn();
                dc2.ColumnName = "MemberIDNo";

                DataColumn dc3 = new DataColumn();
                dc3.ColumnName = "TagID";

                DataColumn dc4 = new DataColumn();
                dc4.ColumnName = "Ring";

                DataColumn dc5 = new DataColumn();
                dc5.ColumnName = "Remarks";

                entrylistlogs.Columns.Add(dc1);
                entrylistlogs.Columns.Add(dc2);
                entrylistlogs.Columns.Add(dc3);
                entrylistlogs.Columns.Add(dc4);
                entrylistlogs.Columns.Add(dc5);

                //string path = ReadText.ReadFilePath("datapath");
                //string dateString = this.DateRelease.Year.ToString() + this.DateRelease.Month.ToString().PadLeft(2, '0') + this.DateRelease.Day.ToString().PadLeft(2, '0');

                string pathSyncApplication = AppDomain.CurrentDomain.BaseDirectory + "SyncApplication";
                string filepath = pathSyncApplication + "\\resultlogs.txt";

                if (File.Exists(filepath))
                {
                    string[] pigeonCollection = ReadText.ReadTextFile(filepath);

                    foreach (string item in pigeonCollection)
                    {
                        string[] value = item.Split('|');
                        DataRow dr = entrylistlogs.NewRow();
                        dr["RETRY"] = "RETRY";
                        dr["MemberIDNo"] = value[0].ToString();
                        dr["TagID"] = value[1].ToString();
                        dr["Ring"] = value[2].ToString();
                        dr["Remarks"] = value[3].ToString();
                        entrylistlogs.Rows.Add(dr);
                    }

                }

                dtList.DataSource = entrylistlogs;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private bool CheckRemarks(string remarks,string filepath)
        {
            try
            {
                if (File.Exists(filepath))
                {
                    string[] pigeonCollection = ReadText.ReadTextFile(filepath);
                    if (pigeonCollection.Contains(remarks)) return true;
                }

                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void UpdateLogs(string remarks,string filepath)
        {
            try
            {
                if (File.Exists(filepath))
                {
                    string[] pigeonCollection = ReadText.ReadTextFile(filepath);
                    File.Delete(filepath);

                    foreach (var item in pigeonCollection)
                    {
                        if (item != remarks)
                        {

                            if (File.Exists(filepath))
                            {
                                using (StreamWriter sw = File.AppendText(filepath))
                                {
                                    sw.WriteLine(item);
                                }
                            }
                            else
                            {
                                using (StreamWriter sw = File.CreateText(filepath))
                                {
                                    sw.WriteLine(item);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmSyncEclock sync = new frmSyncEclock();
            sync.ClubName = ClubName;
            sync.DateRelease = DateRelease;
            sync.ActionType = ActionType;
            sync.ShowDialog();

            if (ActionType == "ENTRYDB")
            {
                ReadEntryLogs();
            }
            else if (ActionType == "RESULTDB")
            {
                ReadResultLogs();
            }

            if (dtList.Rows.Count == 0)
            {
                MessageBox.Show("Eclock " + ActionTypeDescription + " sync to database successfully.", "Sync All");
            }
            else
            {
                MessageBox.Show("Eclock " + ActionTypeDescription + " sync to database error detected.", "Sync All Error");
            }
        }
    }
}