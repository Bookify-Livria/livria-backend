using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.commerce.Domain.Model.Aggregates;
using LivriaBackend.users.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using System;

namespace LivriaBackend.Shared.Infrastructure.Persistence.EFC.Configuration
{
    public class AppDbContext : DbContext
    {
        // DbSet para cada agregado o entidad raíz
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; } // Entidad base para herencia
        public DbSet<UserClient> UserClients { get; set; } // Entidad derivada UserClient
        public DbSet<UserAdmin> UserAdmins { get; set; } // Entidad derivada UserAdmin

        public DbSet<Community> Communities { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserCommunity> UserCommunities { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para la entidad User (mapeo Table-Per-Type para User, UserClient, UserAdmin)
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users"); // Tabla base
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).IsRequired().ValueGeneratedOnAdd(); // Id auto-generado por la DB
                entity.Property(u => u.Display).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Password).IsRequired().HasMaxLength(255); // Espacio para contraseña hasheada
            });

            // Configuración para la entidad UserClient (hereda de User)
            modelBuilder.Entity<UserClient>(entity =>
            {
                entity.ToTable("userclients"); // Tabla separada para UserClient (TPT)
                entity.Property(uc => uc.Icon).HasMaxLength(255);
                entity.Property(uc => uc.Phrase).HasMaxLength(255);
                entity.Property(uc => uc.Subscription).HasMaxLength(50);
                entity.Ignore(uc => uc.Order); // Ignorar la propiedad Order si no es una entidad persistente
                entity.HasBaseType<User>(); // Establece la herencia
            });

            // Configuración para la entidad UserAdmin (hereda de User)
            modelBuilder.Entity<UserAdmin>(entity =>
            {
                entity.ToTable("useradmins"); // Tabla separada para UserAdmin (TPT)
                entity.Property(ua => ua.AdminAccess).IsRequired();
                entity.Property(ua => ua.SecurityPin).HasMaxLength(255); // Ajustar longitud según necesidad
                entity.HasBaseType<User>(); // Establece la herencia
            });

            // Configuración para la entidad Book
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("books");
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(b => b.Title).IsRequired().HasMaxLength(255);
                entity.Property(b => b.Author).IsRequired().HasMaxLength(100);
                entity.Property(b => b.Genre).HasMaxLength(50);
                entity.Property(b => b.Price).IsRequired().HasColumnType("decimal(10, 2)");
                entity.Property(b => b.Description).HasMaxLength(1000);
            });

            // Configuración para la entidad Community
            modelBuilder.Entity<Community>(entity =>
            {
                entity.ToTable("communities");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Description).IsRequired().HasMaxLength(500);
                entity.Property(c => c.Type).IsRequired().HasMaxLength(50);
                entity.Property(c => c.Image).HasMaxLength(255);
                entity.Property(c => c.Banner).HasMaxLength(255);

                entity.HasMany(c => c.Posts)
                      .WithOne(p => p.Community)
                      .HasForeignKey(p => p.CommunityId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade); // Cuando se borra una comunidad, se borran sus posts
            });

            // Configuración para la entidad Post (MODIFICADA)
            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("posts");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(p => p.Username).IsRequired().HasMaxLength(50);
                entity.Property(p => p.Content).IsRequired().HasMaxLength(2000);
                entity.Property(p => p.Img).HasMaxLength(255);
                entity.Property(p => p.CreatedAt).IsRequired(); // Mapear la nueva propiedad CreatedAt

                // Relación con Community
                entity.HasOne(p => p.Community)
                      .WithMany(c => c.Posts)
                      .HasForeignKey(p => p.CommunityId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);

                // Nueva: Configurar relación con UserClient
                entity.HasOne(p => p.UserClient) // Un Post es hecho por un UserClient
                      .WithMany() // Un UserClient puede hacer muchos Posts, pero no hay una colección directa de Posts en UserClient
                      .HasForeignKey(p => p.UserId) // La FK es UserId en Post
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Restrict); // Evita que se borre un UserClient si tiene posts asociados
            });

            // Configuración para la tabla de unión UserCommunity
            modelBuilder.Entity<UserCommunity>(entity =>
            {
                entity.ToTable("user_communities");
                entity.HasKey(uc => new { uc.UserClientId, uc.CommunityId }); // Clave compuesta

                entity.Property(uc => uc.JoinedDate).IsRequired();

                entity.HasOne(uc => uc.UserClient)
                      .WithMany(u => u.UserCommunities)
                      .HasForeignKey(uc => uc.UserClientId)
                      .OnDelete(DeleteBehavior.Cascade); // Si se borra un UserClient, se borran sus entradas en UserCommunity

                entity.HasOne(uc => uc.Community)
                      .WithMany(c => c.UserCommunities)
                      .HasForeignKey(uc => uc.CommunityId)
                      .OnDelete(DeleteBehavior.Cascade); // Si se borra una Community, se borran sus entradas en UserCommunity
            });
        }
    }
}