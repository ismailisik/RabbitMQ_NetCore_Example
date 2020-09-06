using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ_Exaple_WortToPdf.Producer.Models
{
    public class WordToPdfModel
    {
        public string  Email { get; set; }
        public IFormFile File { get; set; }
    }
}
