using GreenSeed.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GreenSeed.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> //alterado para buscar o "ApplicationUser" do Models
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CommunityPhotoUpload> CommunityPhotoUploads { get; set; }
        public DbSet<CommunityPhotoComment> CommunityPhotoComments { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<ChallengeResponse> ChallengeResponses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Challenge>()
                .HasMany(c => c.ChallengeResponses)
                .WithOne(cr => cr.Challenge)
                .HasForeignKey(cr => cr.ChallengeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ChallengeResponse>()
                .HasOne(cr => cr.User)
                .WithMany()
                .HasForeignKey(cr => cr.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Evita a exclusão em cascata de respostas ao excluir um usuário




            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            // Seed para categorias
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Flores", Description = "Todas as variedades de flores" },
                new Category { CategoryId = 2, Name = "Plantas", Description = "Plantas para decoração de ambientes" },
                new Category { CategoryId = 3, Name = "Fertilizantes", Description = "Fertilizantes para as plantas e flores" },
                new Category { CategoryId = 4, Name = "Vasos", Description = "Vasos para plantas e flores" },
                new Category { CategoryId = 5, Name = "Acessórios", Description = "Acessórios para jardinagem" }
            );

            // Seed para produtos
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Rosa", Description = "Rosa vermelha clássica", ImageUrl = "https://via.placeholder.com/150", Price = 5.99m, Stock = 100, CategoryId = 1 },
                new Product { ProductId = 2, Name = "Orquídea", Description = "Orquídea branca", ImageUrl = "https://via.placeholder.com/150", Price = 15.99m, Stock = 50, CategoryId = 2 },
                new Product { ProductId = 3, Name = "Samambaia", Description = "Planta ornamental para ambientes internos", ImageUrl = "https://via.placeholder.com/150", Price = 25.99m, Stock = 30, CategoryId = 3 },
                new Product { ProductId = 4, Name = "Cacto", Description = "Planta do deserto", ImageUrl = "https://via.placeholder.com/150", Price = 4.99m, Stock = 30, CategoryId = 2 },
                new Product { ProductId = 5, Name = "Fertilizante NPK", Description = "Fertilizante para plantas", ImageUrl = "https://via.placeholder.com/150", Price = 10.99m, Stock = 200, CategoryId = 3 },
                new Product { ProductId = 6, Name = "Fertilizante JBL", Description = "Fertilizante para flores", ImageUrl = "https://via.placeholder.com/150", Price = 10.99m, Stock = 200, CategoryId = 3 },
                new Product { ProductId = 7, Name = "Vaso de cerâmica", Description = "Vaso de cerâmica para plantas", ImageUrl = "https://via.placeholder.com/150", Price = 7.99m, Stock = 150, CategoryId = 4 },
                new Product { ProductId = 8, Name = "Vaso de PLASTICO", Description = "Vaso de plastico para plantas", ImageUrl = "https://via.placeholder.com/150", Price = 3.99m, Stock = 150, CategoryId = 4 },
                new Product { ProductId = 9, Name = "Luvas de jardinagem", Description = "Luvas para jardinagem", ImageUrl = "https://via.placeholder.com/150", Price = 3.99m, Stock = 100, CategoryId = 5 },
                new Product { ProductId = 10, Name = "Pinça", Description = "Luvas para jardinagem", ImageUrl = "https://via.placeholder.com/150", Price = 8.99m, Stock = 100, CategoryId = 5 }
            );
        }
    }
}
