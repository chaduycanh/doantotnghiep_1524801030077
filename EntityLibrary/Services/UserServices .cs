using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary
{
    public class UserServices
    {
        DOANTOTNGHIEPEntities entity = new DOANTOTNGHIEPEntities();
        //public dynamic Save(ExtratimeActive data)
        //{
        //    try
        //    {
        //        entity.ExtratimeActives.Add(data);
        //        entity.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }

        //}
        //public dynamic Delete(long Id)
        //{
        //    try
        //    {
        //        var data = entity.ExtratimeActives.Where(p => p.PID == Id).FirstOrDefault();
        //        entity.ExtratimeActives.Remove(data);
        //        entity.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }

        //}
        //public dynamic Update(ExtratimeActive data)
        //{
        //    try
        //    {
        //        entity.ExtratimeActives.Add(data);
        //        entity.Entry(data).State= System.Data.Entity.EntityState.Modified;
        //        entity.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }

        //}
        public List<dynamic> GetAllData()
        {
            try
            {
                List<dynamic> temp = new List<dynamic>();

                var data = (from p in entity.UserLogins
                              join e in entity.Roles
                              on p.Role equals e.Code
                            where p.STATUS==1
                              select new
                              {
                                  pid=p.PID,
                                  fullName = p.FullName,
                                  role = e.Name,
                                  codeRole = p.Role,
                                  msgv =p.Code
                              }).ToList();
                foreach (var tempData in data)
                {
                    temp.Add(tempData);
                }
                return temp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<Role> GetAllRole()
        {
            try
            {
                return entity.Roles.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //public ExtratimeActive GetDataById(long id)
        //{
        //    try
        //    {
        //        var list = entity.ExtratimeActives.Where(p=>p.PID==id).FirstOrDefault();
        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
    }
}
