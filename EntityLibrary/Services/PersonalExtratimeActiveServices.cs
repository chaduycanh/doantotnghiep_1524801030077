using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary
{
    public class PersonalExtratimeActiveServices
    {
        DOANTOTNGHIEPEntities entity = new DOANTOTNGHIEPEntities();
        public dynamic Save(PersonalExtratimeActive data)
        {
            try
            {
                entity.PersonalExtratimeActives.Add(data);
                entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public dynamic Delete(long Id)
        {
            try
            {
               
                var data = entity.PersonalExtratimeActives.Where(p => p.Pid == Id).FirstOrDefault();
                if (data.Status != 0)
                {
                    return false;
                }
                    entity.PersonalExtratimeActives.Remove(data);
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
        public dynamic Update(PersonalExtratimeActive data)
        {
            try
            {
                var model = entity.PersonalExtratimeActives.Where(p => p.Pid == data.Pid).FirstOrDefault();
                if(model.Status!=0)
                {
                    return false;
                }
                model.KindActive = data.KindActive;
                model.Location = data.Location;
                model.Time = data.Time;
                model.Content = data.Content;         
                //entity.PersonalExtratimeActives.Add(data);
                //entity.Entry(data).State= System.Data.Entity.EntityState.Modified;
                entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public List<PersonalExtratimeActive> GetAllData()
        {
            try
            {
                var list = entity.PersonalExtratimeActives.ToList();
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
                var list = entity.PersonalExtratimeActives.Where(p=>p.Pid==id).FirstOrDefault();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public dynamic UpdateFile(PersonalExtratimeActive data)
        {
            try
            {
                var model = entity.PersonalExtratimeActives.Where(p => p.Pid == data.Pid).FirstOrDefault();
                if (model.Status != 0)
                {
                    return false;
                }
                model.Proof = model.Proof+data.Proof;
                //entity.PersonalExtratimeActives.Add(data);
                //entity.Entry(data).State= System.Data.Entity.EntityState.Modified;
                entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public dynamic DeleteFile(PersonalExtratimeActive data)
        {
            try
            {
                var model = entity.PersonalExtratimeActives.Where(p => p.Pid == data.Pid).FirstOrDefault();
                if (model.Status != 0)
                {
                    return false;
                }
                string url = string.Empty;

                var listProof = model.Proof.Remove(model.Proof.Length - 1).Split(';');
                foreach (var item in listProof)
                {
                    if (item != data.Proof)
                    {
                        url = url + item + ";";
                    }
                }
                model.Proof = url;
                entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
