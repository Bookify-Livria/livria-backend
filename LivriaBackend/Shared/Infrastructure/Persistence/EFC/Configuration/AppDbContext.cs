using LivriaBackend.commerce.Domain.Model.Aggregates;
using LivriaBackend.users.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic; // Asegúrate de tener este using para List<int> si lo usas en UserClient

namespace LivriaBackend.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserClient> UserClients { get; set; }
    public DbSet<UserAdmin> UserAdmins { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // === Configuración de la tabla Book ===
        modelBuilder.Entity<Book>().ToTable("Books");
        modelBuilder.Entity<Book>().HasKey(b => b.Id);
        // (otras configuraciones de Book si las tienes)


        // === Configuración de Herencia TPT (Table Per Type) ===

        // 1. Mapear la clase base User a su propia tabla 'Users'
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<User>().Property(u => u.Display).IsRequired().HasMaxLength(255);
        modelBuilder.Entity<User>().Property(u => u.Username).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(255);
        modelBuilder.Entity<User>().Property(u => u.Password).IsRequired().HasMaxLength(255);

        // 2. Mapear UserClient a su propia tabla 'UserClients'
        modelBuilder.Entity<UserClient>().ToTable("UserClients");
        modelBuilder.Entity<UserClient>().Property(uc => uc.Subscription).HasMaxLength(50);
        modelBuilder.Entity<UserClient>().Property(uc => uc.Icon).HasMaxLength(255);
        modelBuilder.Entity<UserClient>().Property(uc => uc.Phrase).HasMaxLength(255);
        // Si mapeas Order, configúralo aquí

        // 3. Mapear UserAdmin a su propia tabla 'UserAdmins'
        modelBuilder.Entity<UserAdmin>().ToTable("UserAdmins");
        modelBuilder.Entity<UserAdmin>().Property(ua => ua.AdminAccess).IsRequired();
        modelBuilder.Entity<UserAdmin>().Property(ua => ua.SecurityPin).HasMaxLength(50);

        // === ¡NUEVO! Configuración de Seed Data para UserAdmin ===
        // Es crucial que el ID sea fijo y negativo o un valor que sepas que no colisionará
        // con IDs autoincrementales reales, especialmente si tu DB empieza IDs desde 1.
        // EF Core ajustará esto en la DB para IDs positivos si es autoincremental.
        modelBuilder.Entity<UserAdmin>().HasData(
            new UserAdmin(
                id: 1, // ID fijo para el admin por defecto
                display: "Super Administrador",
                username: "admin_default",
                email: "admin@livria.com",
                password: "hashed_password_admin_123", // ¡Siempre usa un hash de contraseña real en producción!
                adminAccess: true,
                securityPin: "0000" // O un PIN seguro
            )
        );

        // Puedes añadir más datos de sembrado aquí si lo deseas
        // modelBuilder.Entity<UserClient>().HasData(
        //     new UserClient(
        //         id: 2, // Otro ID para UserClient si lo deseas
        //         display: "Cliente de Prueba",
        //         username: "client_test",
        //         email: "client@livria.com",
        //         password: "hashed_password_client_abc",
        //         icon: "default_icon.png",
        //         phrase: "Mi frase",
        //         order: new List<int>(), // Vacío o con datos si Order está mapeado
        //         subscription: "Free"
        //     )
        // );
    }
}