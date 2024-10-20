using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using GreenSeed.Models;

namespace GreenSeed.Services
{
    public class OrderQueueService
    {
        private readonly QueueClient _queueClient;

        public OrderQueueService(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("AzureStorage");
            string queueName = "orderstatus";
            _queueClient = new QueueClient(connectionString, queueName);
            _queueClient.CreateIfNotExists();
        }

        public async Task SendOrderStatusAsync(Order order)
        {
            string message = JsonSerializer.Serialize(new OrderStatusMessage
            {
                OrderId = order.OrderId,
                Status = "Pending" // Status inicial
            });

            await _queueClient.SendMessageAsync(message);
        }
    }

    public class OrderStatusMessage
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
    }
}