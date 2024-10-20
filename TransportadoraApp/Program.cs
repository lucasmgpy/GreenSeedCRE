using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TransportadoraApp.Data;
using TransportadoraApp.Models;

namespace TransportadoraApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Executa o serviço de processamento de fila
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    // Adiciona o arquivo appsettings.json
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    // Configuração do DbContext
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection")));

                    // Configuração do QueueClient
                    string connectionString = hostContext.Configuration.GetConnectionString("AzureStorage");
                    string queueName = hostContext.Configuration["QueueName"];

                    if (string.IsNullOrEmpty(connectionString))
                    {
                        throw new ArgumentNullException("AzureStorage", "A string de conexão para AzureStorage está vazia ou nula.");
                    }

                    if (string.IsNullOrEmpty(queueName))
                    {
                        throw new ArgumentNullException("QueueName", "O nome da fila está vazio ou nulo.");
                    }

                    services.AddSingleton(new QueueClient(connectionString, queueName));

                    // Adiciona o serviço de processamento
                    services.AddHostedService<QueueProcessor>();
                });
    }

    public class QueueProcessor : IHostedService, IDisposable
    {
        private readonly QueueClient _queueClient;
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;

        public QueueProcessor(QueueClient queueClient, IServiceScopeFactory scopeFactory)
        {
            _queueClient = queueClient;
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Configura o Timer para verificar a fila a cada 10 segundos
            _timer = new Timer(ProcessQueue, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            return Task.CompletedTask;
        }

        private async void ProcessQueue(object state)
        {
            try
            {
                // Recebe a próxima mensagem da fila
                QueueMessage[] messages = await _queueClient.ReceiveMessagesAsync(maxMessages: 1, visibilityTimeout: TimeSpan.FromSeconds(30));

                if (messages.Length > 0)
                {
                    QueueMessage message = messages[0];
                    string messageText = message.MessageText;

                    // Desserializa a mensagem
                    OrderStatusMessage orderStatus = JsonSerializer.Deserialize<OrderStatusMessage>(messageText);

                    if (orderStatus != null)
                    {
                        Console.WriteLine($"Processando OrderId: {orderStatus.OrderId}");

                        // Simula o processamento (por exemplo, esperar alguns segundos)
                        await Task.Delay(TimeSpan.FromSeconds(5));

                        // Atualiza o status da encomenda no banco de dados
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                            var order = await dbContext.Orders.FindAsync(orderStatus.OrderId);
                            if (order != null)
                            {
                                order.Status = "Entregue"; // Atualiza para "Entregue"
                                await dbContext.SaveChangesAsync();
                                Console.WriteLine($"OrderId {order.OrderId} atualizado para 'Entregue'.");
                            }
                            else
                            {
                                Console.WriteLine($"OrderId {orderStatus.OrderId} não encontrado no banco de dados.");
                            }
                        }

                        // Remove a mensagem da fila após o processamento
                        await _queueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar a fila: {ex.Message}");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
