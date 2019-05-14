using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary
{
    public class AssessServices
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
        public List<PersonalExtratimeActive> GetAllData()
        {
            try
            {
                var list = entity.PersonalExtratimeActives.Where(p => p.Status != 1 && p.Status != -1).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public PersonalExtratimeActive GetDataById(long id)
        {
            try
            {
                var list = entity.PersonalExtratimeActives.Where(p => p.Pid == id).FirstOrDefault();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
