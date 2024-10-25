using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Navy.Services
{
    internal class MailService
    {
        public static void Send (string subject, string body)
        {
            var bodyHtml = File.ReadAllText(Environment.CurrentDirectory+@"\Temaplate\index.html");
            bodyHtml = bodyHtml.Replace("<#DatosTiempo#>", body);
            var addressFrom = new MailAddress("neokeitdev@gmail.com", "neokeitdev");
            var addressTo = new MailAddress("neokeit@gmail.com");
            var message = new MailMessage(addressFrom, addressTo);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = bodyHtml;

            var client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("neokeitdev@gmail.com", "jowz tmkf mjsd gtlc");
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceInformation("Exception caught in CreateTestMessage2(): {0}", ex);
            }
        }
    }
}
