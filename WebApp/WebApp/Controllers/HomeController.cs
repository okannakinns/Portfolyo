using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using static System.Net.Mime.MediaTypeNames;
using WebApp.Models;
using System.IO;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMail(EmailSettings MailForm)
        {
            string fromAddress = "testeposta2023@gmail.com";
            string password = "wztdvthfwntaytrj";
            string ErrorMessage = "Gönderme İşlemi Tamamlanırken Bir Hata Oluştu.";
            string SuccessMessage = "Gönderme İşlemi Tamamlandı.";


            // E-posta alıcı bilgisi
            string toAddress = "okannakinns@gmail.com";
            // "epostalar.txt" dosyasından e-postaları alıyoruz
          

               
                   
                    MailMessage mailMessage = new MailMessage(fromAddress, toAddress);
                    mailMessage.IsBodyHtml = true;

                    // E-posta başlığı
                    string subject = MailForm.Subject;



                   

                    // E-posta gövdesine imza içeriğini ekliyoruz
                    string body = MailForm.Message+ " <br> <br> İsmim: <strong>" + MailForm.Name+ "</strong> <br> Mail Adresim: <strong>" + MailForm.Email+ "</strong>";

                    // E-posta gönderme işlemi
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential(fromAddress, password);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    smtpClient.Send(mailMessage);
                
                return RedirectToAction("Index");
            }
        [HttpPost]
        public IActionResult DownloadCv()
        {
            // İndirilecek dosyanın fiziksel yolu
            string fileName = "okan.akin.cv.pdf"; // Dosya adı

            string basicfolderPath = AppDomain.CurrentDomain.BaseDirectory;

  
            string folderPath = Path.Combine(basicfolderPath, fileName);

            // Dosya var mı kontrol edin
            if (!System.IO.File.Exists(folderPath))
            {
                return NotFound();
            }

            var dosyaBytes = System.IO.File.ReadAllBytes(folderPath);
            return File(dosyaBytes, "application/octet-stream", fileName);
        }


    }
}