using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Navy.Services
{
    internal class MailService
    {
        public static void Send (string subject, string body, string serieBody)
        {
            var bodyHtml = GenterateBodyHtml(body, serieBody);
            var addressFrom = new MailAddress("neokeitdev@gmail.com", "neokeitdev");
            var addressTo = new MailAddress("neokeit@gmail.com");
            var message = new MailMessage(addressFrom, addressTo)
            {
                Subject = subject,
                IsBodyHtml = true,
                Body = bodyHtml
            };

            var client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("neokeitdev@gmail.com", "jowz tmkf mjsd gtlc")
            };
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceInformation("Exception caught in CreateTestMessage2(): {0}", ex);
            }
        }

        private static string GenterateBodyHtml(string body, string serieBody)
        {
            var bodyHtml = File.ReadAllText(Environment.CurrentDirectory + @"\Temaplate\index.html");
            bodyHtml = bodyHtml.Replace("<#DatosTiempo#>", body);
            bodyHtml = bodyHtml.Replace("<#DatosSeries#>", serieBody);
            bodyHtml = bodyHtml.Replace("<#Fecha#>", DateTime.Now.ToString("f"));
            return bodyHtml;
        }
    }
}
