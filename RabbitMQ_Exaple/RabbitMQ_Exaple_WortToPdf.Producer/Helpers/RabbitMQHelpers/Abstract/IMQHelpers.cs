using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ_Exaple_WortToPdf.Producer.Heplers.RabbitMQHelpers.Abstract
{
    public interface IMQHelpers
    {
        IModel GetModel();
    }
}
