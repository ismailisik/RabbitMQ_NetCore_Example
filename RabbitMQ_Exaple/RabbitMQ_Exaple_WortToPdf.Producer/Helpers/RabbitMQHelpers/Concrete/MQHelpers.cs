using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ_Exaple_WortToPdf.Producer.Heplers.RabbitMQHelpers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQ_Exaple_WortToPdf.Producer.Helpers.RabbitMQHelpers.Concrete
{
    public class MQHelpers : IMQHelpers
    {
        private IConfiguration _configuration;

        public MQHelpers(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IModel GetModel()
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(_configuration["ConnectionStrings:RabbitMQConnectionString"]);

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            return channel;
        }
    }
}
