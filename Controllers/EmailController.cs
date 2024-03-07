using System.Net.Mail;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using SignalIR.Models;
using Microsoft.Extensions.Configuration;
namespace SignalIR.Controllers
{
    public class EmailController : Controller
    {
        private IConfiguration _Configuration;
        public EmailController(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendEmail(EmailModel emailModel)
        {
            //Read SMTP settings from AppSettings.json.
            string host = this._Configuration.GetValue<string>("Smtp:Server");
            int port = this._Configuration.GetValue<int>("Smtp:Port");
            string fromAddress = this._Configuration.GetValue<string>("Smtp:FromAddress");
            string userName = this._Configuration.GetValue<string>("Smtp:UserName");
            string password = this._Configuration.GetValue<string>("Smtp:Password");
            using (MailMessage mm = new MailMessage(fromAddress, emailModel.To))
            {
                mm.Subject = emailModel.Subject;
                mm.Body = emailModel.Body;

                mm.IsBodyHtml = false;
                 

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = host;
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential(userName, password);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.EnableSsl = true;  
                    smtp.Port = port;
                    smtp.Send(mm);
                }
            }
            return View();
        }
    }
}
