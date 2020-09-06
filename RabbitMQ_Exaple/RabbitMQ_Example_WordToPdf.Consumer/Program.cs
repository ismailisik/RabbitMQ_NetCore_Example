using System;
using System.IO;
using System.Net.Mail;

namespace RabbitMQ_Example_WordToPdf.Consumer
{
   
 
    class Program
    {
        //İlk olarak email göndericek method'u yazıyorum.
        public static bool EmailSend(string email, MemoryStream ms, string fileName)
        {
            try
            {
                ms.Position = 0; //En baştan itibaren oku...

                System.Net.Mime.ContentType contentType = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf); //Mailime ekleyeceğim verinin tipini belirliyorum.

                Attachment attachment = new Attachment(ms, contentType);
                attachment.ContentDisposition.FileName = $"{fileName}.pdf";

                //Mail Gönderme İşlemleri

                MailMessage mail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();

                mail.From = new MailAddress("Email Adresiniz.");
                mail.To.Add(email);

                mail.Subject = "Word To Pdf | Example Work"; //Başlık Belirledim..
                mail.Body = "Word Dosyanız Başır ile Pdf Dosyasına Dönüştürülmüştür Ekten Ulaşabilirsiniz."; //Mailimin body'sini belirledim.
                mail.IsBodyHtml = true; //Html içericek.
                mail.Attachments.Add(attachment); //Pdf attach ettim...

                //SmtpClient Ayarları

                smtpClient.Host = "Some Host";
                smtpClient.Port = 587;
                smtpClient.Credentials = new System.Net.NetworkCredential("Some User Name", "Some Password");

                ms.Close();
                ms.Dispose();

                Console.WriteLine("Mail Gönderilmiştir...");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Masajınız Gönderilemedi...Mesaj: {ex}");
                return false;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
