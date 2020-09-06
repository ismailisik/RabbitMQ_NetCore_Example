using Microsoft.AspNetCore.Http;
using RabbitMQ_Exaple_WortToPdf.Producer.Helpers.MemoryStreamHelpers.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ_Exaple_WortToPdf.Producer.Helpers.MemoryStreamHelpers.Concrete
{
    public class MemoryStreamHelpers : IMemoryStreamHelpers
    {
        public MemoryStream FileCopyToMemory(IFormFile formFile)
        {
            MemoryStream ms = new MemoryStream();
            formFile.CopyTo(ms);
            return ms;
        }
    }
}
