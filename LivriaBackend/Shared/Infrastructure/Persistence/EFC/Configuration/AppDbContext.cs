using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.commerce.Domain.Model.Aggregates; 
using LivriaBackend.commerce.Domain.Model.Entities;    
using LivriaBackend.commerce.Domain.Model.ValueObjects; 
using LivriaBackend.users.Domain.Model.Aggregates;
using LivriaBackend.notifications.Domain.Model.Aggregates;
using LivriaBackend.notifications.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;

namespace LivriaBackend.Shared.Infrastructure.Persistence.EFC.Configuration
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; } 
        public DbSet<OrderItem> OrderItems { get; set; } 

        public DbSet<User> Users { get; set; }
        public DbSet<UserClient> UserClients { get; set; }
        public DbSet<UserAdmin> UserAdmins { get; set; }

        public DbSet<Community> Communities { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserCommunity> UserCommunities { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(u => u.Display).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Password).IsRequired().HasMaxLength(255);
            });

            modelBuilder.Entity<UserClient>(entity =>
            {
                entity.ToTable("userclients");
                entity.Property(uc => uc.Icon).HasMaxLength(255);
                entity.Property(uc => uc.Phrase).HasMaxLength(255);
                entity.Property(uc => uc.Subscription).HasMaxLength(50);
                
                
                
                entity.HasBaseType<User>();
                
                entity.HasMany(uc => uc.FavoriteBooks)
                    .WithMany() 
                    .UsingEntity(j => j.ToTable("user_favorite_books"));

                
                entity.HasMany(uc => uc.Orders) 
                    .WithOne(o => o.UserClient) 
                    .HasForeignKey(o => o.UserClientId) 
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict); 
            });

            modelBuilder.Entity<UserAdmin>(entity =>
            {
                entity.ToTable("useradmins");
                entity.Property(ua => ua.AdminAccess).IsRequired();
                entity.Property(ua => ua.SecurityPin).HasMaxLength(255);
                entity.HasBaseType<User>();
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("books");
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(b => b.Title).IsRequired().HasMaxLength(255);
                entity.Property(b => b.Description).HasMaxLength(1000);
                entity.Property(b => b.Author).IsRequired().HasMaxLength(100);
                entity.Property(b => b.Price).IsRequired().HasColumnType("decimal(10, 2)");
                entity.Property(b => b.Stock).IsRequired();
                entity.Property(b => b.Cover).HasMaxLength(255);
                entity.Property(b => b.Genre).HasMaxLength(50);
                entity.Property(b => b.Language).HasMaxLength(50);

                entity.HasMany(b => b.Reviews)
                      .WithOne(r => r.Book)
                      .HasForeignKey(r => r.BookId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("reviews");
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(r => r.Content).IsRequired().HasMaxLength(1000);
                entity.Property(r => r.Username).IsRequired().HasMaxLength(100);

                entity.Property(r => r.BookId).IsRequired();
                entity.HasOne(r => r.Book)
                      .WithMany(b => b.Reviews)
                      .HasForeignKey(r => r.BookId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(r => r.UserClientId).IsRequired();
                entity.HasOne(r => r.UserClient)
                      .WithMany()
                      .HasForeignKey(r => r.UserClientId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.ToTable("cart_items");
                entity.HasKey(ci => ci.Id);
                entity.Property(ci => ci.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(ci => ci.Quantity).IsRequired();

                entity.Property(ci => ci.BookId).IsRequired();
                entity.HasOne(ci => ci.Book)
                      .WithMany()
                      .HasForeignKey(ci => ci.BookId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(ci => ci.UserClientId).IsRequired();
                entity.HasOne(ci => ci.UserClient)
                      .WithMany()
                      .HasForeignKey(ci => ci.UserClientId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(ci => new { ci.BookId, ci.UserClientId }).IsUnique();
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");
                entity.HasKey(o => o.Id);
                entity.Property(o => o.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(o => o.Code).IsRequired().HasMaxLength(6);
                entity.HasIndex(o => o.Code).IsUnique(); 

                entity.Property(o => o.UserClientId).IsRequired();
                entity.HasOne(o => o.UserClient) 
                    .WithMany(uc => uc.Orders) 
                    .HasForeignKey(o => o.UserClientId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict); 

                entity.Property(o => o.UserEmail).IsRequired().HasMaxLength(100);
                entity.Property(o => o.UserPhone).IsRequired().HasMaxLength(20);
                entity.Property(o => o.UserFullName).IsRequired().HasMaxLength(255);
                entity.Property(o => o.IsDelivery).IsRequired();
                entity.Property(o => o.Total).IsRequired().HasColumnType("decimal(10, 2)");
                entity.Property(o => o.Date).IsRequired();

                
                entity.OwnsOne(o => o.Shipping, shipping =>
                {
                    shipping.Property(s => s.Address).IsRequired().HasMaxLength(255).HasColumnName("ShippingAddress");
                    shipping.Property(s => s.City).IsRequired().HasMaxLength(100).HasColumnName("ShippingCity");
                    shipping.Property(s => s.District).IsRequired().HasMaxLength(100).HasColumnName("ShippingDistrict");
                    shipping.Property(s => s.Reference).HasMaxLength(500).HasColumnName("ShippingReference");
                    
                });

                
                entity.HasMany(o => o.Items)
                      .WithOne(oi => oi.Order)
                      .HasForeignKey(oi => oi.OrderId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade); 
            });

            
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("order_items");
                entity.HasKey(oi => oi.Id);
                entity.Property(oi => oi.Id).IsRequired().ValueGeneratedOnAdd();

                entity.Property(oi => oi.BookId).IsRequired();
                entity.Property(oi => oi.BookTitle).IsRequired().HasMaxLength(255);
                entity.Property(oi => oi.BookAuthor).IsRequired().HasMaxLength(100);
                entity.Property(oi => oi.BookPrice).IsRequired().HasColumnType("decimal(10, 2)");
                entity.Property(oi => oi.BookCover).HasMaxLength(255); 

                entity.Property(oi => oi.Quantity).IsRequired();
                entity.Property(oi => oi.ItemTotal).IsRequired().HasColumnType("decimal(10, 2)");

                entity.Property(oi => oi.OrderId).IsRequired();
 
            });


            
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
                      .OnDelete(DeleteBehavior.Cascade);
            });

            
            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("posts");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(p => p.Username).IsRequired().HasMaxLength(50);
                entity.Property(p => p.Content).IsRequired().HasMaxLength(2000);
                entity.Property(p => p.Img).HasMaxLength(255);
                entity.Property(p => p.CreatedAt).IsRequired();

                entity.HasOne(p => p.Community)
                      .WithMany(c => c.Posts)
                      .HasForeignKey(p => p.CommunityId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.UserClient)
                      .WithMany()
                      .HasForeignKey(p => p.UserId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Restrict);
            });

            
            modelBuilder.Entity<UserCommunity>(entity =>
            {
                entity.ToTable("user_communities");
                entity.HasKey(uc => new { uc.UserClientId, uc.CommunityId });

                entity.Property(uc => uc.JoinedDate).IsRequired();

                entity.HasOne(uc => uc.UserClient)
                      .WithMany(u => u.UserCommunities)
                      .HasForeignKey(uc => uc.UserClientId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(uc => uc.Community)
                      .WithMany(c => c.UserCommunities)
                      .HasForeignKey(uc => uc.CommunityId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("notifications");
                entity.HasKey(n => n.Id);
                entity.Property(n => n.Id).IsRequired().ValueGeneratedOnAdd();

                entity.Property(n => n.Date).IsRequired();
                entity.Property(n => n.Title).IsRequired().HasMaxLength(100);
                entity.Property(n => n.Content).IsRequired().HasMaxLength(500);

                entity.Property(n => n.Type)
                      .IsRequired()
                      .HasConversion<string>();
            });
        }
    }
}