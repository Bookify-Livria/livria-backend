using LivriaBackend.commerce.Application.Internal.CommandServices;
using LivriaBackend.commerce.Application.Internal.QueryServices;
using LivriaBackend.commerce.Domain.Repositories;
using LivriaBackend.commerce.Domain.Services;
using LivriaBackend.commerce.Infrastructure.Repositories;
using LivriaBackend.Shared.Domain.Repositories;
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using LivriaBackend.users.Interfaces.REST.Transform;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using AutoMapper;

// Usings for the users module
using LivriaBackend.users.Application.Internal.CommandServices;
using LivriaBackend.users.Application.Internal.QueryServices;
using LivriaBackend.users.Domain.Model.Repositories;
using LivriaBackend.users.Domain.Model.Services;
using LivriaBackend.users.Infrastructure.Repositories;
using LivriaBackend.users.Domain.Model.Aggregates; // Nuevo using para UserAdmin

// Usings for ACL
using LivriaBackend.users.Interfaces.ACL;
using LivriaBackend.users.Application.ACL;

// USINGS FOR COMMUNITIES MODULE
using LivriaBackend.communities.Domain.Repositories;
using LivriaBackend.communities.Domain.Model.Services;
using LivriaBackend.communities.Infrastructure.Repositories;
using LivriaBackend.communities.Application.Internal.CommandServices;
using LivriaBackend.communities.Application.Internal.QueryServices;
using LivriaBackend.communities.Interfaces.REST.Transform;

// NUEVOS USINGS para la lógica de inicialización
using System.Reflection; // Para GetProperty, SetValue (reflexión)
using System.Linq; // Para FirstOrDefaultAsync
using System.Threading.Tasks; // Para await y Task
using Microsoft.Extensions.Logging; // Para ILogger


var builder = WebApplication.CreateBuilder(args);

// Get connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
    throw new InvalidOperationException("Database connection string 'DefaultConnection' not found.");

// Configure DbContext for MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(connectionString)); // <-- CAMBIO AQUÍ: Usando UseMySQL(connectionString) directamente

// Dependency Injection (Unit of Work and Repository)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

// =======================================================================
//  Register Repositories for User, UserClient, UserAdmin
// =======================================================================
builder.Services.AddScoped<IUserClientRepository, UserClientRepository>();
builder.Services.AddScoped<IUserAdminRepository, UserAdminRepository>();

// =======================================================================
// Register Repositories for Communities, Posts, and NEW UserCommunities
// =======================================================================
builder.Services.AddScoped<ICommunityRepository, CommunityRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IUserCommunityRepository, UserCommunityRepository>();

// =======================================================================
//  Register Application Services (Command Services and Query Services)
//  for Book, UserClient, UserAdmin.
// =======================================================================
// Application Services for Book
builder.Services.AddScoped<BookCommandService>();
builder.Services.AddScoped<BookQueryService>();

// Application Services for UserClient
builder.Services.AddScoped<IUserClientCommandService, UserClientCommandService>();
builder.Services.AddScoped<IUserClientQueryService, UserClientQueryService>();

// Application Services for UserAdmin
builder.Services.AddScoped<IUserAdminCommandService, UserAdminCommandService>();
builder.Services.AddScoped<IUserAdminQueryService, UserAdminQueryService>();

// =======================================================================
// Register Application Services for Community, Post, and NEW UserCommunity
// =======================================================================
builder.Services.AddScoped<ICommunityCommandService, CommunityCommandService>();
builder.Services.AddScoped<ICommunityQueryService, CommunityQueryService>();
builder.Services.AddScoped<IPostCommandService, PostCommandService>();
builder.Services.AddScoped<IPostQueryService, PostQueryService>();
builder.Services.AddScoped<IUserCommunityCommandService, UserCommunityCommandService>();


// =======================================================================
//  Configure AutoMapper
// =======================================================================
builder.Services.AddAutoMapper(typeof(UsersMappingProfile).Assembly);
builder.Services.AddAutoMapper(typeof(CommunitiesMappingProfile).Assembly);

// =======================================================================
//  Register Domain Services (Book is already there).
// =======================================================================
builder.Services.AddScoped<BookService>();

// =======================================================================
//  Register ACL Facades
// =======================================================================
builder.Services.AddScoped<IUserClientContextFacade, UserClientContextFacade>();


// Enable Controllers
builder.Services.AddControllers();

// =======================================================================
//  Configure Swagger for API documentation
// =======================================================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Livria API",
        Version = "v1",
        Description = "API for book management in Livria"
    });
});

var app = builder.Build();

// =======================================================================
//  LÓGICA PARA INICIALIZAR LA BASE DE DATOS Y EL USERADMIN POR DEFECTO
// =======================================================================
// Esto se ejecuta una vez al inicio de la aplicación
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        
        // ADVERTENCIA: context.Database.EnsureCreated() no maneja migraciones.
        // Si tu esquema de base de datos cambia, necesitarás borrar la base de datos manualmente
        // (por ejemplo, desde MySQL Workbench) para que se recree con el nuevo esquema,
        // o usar migraciones de EF Core (dotnet ef migrations add/update) en su lugar.
        // Puedes descomentar la siguiente línea durante el DESARROLLO para recrear la DB en cada inicio:
        // context.Database.EnsureDeleted(); // ¡MUCHO CUIDADO! Borra la DB cada vez. Usar solo en desarrollo.
        
        context.Database.EnsureCreated(); // Crea la DB si no existe, no aplica migraciones.

        // Verificar si el UserAdmin por defecto (ID 0) ya existe en la base de datos
        var defaultUserAdmin = await context.UserAdmins.FirstOrDefaultAsync(ua => ua.Id == 0);

        if (defaultUserAdmin == null)
        {
            // Si no existe, crear y añadir el UserAdmin por defecto
            var userAdmin = new UserAdmin(
                "Super Administrador",
                "admin_default",
                "admin@livria.com",
                "hashed_password_admin_123",
                true,
                "0000"
            );
            
            // Asigna el ID explícitamente a 0 usando reflexión.
            // Esto es necesario porque el setter de 'Id' es 'protected' en la clase base 'User',
            // y no podemos asignarlo directamente desde fuera de la clase 'User' o 'UserAdmin' sin un constructor específico.
            // La reflexión permite saltarse el modificador de acceso para este propósito de inicialización.
            userAdmin.GetType().GetProperty("Id")?.SetValue(userAdmin, 0);

            context.UserAdmins.Add(userAdmin);
            await context.SaveChangesAsync(); // Guarda los cambios en la base de datos
            Console.WriteLine("UserAdmin por defecto creado con éxito (ID 0).");
        }
        else
        {
            Console.WriteLine("UserAdmin por defecto (ID 0) ya existe en la base de datos.");
        }
    }
    catch (Exception ex)
    {
        // Si hay un error, lo registra. Asegúrate de tener ILogger configurado o usa Console.WriteLine
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al inicializar el UserAdmin por defecto o la base de datos.");
        // También puedes usar Console.WriteLine para depuración simple:
        // Console.WriteLine($"Error al inicializar: {ex.Message}");
    }
}
// =======================================================================
// FIN DE LÓGICA DE INICIALIZACIÓN
// =======================================================================


// =======================================================================
//  Configure the HTTP middleware pipeline
// =======================================================================
// Enable Swagger UI in development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();