using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ_Exaple_WortToPdf.Producer.Models
{
    public class MessageWordToPdf
    {
        public string Email { get; set; }
        public byte[] WordByte { get; set; }
        public string WordName { get; set; }
    }
}
