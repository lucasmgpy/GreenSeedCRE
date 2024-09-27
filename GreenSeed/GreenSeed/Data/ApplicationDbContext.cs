using GreenSeed.Models;
using GreenSeedCRE.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GreenSeed.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets para as entidades
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PhotoChallenge> PhotoChallenges { get; set; }
        public DbSet<ChallengeOption> ChallengeOptions { get; set; }
        public DbSet<PhotoChallengeParticipation> PhotoChallengeParticipations { get; set; }
        public DbSet<CommunityPhotoUpload> CommunityPhotoUploads { get; set; }
        public DbSet<DeliveryCompany> DeliveryCompanies { get; set; }
        public DbSet<Discount> Discounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração de herança: TPH (Table Per Hierarchy)
            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("Role")
                .HasValue<User>("User")
                .HasValue<Admin>("Admin");

            // Relação entre Admin e PhotoChallenge
            modelBuilder.Entity<PhotoChallenge>()
                .HasOne(pc => pc.Admin)
                .WithMany(a => a.PhotoChallenges)
                .HasForeignKey(pc => pc.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relação entre PhotoChallenge e ChallengeOption
            modelBuilder.Entity<ChallengeOption>()
                .HasOne(co => co.PhotoChallenge)
                .WithMany(pc => pc.ChallengeOptions)
                .HasForeignKey(co => co.ChallengeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Garantir que cada PhotoChallenge tenha exatamente 4 ChallengeOptions
            // Nota: Isso não pode ser enforceado diretamente via EF Core, mas pode ser validado na lógica de negócios.

            // Relação entre User e Cart (um para um)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relação entre User e Discount (um para um)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Discount)
                .WithOne(d => d.User)
                .HasForeignKey<Discount>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuração das tabelas para herança
            modelBuilder.Entity<Product>()
                .HasDiscriminator<string>("ProductType");

            // Configurar as chaves primárias compostas ou outras restrições se necessário

            // Configuração das relações Many-to-Many (se aplicável)
            // Exemplo: Participação dos usuários nos desafios
            modelBuilder.Entity<PhotoChallengeParticipation>()
                .HasOne(pcp => pcp.PhotoChallenge)
                .WithMany(pc => pc.Participations)
                .HasForeignKey(pcp => pcp.ChallengeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PhotoChallengeParticipation>()
                .HasOne(pcp => pcp.User)
                .WithMany(u => u.PhotoChallengeParticipations)
                .HasForeignKey(pcp => pcp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PhotoChallengeParticipation>()
                .HasOne(pcp => pcp.SelectedOption)
                .WithMany()
                .HasForeignKey(pcp => pcp.SelectedOptionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurações adicionais, como índices e restrições exclusivas
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .IsUnique();

            // Configuração para DeliveryCompany
            modelBuilder.Entity<DeliveryCompany>()
                .HasMany(dc => dc.Orders)
                .WithOne(o => o.DeliveryCompany)
                .HasForeignKey(o => o.DeliveryCompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuração para PhotoChallenge - Participações
            modelBuilder.Entity<PhotoChallengeParticipation>()
                .HasOne(pcp => pcp.SelectedOption)
                .WithMany()
                .HasForeignKey(pcp => pcp.SelectedOptionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
