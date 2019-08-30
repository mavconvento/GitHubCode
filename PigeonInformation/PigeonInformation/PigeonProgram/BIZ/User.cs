﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace PigeonProgram.BIZ
{
    class User
    {
        #region Constant
        #endregion

        #region Variable
        DAL.User user;
        #endregion

        #region Properties
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public String ClubName { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String ReTypePassword { get; set; }
        public String Version { get; set; }
        public byte[] Logo { get; set; }
        public String Name { get; set; }
        public string LoftName { get; set; }
        public String Address { get; set; }
        public String ContactNumber { get; set; }
        public Int64 Resolution { get; set; }
        public Int64 ResolutionY { get; set; }
        public String BackgroundImages { get; set; }
        #endregion

        #region Public Methods
        public DataSet Login()
        {
            try
            {
                DataSet dtResult;
                user = new DAL.User();
                PopulateDataLayer();
                dtResult = user.Login();
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet PedigreeSetup(Int64 userID)
        {
            try
            {
                DataSet dtResult;
                user = new DAL.User();
                UserID = userID;
                PopulateDataLayer();
                dtResult = user.PedigreeSetup();
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean Save()
        {
            try
            {
                Boolean status = false;
                if (Password == ReTypePassword)
                {

                    user = new DAL.User();
                    PopulateDataLayer();
                    user.Save();
                    MessageBox.Show("Username and Password Save!", "Record Save");
                    status = true;
                }
                else
                {
                    MessageBox.Show("Password not match");
                }
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean ChangePassword()
        {
            try
            {
                Boolean status = false;

                user = new DAL.User();
                PopulateDataLayer();
                user.ChangePassword();
                MessageBox.Show("New password set. Change password successful");
                status = true;

                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UserSelectAll(DataGridView UserList)
        {
            try
            {
                user = new DAL.User();
                PopulateDataLayer();
                UserList.DataSource = user.UserSelectAll().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean UserDelete()
        {
            try
            {
                Boolean status = false;
                user = new DAL.User();
                PopulateDataLayer();
                user.UserDelete();
                MessageBox.Show("Record Successfully Deleted!", "Delete Record");
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Private Methods
        private void PopulateDataLayer()
        {
            try
            {
                user.UserID = UserID;
                user.ClubID = ClubID;
                user.ClubName = ClubName;
                user.UserName = UserName;
                user.Password = Password;
                user.Version = Version;
                user.BLLUser = this;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}