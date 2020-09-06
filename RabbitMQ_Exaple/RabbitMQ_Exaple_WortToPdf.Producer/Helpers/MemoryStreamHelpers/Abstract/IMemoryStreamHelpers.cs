using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ_Exaple_WortToPdf.Producer.Helpers.MemoryStreamHelpers.Abstract
{
    public interface IMemoryStreamHelpers
    {
        MemoryStream FileCopyToMemory(IFormFile formFile);
    }
}
