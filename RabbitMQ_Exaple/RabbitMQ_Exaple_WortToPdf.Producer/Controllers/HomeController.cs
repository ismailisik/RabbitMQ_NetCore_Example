using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ_Exaple_WortToPdf.Producer.Helpers.MemoryStreamHelpers.Abstract;
using RabbitMQ_Exaple_WortToPdf.Producer.Heplers.RabbitMQHelpers.Abstract;
using RabbitMQ_Exaple_WortToPdf.Producer.Models;


namespace RabbitMQ_Exaple_WortToPdf.Producer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IMQHelpers _mqHelpers;
        private IMemoryStreamHelpers _streamHelpers;
        
        public HomeController(ILogger<HomeController> logger, IMQHelpers mqHelpers, IMemoryStreamHelpers streamHelpers)
        {
            _logger = logger;
            _mqHelpers = mqHelpers;
            _streamHelpers = streamHelpers;
        }

        public IActionResult WordToPdf()
        {
            return View();
        }

        [HttpPost]
        public IActionResult WordToPdf(WordToPdfModel wordToPdf)
        {
            using (var channel= _mqHelpers.GetModel())
            {
                channel.ExchangeDeclare("Covert_Exchange", type: ExchangeType.Direct, durable: true, false, null);
               
                channel.QueueDeclare("File", durable: true, exclusive: false, autoDelete: false, null);
                //Not: Oluşturduğumuz Queue exclusive özelliği false çünkü birden çok consumer tarafından dinlenmesini istiyoruz.

                channel.QueueBind("File", "Covert_Exchange", "WordToPdf");

                var MessageWordToPdf = new MessageWordToPdf()
                {
                    Email = wordToPdf.Email,
                    WordName = wordToPdf.File.FileName,
                    WordByte = _streamHelpers.FileCopyToMemory(wordToPdf.File).ToArray()
                };

                var SerilizeMessage = JsonConvert.SerializeObject(MessageWordToPdf);
                var ByteMessage = Encoding.UTF8.GetBytes(SerilizeMessage);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish("Covert_Exchange", "WordToPdf", basicProperties: properties, body: ByteMessage);
            }
            ViewBag.Message = "Word Dosyanız Pdf Dosyasına Dönüştürülüp Email Adresinize Gönderilecektir.";
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
