using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary
{
    public class EmailServices
    {
        DOANTOTNGHIEPEntities entity = new DOANTOTNGHIEPEntities();
        public bool SendMail(string user)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Supporter", "canh.cd97@gmail.com"));
                message.To.Add(new MailboxAddress(@"Châu D. Cảnh", "y3siamc@gmail.com"));
                message.Subject = "Password Recovery!!!";

                message.Body = new TextPart("plain")
                {
                    Text = @"Bạn nhận được mail này vì muốn reset password click vào link sau để thực hiện reset đặt lại password"
                };

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
