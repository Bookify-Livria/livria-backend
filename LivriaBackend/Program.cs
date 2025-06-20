using LivriaBackend.commerce.Application.Internal.CommandServices;
using LivriaBackend.commerce.Application.Internal.QueryServices;
using LivriaBackend.commerce.Domain.Repositories;
using LivriaBackend.commerce.Domain.Services;
using LivriaBackend.commerce.Infrastructure.Repositories;
using LivriaBackend.Shared.Domain.Repositories;
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using LivriaBackend.users.Interfaces.REST.Transform; // Importa el namespace de UsersMappingProfile
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using AutoMapper; // Importa AutoMapper
using LivriaBackend.users.Application.Internal.CommandServices;
using LivriaBackend.users.Application.Internal.QueryServices;
using LivriaBackend.users.Domain.Model.Repositories;
using LivriaBackend.users.Domain.Model.Services;
using LivriaBackend.users.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Obtener cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
    throw new InvalidOperationException("Database connection string 'DefaultConnection' not found.");

// Configurar DbContext para MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(connectionString));

// Inyección de dependencias (Unit of Work y Repository)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

// =======================================================================
//  Registro de Repositorios para User, UserClient, UserAdmin
//  Asegúrate de que estas interfaces y sus implementaciones existan
//  en los namespaces importados.
// =======================================================================
builder.Services.AddScoped<IUserClientRepository, UserClientRepository>();
builder.Services.AddScoped<IUserAdminRepository, UserAdminRepository>();
// Si tuvieras un IUserRepository genérico para operaciones sobre la base User, lo añadirías aquí también.

// =======================================================================
//  Registro de Servicios de Aplicación (Command Services y Query Services)
//  para Book, UserClient y UserAdmin.
// =======================================================================
// Servicios de Aplicación para Book
builder.Services.AddScoped<BookCommandService>();
builder.Services.AddScoped<BookQueryService>();

// Servicios de Aplicación para UserClient
builder.Services.AddScoped<IUserClientCommandService, UserClientCommandService>();
builder.Services.AddScoped<IUserClientQueryService, UserClientQueryService>();

// Servicios de Aplicación para UserAdmin
builder.Services.AddScoped<IUserAdminCommandService, UserAdminCommandService>();
builder.Services.AddScoped<IUserAdminQueryService, UserAdminQueryService>();


// =======================================================================
//  Configuración de AutoMapper
//  Asegúrate de que 'UsersMappingProfile' exista en el namespace
//  'LivriaBackend.users.Interfaces.REST.Transform' y herede de AutoMapper.Profile.
// =======================================================================
builder.Services.AddAutoMapper(typeof(UsersMappingProfile).Assembly);


// =======================================================================
//  Registro de Servicios de Dominio (Book ya está).
//  Añade otros servicios de dominio si los tienes (ej. para User).
// =======================================================================
builder.Services.AddScoped<BookService>();
// builder.Services.AddScoped<IUserDomainService, UserDomainService>(); // Ejemplo si tuvieras un servicio de dominio para User


// Habilitar Controllers
builder.Services.AddControllers();

// =======================================================================
//  Configuración de Swagger para documentación de API
// =======================================================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Livria API",
        Version = "v1",
        Description = "API para la gestión de libros en Livria"
    });
});

var app = builder.Build();

// =======================================================================
//  Asegurar que la base de datos está creada y aplicar migraciones
// =======================================================================
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // dbContext.Database.Migrate(); // Usar si tienes migraciones
    dbContext.Database.EnsureCreated(); // Usar si quieres que la DB se cree automáticamente
}

// =======================================================================
//  Configuración del pipeline de middleware HTTP
// =======================================================================
// Habilitar Swagger UI en modo desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Redirige peticiones HTTP a HTTPS

app.UseAuthorization(); // Middleware de autorización (antes de MapControllers)
app.MapControllers(); // Mapea los controladores de la API

app.Run(); // Ejecuta la aplicación