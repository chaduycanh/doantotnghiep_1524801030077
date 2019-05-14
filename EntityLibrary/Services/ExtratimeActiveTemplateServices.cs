using MailKit.Net.Smtp;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary
{
    public class ExtratimeActiveTemplateServices
    {
        DOANTOTNGHIEPEntities entity = new DOANTOTNGHIEPEntities();
        public dynamic Save(ExtratimeActive data)
        {
            using (var dbContextTransaction = entity.Database.BeginTransaction())
            {
                dynamic xa = JsonConvert.DeserializeObject(data.Participant);
                var a = xa[0].Value;
                var n = xa.Count;
                // List<string> teacherCode = new List<string>();

                try
                {

                    entity.ExtratimeActives.Add(data);
                    entity.SaveChanges();
                    List<Attendance> listAttendance = new List<Attendance>();
                    for (int i = 0; i < n; i++)
                    {
                        Attendance attendance = new Attendance();
                        attendance.TeacherCode = xa[i].Value;
                        attendance.ActiveCode = data.PID;
                        listAttendance.Add(attendance);

                    }
                    entity.Attendances.AddRange(listAttendance);
                    entity.SaveChanges();
                    dbContextTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return false;
                }
            }

        }
        public dynamic Delete(long Id)
        {
            try
            {
                var data = entity.ExtratimeActives.Where(p => p.PID == Id).FirstOrDefault();
                entity.ExtratimeActives.Remove(data);
                entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public dynamic Update(ExtratimeActive data)
        {

            using (var dbContextTransaction = entity.Database.BeginTransaction())
            {
                try
                {
                    var model2 = entity.Attendances.Where(p => p.ActiveCode == data.PID).ToList();
                    entity.Attendances.RemoveRange(model2);
                    entity.SaveChanges();
                    dynamic xa = JsonConvert.DeserializeObject(data.Participant);
                    var a = xa[0].Value;
                    var n = xa.Count;
                    List<Attendance> listAttendance = new List<Attendance>();
                    for (int i = 0; i < n; i++)
                    {
                        Attendance attendance = new Attendance();
                        attendance.TeacherCode = xa[i].Value;
                        attendance.ActiveCode = data.PID;
                        listAttendance.Add(attendance);

                    }
                    entity.Attendances.AddRange(listAttendance);
                    entity.ExtratimeActives.Add(data);
                    entity.Entry(data).State = System.Data.Entity.EntityState.Modified;
                    entity.SaveChanges();
                    dbContextTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return false;
                }
            }

        }
        public List<ExtratimeActive> GetAllData()
        {
            try
            {
                var list = entity.ExtratimeActives.ToList();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public ExtratimeActive GetDataById(long id)
        {
            try
            {
                var list = entity.ExtratimeActives.Where(p => p.PID == id).FirstOrDefault();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<ExtratimeActive> GetDataExtraActiveToSendMail(DateTime fromDate, DateTime toDate)
        {
            try
            {

                var modal = entity.ExtratimeActives.Where(p => p.Date >= fromDate && p.Date < toDate).ToList();
                return modal;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<Attendance> GetDataJoinerToSendMail(long code)
        {
            try
            {

                var modal = entity.Attendances.Where(p => p.ActiveCode == code).ToList();
                return modal;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string GetName(string code)
        {
            try
            {

                var modal = entity.UserLogins.Where(p => p.Code == code).FirstOrDefault();
                return modal.FullName;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string GetEmail(string code)
        {
            try
            {

                var modal = entity.UserLogins.Where(p => p.Code == code).FirstOrDefault();
                return modal.USER;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool SendMail(string week, string year,string path,List<UserMail> user)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Supporter", "canh.cd97@gmail.com"));
                message.To.Add(new MailboxAddress(@"Châu D. Cảnh", "y3siamc@gmail.com"));
                foreach (var item in user)
                {
                    message.To.Add(new MailboxAddress(item.name, item.email));
                }
            
           
                message.Subject = "Lịch công tác khác tuần " + week + " năm " + year + "!";

         
                var builder = new BodyBuilder();

                // Set the plain-text version of the message text
                builder.TextBody = @"Lịch công tác được đính kèm bên dưới";
                builder.Attachments.Add(path);
                message.Body = builder.ToMessageBody();
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587);


                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate("canh.cd97@gmail.com", "123456789Ca@");

                    client.Send(message);
                    client.Disconnect(true);
                }
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
