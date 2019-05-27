using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data;

namespace Eclock
{
    public partial class frmBirdTimeArrival : Form
    {
        DataTable MergeRaceResult;
        BIZ.Race BIZRace;

        public DataTable EntryList { get; set; }
        public Int64 RaceReleasePointID { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime StopTimeFrom { get; set; }
        public DateTime StopTimeTo { get; set; }
        public Boolean IsStop { get; set; }
        public Int64 MemberID { get; set; }
        public Int64 ClubID { get; set; }
        public string ClubName { get; set; }
        public string ClubFullName { get; set; }
        public DateTime CutOff { get; set; }
        public Decimal Distance { get; set; }
        public String Mode { get; set; }

        public frmBirdTimeArrival()
        {
            InitializeComponent();
        }

        private void BirdTimeArrival_Load(object sender, EventArgs e)
        {
            try
            {
                if (Mode == "TrainingMode") this.btnSubmitResult.Visible = false;
                Initialize();
                MergingRaceEntry();
            }
            catch (Exception ex)
            {

                MessageBox.Show(BIZ.Common.CustomError(ex.Message),"Error");
            }
        }

        private void Initialize()
        {
            try
            {
                //Add Column in Merge Race Result table table
                MergeRaceResult = new DataTable();
                MergeRaceResult.Columns.Add("SerialRFIDNo");
                MergeRaceResult.Columns.Add("BandNumber");
                MergeRaceResult.Columns.Add("ArrivalTime");
                MergeRaceResult.Columns.Add("Flight");
                MergeRaceResult.Columns.Add("Speed (mpm)");
                MergeRaceResult.Columns.Add("Remarks");
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void BindDataGrid()
        {
            try
            {
                dataGridView1.DataSource = MergeRaceResult;
                dataGridView1.Columns["SerialRFIDNo"].Visible = false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private string ResultRemarks(DateTime Arrival)
        {
            try
            {
                TimeSpan timespan = CutOff.Subtract(Arrival);
                if (timespan.TotalSeconds < 0)
                {
                    return "CUT-OFF";
                }

                return "";
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void MergingRaceEntry()
        {
            try
            {
                int TotalBirdArrived = 0;
                bool foundBird = false;
                foreach (DataRow item in EntryList.Rows)
                {
                    DateTime indexDate = ReleaseDate;
                    while (indexDate <= CutOff)
                    {
                        string ApplicationDirectory = BIZ.Common.GetApplicationDirectory();
                        string TodayFolder = indexDate.Year.ToString() + "_" + indexDate.Month.ToString() + "_" + indexDate.Day.ToString();
                        string WithTimeRootDirectory = ApplicationDirectory + "DataCollection\\Member\\Raceresult\\"  + Mode + "\\WithTime\\" + TodayFolder;
                        string fullpath = WithTimeRootDirectory + "\\" + item["SerialRFIDNo"].ToString() + ".inf";
                        if (File.Exists(fullpath))
                        {
                            TextReader tr = new StreamReader(fullpath);
                            using (tr)
                            {
                                string[] content = BIZ.Common.Decrypt(tr.ReadLine()).Split('|');
                                DataRow dr = MergeRaceResult.NewRow();
                                dr["SerialRFIDNo"] = item["SerialRFIDNo"].ToString();
                                dr["BandNumber"] = item["BandNumber"].ToString();
                                dr["ArrivalTime"] = content[1].ToString();

                                //flight always false to get the exact flight of the bird base on arrival
                                dr["Flight"] = BIZ.Common.Flight(ReleaseDate, Convert.ToDateTime(content[1].ToString()), StopTimeFrom, StopTimeTo, false);

                                //flight base on stop time to get the exact speed of the bird base on arrival - less timespan
                                dr["Speed (mpm)"] = BIZ.Common.Speed(Distance, BIZ.Common.Flight(ReleaseDate, Convert.ToDateTime(content[1].ToString()), StopTimeFrom, StopTimeTo, IsStop));
                                
                                //get remarks of result
                                if (Mode == "RaceMode") dr["Remarks"] = ResultRemarks(Convert.ToDateTime(content[1].ToString()));
                                MergeRaceResult.Rows.Add(dr);
                                TotalBirdArrived += 1;
                                foundBird = true;
                            };
                        }
                        indexDate = indexDate.AddDays(1);
                    }

                    if (!foundBird)
                       {
                           DataRow dr = MergeRaceResult.NewRow();
                           dr["BandNumber"] = item["BandNumber"].ToString();
                           dr["Remarks"] = "NOT ARRIVE";
                           MergeRaceResult.Rows.Add(dr);
                    }
                    foundBird = false;
                }

                txtTotal.Text = "Total Bird Arrived : " + TotalBirdArrived;
                BindDataGrid();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        private void btnSubmitResult_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtResult = (DataTable)dataGridView1.DataSource;
                BIZRace = new BIZ.Race();
                foreach (DataRow item in dtResult.Rows)
                {
                    if (item["Remarks"] != "NOT ARRIVE")
                    {
                        BIZRace.ClubID = ClubID;
                        BIZRace.MemberID = MemberID;
                        BIZRace.RaceReleasePointID = RaceReleasePointID;
                        BIZRace.ArrivalTime = Convert.ToDateTime(item["ArrivalTime"]);
                        BIZRace.SerialRFIDNo = item["SerialRFIDNo"].ToString();
                        BIZRace.SubmitRaceResult();

                        //string ApplicationDirectory = BIZ.Common.GetApplicationDirectory();
                        //string TodayFolder = indexDate.Year.ToString() + "_" + indexDate.Month.ToString() + "_" + indexDate.Day.ToString();
                        //string WithTimeRootDirectory = ApplicationDirectory + "DataCollection\\Member\\Raceresult\\" + Mode + "\\WithTime\\" + TodayFolder;
                        //string fullpath = WithTimeRootDirectory + "\\" + item["SerialRFIDNo"].ToString() + ".inf";

                        //if (!File.Exists(fullpath)) File.Create(fullpath).Close();

                        //using (System.IO.StreamWriter file = new System.IO.StreamWriter(fullpath, true))
                        //{
                        //    file.WriteLine(DateTime.Now);
                        //    file.Close();
                        //};

                    } 
                }

                MessageBox.Show("Race Result Summitted.");
            }
            catch (Exception ex)
            {
                
                 MessageBox.Show(BIZ.Common.CustomError(ex.Message),"Error");
            }
        }
    }
}
