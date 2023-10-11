using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DomainObjects;

namespace BusinessLayer
{
    public class MemberBLL
    {

        public DataTable GetMemberName(string memberid)
        {
            try
            {
                DataLayer.MemberDal dal = new MemberDal();
                return dal.GetMemberInfo(memberid);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetPigeonDetails(string rfid)
        {
            try
            {
                DataLayer.MemberDal dal = new MemberDal();
                return dal.GetPigeonInfo(rfid);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetPigeonInfoByRingNo(string ring, string memberid)
        {
            try
            {
                DataLayer.MemberDal dal = new MemberDal();
                return dal.GetPigeonInfoByRingNo(ring,memberid);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable GetPigeonList(string memberid)
        {
            try
            {
                DataLayer.MemberDal dal = new MemberDal();
                return dal.GetPigeonList(memberid);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetMemberWithMultipleClub()
        {
            try
            {
                DataLayer.MemberDal dal = new MemberDal();
                return dal.GetMemberWithMultipleClub();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void MemberSave(string memberid, bool isMultiple)
        {
            try
            {
                DataLayer.MemberDal dal = new MemberDal();
                dal.MemberSave(memberid, isMultiple);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void PigeonSave(string pigeonId, string memberid, bool isRegistered, string ering)
        {
            try
            {
                DataLayer.MemberDal dal = new MemberDal();
                dal.PigeonSave(pigeonId, memberid, isRegistered, ering);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
