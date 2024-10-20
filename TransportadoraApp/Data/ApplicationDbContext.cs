// TransportadoraApp/Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using TransportadoraApp.Models;

namespace TransportadoraApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        // Configurações adicionais podem ser necessárias dependendo da estrutura do banco de dados
    }

    // Definição do modelo Order (simplificada)
    public class Order
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
    }
}
