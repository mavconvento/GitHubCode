using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PegionClocking.Common;
using PegionClocking.DAL;
using System.IO;

namespace PegionClocking
{
    public partial class frmMain : Form
    {

        #region Constants
        #endregion

        #region Variables

        #endregion

        #region Properties
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public Boolean IsAdmin { get; set; }
        #endregion

        #region Events
        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            //this.Hide();
            Common.Global.ClubNameConnection = "";
            Login();
        }
        private void newMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.MemberDataEntry);  //select Member
        }
        private void masterlistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.MemberMasterlist);  //select Member Masterlist
        }
        private void raceEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.RaceEntry);  //select Member Masterlist
        }
        private void locationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.Location); //select Location
        }
        private void scheduleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.Schedule); //select Schedule
        }
        private void scheduleDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.RaceScheduleDetails); //select race schedule details
        }
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login();
        }
        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.RaceCategory); //select race category details
        }
        private void groupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.RaceCategoryGroup); //select race category group details
        }
        private void userToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.User); //select user
        }
        private void clubToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.Club); //select Club
        }
        private void releaseDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.RaceResult); //select Race Result
        }
        private void scheduleCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.RaceResults); //select Race Result
        }
        private void memberRingManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.RingMangement); //select Race Result
        }
        private void regionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.Region); //select region
        }
        private void calculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.Calculator); //select region
        }
        private void inboxViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.InboxView); //select region
        }
        private void releasePointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.ByReleasePoint);
        }
        private void byMemberIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.ByMemberID);
        }
        private void generateStickerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.StickerGeneration);
        }

        private void transactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.Transaction);
        }

        private void transactionSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.TransactionHistory);
        }
        private void viewClubRaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.ViewClubRace);
        }
        private void exportEClockDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.ExportEClockData);
            
        }
        private void loanMAVCAccessCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.LoadMAVCAccessCard);
        }
        private void registerMobileNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.RegisterMobileNumber);
        }
        private void paymentSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.PaymentHistory);
        }
        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.ChangePassword);
        }
        private void readerRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.ReaderRegistration);
        }
        private void rFIDRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectMenu(Common.Common.MemberMenu.RFIDRegistration);
        }
        #endregion

        #region Private Methods
        private void Login()
        {
            try
            {
                ShowControl();
                frmLogin login = new frmLogin();
                login.ShowDialog();
                ClubID = login.ClubID;
                UserID = login.UserID;
                IsAdmin = login.IsAdmin;
                //Common.Global.ClubNameConnection = "_" + login.ClubName;

                if (login.ClubName != null)
                {
                    this.Text = "Main (" + login.ClubName.ToUpper() + ")";
                }


                if (UserID == 0)
                {
                    this.Close();
                }
                else
                {
                    if (!IsAdmin)
                    {
                        HideControl();
                    }
                    this.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void ShowControl()
        {
            this.administratorTabToolStripMenuItem.Visible = true;
            this.archiveToolStripMenuItem.Visible = true;
            this.inboxViewToolStripMenuItem.Visible = true;
            this.regionToolStripMenuItem.Visible = true;
            this.generateStickerToolStripMenuItem.Visible = true;
            this.readerRegistrationToolStripMenuItem.Visible = true;
            this.rFIDRegistrationToolStripMenuItem.Visible = true;
            this.transactionToolStripMenuItem.Visible = true;
            this.viewClubRaceToolStripMenuItem.Visible = true;
            this.loanMAVCAccessCardToolStripMenuItem.Visible = true;
            this.changePasswordToolStripMenuItem.Visible = true;
        }
        private void HideControl()
        {
            this.administratorTabToolStripMenuItem.Visible = false;
            this.archiveToolStripMenuItem.Visible = false;
            this.generateStickerToolStripMenuItem.Visible = false;
            this.readerRegistrationToolStripMenuItem.Visible = false;
            this.rFIDRegistrationToolStripMenuItem.Visible = false;
            this.transactionToolStripMenuItem.Visible = false;
            this.regionToolStripMenuItem.Visible = false;
            this.viewClubRaceToolStripMenuItem.Visible = false;
            this.exportEClockDataToolStripMenuItem.Visible = false;
        }
        private void SelectMenu(Common.Common.MemberMenu selectedMenu)
        {
            switch (selectedMenu)
            {
                case Common.Common.MemberMenu.ReaderRegistration:
                    frmGenerateEclockInfo readerRegistration = new frmGenerateEclockInfo();
                    readerRegistration.ShowDialog();
                    break;

                case Common.Common.MemberMenu.RFIDRegistration:
                    frmRFIDRegistration rfidRegistration = new frmRFIDRegistration();
                    rfidRegistration.ShowDialog();
                    break;

                case Common.Common.MemberMenu.ChangePassword:
                    frmChangePassword changePassword = new frmChangePassword();
                    changePassword.ClubID = ClubID;
                    changePassword.UserID = UserID;
                    changePassword.ShowDialog();
                    break;

                case Common.Common.MemberMenu.ByReleasePoint:
                    frmMemberPerReleasePoint memberDistancePerReleasePoint = new frmMemberPerReleasePoint();
                    memberDistancePerReleasePoint.ClubID = ClubID;
                    memberDistancePerReleasePoint.UserID = UserID;
                    memberDistancePerReleasePoint.ShowDialog();
                    break;
                case Common.Common.MemberMenu.ByMemberID:
                    frmMemberDistance memberDistancePerMemberID = new frmMemberDistance();
                    memberDistancePerMemberID.ClubID = ClubID;
                    memberDistancePerMemberID.UserID = UserID;
                    memberDistancePerMemberID.ShowDialog();
                    break;
                case Common.Common.MemberMenu.MemberDataEntry:
                    frmMemberDataEntry memberDataEntry = new frmMemberDataEntry();
                    memberDataEntry.ClubID = ClubID;
                    memberDataEntry.UserID = UserID;
                    memberDataEntry.ShowDialog();
                    break;
                case Common.Common.MemberMenu.MemberMasterlist:
                    frmMemberMasterlist memberMasterlist = new frmMemberMasterlist();
                    memberMasterlist.ClubID = ClubID;
                    memberMasterlist.UserID = UserID;
                    memberMasterlist.ShowDialog();
                    break;
                case Common.Common.MemberMenu.RaceEntry:
                    frmRaceEntry raceEntry = new frmRaceEntry();
                    raceEntry.ClubID = ClubID;
                    raceEntry.UserID = UserID;
                    raceEntry.ShowDialog();
                    break;
                case Common.Common.MemberMenu.TransactionHistory:
                    frmTransactionSummary transactionHistory = new frmTransactionSummary();
                    transactionHistory.ClubID = ClubID;
                    transactionHistory.ShowDialog();
                    break;
                case Common.Common.MemberMenu.RaceResult:
                    frmRaceResult raceResult = new frmRaceResult();
                    raceResult.ClubID = ClubID;
                    raceResult.UserID = UserID;
                    raceResult.ShowDialog();
                    break;
                case Common.Common.MemberMenu.RaceResults:
                    frmRaceResults raceResults = new frmRaceResults();
                    raceResults.ClubID = ClubID;
                    raceResults.UserID = UserID;
                    raceResults.ShowDialog();
                    break;
                case Common.Common.MemberMenu.Location:
                    frmLocation location = new frmLocation();
                    location.ClubID = ClubID;
                    location.UserID = UserID;
                    location.ShowDialog();
                    break;
                case Common.Common.MemberMenu.Club:
                    frmClub club = new frmClub();
                    club.ClubID = ClubID;
                    club.UserID = UserID;
                    club.ShowDialog();
                    break;
                case Common.Common.MemberMenu.Schedule:
                    frmSchedule schedule = new frmSchedule();
                    schedule.ClubID = ClubID;
                    schedule.UserID = UserID;
                    schedule.ShowDialog();
                    break;
                case Common.Common.MemberMenu.RaceScheduleDetails:
                    frmScheduleDetails scheduleDetails = new frmScheduleDetails();
                    scheduleDetails.ClubID = ClubID;
                    scheduleDetails.UserID = UserID;
                    scheduleDetails.ShowDialog();
                    break;
                case Common.Common.MemberMenu.RaceCategory:
                    frmRaceCategory raceCategory = new frmRaceCategory();
                    raceCategory.ClubID = ClubID;
                    raceCategory.UserID = UserID;
                    raceCategory.ShowDialog();
                    break;
                case Common.Common.MemberMenu.RaceCategoryGroup:
                    frmRaceCategoryGroup raceCategoryGroup = new frmRaceCategoryGroup();
                    raceCategoryGroup.ClubID = ClubID;
                    raceCategoryGroup.UserID = UserID;
                    raceCategoryGroup.ShowDialog();
                    break;
                case Common.Common.MemberMenu.User:
                    frmUser user = new frmUser();
                    //user.ClubID = ClubID;
                    //user.UserID = UserID;
                    user.ShowDialog();
                    break;
                case Common.Common.MemberMenu.RingMangement:
                    frmMemberRingManagement ringManagement = new frmMemberRingManagement();
                    ringManagement.ClubID = ClubID;
                    ringManagement.UserID = UserID;
                    ringManagement.ShowDialog();
                    break;
                case Common.Common.MemberMenu.Region:
                    frmRegion region = new frmRegion();
                    region.ClubID = ClubID;
                    region.UserID = UserID;
                    region.ShowDialog();
                    break;
                case Common.Common.MemberMenu.Calculator:
                    frmCalculator calculator = new frmCalculator();
                    calculator.ClubID = ClubID;
                    calculator.UserID = UserID;
                    calculator.ShowDialog();
                    break;
                case Common.Common.MemberMenu.InboxView:
                    Inbox_View inbox = new Inbox_View();
                    inbox.ClubID = ClubID;
                    inbox.UserID = UserID;
                    inbox.ShowDialog();
                    break;
                case Common.Common.MemberMenu.StickerGeneration:
                    frmStickerGeneration stickerNumber = new frmStickerGeneration();
                    stickerNumber.ShowDialog();
                    break;
                case Common.Common.MemberMenu.ViewClubRace:
                    frmViewClubRace viewClubRace = new frmViewClubRace();
                    viewClubRace.ShowDialog();
                    break;
                case Common.Common.MemberMenu.Transaction:
                    frmTransaction transaction = new frmTransaction();
                    transaction.ShowDialog();
                    break;
                case Common.Common.MemberMenu.ExportEClockData:
                    frmExportEclockData ExportEClockData = new frmExportEclockData();
                    ExportEClockData.ClubID = ClubID;
                    ExportEClockData.UserID = UserID;
                    ExportEClockData.ShowDialog();
                    break;
                case Common.Common.MemberMenu.LoadMAVCAccessCard:
                    frmLoadCard LoanMavcCard = new frmLoadCard();
                    LoanMavcCard.ClubID = ClubID;
                    LoanMavcCard.UserID = UserID;
                    LoanMavcCard.ShowDialog();
                    break;
                case Common.Common.MemberMenu.RegisterMobileNumber:
                    frmRegisterMobileNumber RegisterMobileNumber = new frmRegisterMobileNumber();
                    RegisterMobileNumber.ClubID = ClubID;
                    RegisterMobileNumber.UserID = UserID;
                    RegisterMobileNumber.ShowDialog();
                    break;
                case Common.Common.MemberMenu.PaymentHistory:
                    frmPaymentTransactionSummary PaymentSummary = new frmPaymentTransactionSummary();
                    PaymentSummary.ClubID = ClubID;
                    PaymentSummary.UserID = UserID;
                    PaymentSummary.ShowDialog();
                    break;
            }
            
            Common.Global.IsMainDatabase = false;
        }
        #endregion

        private void inbox1ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Archive archive = new Archive();
                archive.ArchiveInbox1();
                MessageBox.Show("Inbox 1 successfully archive.", "Archive");
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }

        private void inbox2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Archive archive = new Archive();
                archive.ArchiveInbox2();
                MessageBox.Show("Inbox 2 successfully archive.", "Archive");
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }

        private void raceResultV1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Archive archive = new Archive();
                archive.ArchiveRaceResultV1();
                MessageBox.Show("Race Result V1 successfully archive.", "Archive");
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }

        private void raceResultV3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Archive archive = new Archive();
                archive.ArchiveRaceResultV3();
                MessageBox.Show("Race Result V3 successfully archive.", "Archive");
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }

        private void raceResultV4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Archive archive = new Archive();
                archive.ArchiveRaceResultV4();
                MessageBox.Show("Race Result V4 successfully archive.", "Archive");
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
    }
}
