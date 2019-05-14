using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary
{
    public class HomeServices
    {
        DOANTOTNGHIEPEntities entity = new DOANTOTNGHIEPEntities();

        public dynamic Check(long Id,int status)
        {
            try
            {
               
                var data = entity.PersonalExtratimeActives.Where(p => p.Pid == Id).FirstOrDefault();
                data.Status = status;
                entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public dynamic GetFile(long Id)
        {
            try
            {

                var data = entity.PersonalExtratimeActives.Where(p => p.Pid == Id).FirstOrDefault();
                return data.Proof;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<ExtratimeActive> GetAllDataCalender(string msgv)
        {
            try
            {
 
                var model = entity.ExtratimeActives.ToList();

                return model;
                //var modalUser = entity.UserLogins.Where(p => p.Code == msgv).FirstOrDefault();
                //if(modalUser.Role=="Admin")
                //{
                //    //var list = entity.PersonalExtratimeActives.Where(p => p.Status != 1 && p.Status != -1).ToList();
                //    return null;
                //}
                //else
                //{

            }
            catch (Exception ex)
            {
                return null;
            }
        }    
    }
}
