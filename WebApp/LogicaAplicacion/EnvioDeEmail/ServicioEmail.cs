using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.EnvioDeEmail
{
    public class ServicioEmail : IServicioEmail
    {
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _emailFrom = "siurxps@gmail.com";
        private readonly string _emailPassword = "gyblsysgtfspsxlg";
           

        public void EnviarEmail(string destinatario, string asunto, string mensaje)
        {
            using SmtpClient client = new SmtpClient(_smtpServer, _smtpPort);
            client.Credentials = new NetworkCredential(_emailFrom, _emailPassword);
            client.EnableSsl = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailFrom),
                Subject = asunto,
                Body = mensaje,
                IsBodyHtml = true
            };

            mailMessage.To.Add(destinatario);

            client.Send(mailMessage);
        }
    }
}