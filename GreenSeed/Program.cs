using GreenSeed.Data;
using GreenSeed.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using GreenSeed.Services;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs.Models;
using Microsoft.Identity.Client.Extensions.Msal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    options.SignIn.RequireConfirmedAccount = true;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // Limite de 10MB
});


// Configuração do Azure Blob Storage
builder.Services.AddSingleton(x =>
{
    string connectionString = builder.Configuration.GetConnectionString("AzureStorage");
    string containerName = builder.Configuration.GetValue<string>("BlobStorage:ContainerName");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new ArgumentNullException("AzureStorage connection string is not configured.");
    }
    if (string.IsNullOrEmpty(containerName))
    {
        throw new ArgumentNullException("BlobStorage ContainerName is not configured.");
    }
    BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);
    containerClient.CreateIfNotExists(PublicAccessType.Blob); // Definir o nível de acesso
    return containerClient;
});

// Adicionar serviços de repositório e Identity
builder.Services.AddScoped<IRepository<CommunityPhotoUpload>, Repository<CommunityPhotoUpload>>();
builder.Services.AddScoped<IRepository<CommunityPhotoComment>, Repository<CommunityPhotoComment>>();


builder.Services.AddScoped<OrderQueueService>(); //injeção do serviço para processamento das encomendas

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await SeedData.InitializeAsync(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro durante a semeadura de dados.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

public partial class Program { }
