using domain.DomainModels;
using domain.Identity;
using domain.Relations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace repository
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Projection> Projections { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<ProjectionShoppingCart> ProjectionShoppingCarts { get; set; }
        public virtual DbSet<ProjectionOrder> ProjectionOrders { get; set; }
        public virtual DbSet<EmailMessage> EmailMessages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //enum
            builder.Entity<Movie>()
                .Property(u => u.Category)
                .HasConversion<string>()
                .HasMaxLength(50);

            //primary keys
            builder.Entity<Movie>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<ShoppingCart>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Projection>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<ProjectionOrder>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<ProjectionShoppingCart>()
                .HasKey(x => new { x.ProjectionId, x.ShoppingCartId });

            builder.Entity<EmailMessage>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            //Movie-Projection 1-N
            builder.Entity<Movie>()
                .HasMany(x => x.Projections)
                .WithOne(x => x.Movie)
                .HasForeignKey(x => x.Id);

            builder.Entity<Projection>()
                .HasOne(x => x.Movie)
                .WithMany(x => x.Projections)
                .HasForeignKey(x => x.MovieId)
                .IsRequired();

            //Projection-ProjectionShoppingCart 1-N (obratnata vrska ne e potrebna)
            builder.Entity<ProjectionShoppingCart>()
                .HasOne(x => x.Projection)
                .WithMany(x => x.ProjectionsInShoppingCart)
                .HasForeignKey(x => x.ProjectionId)
                .IsRequired();

            //ShoppingCart-ProjectionShoppingCart 1-N (obratnata vrska ne e potrebna)
            builder.Entity<ProjectionShoppingCart>()
                .HasOne(x => x.ShoppingCart)
                .WithMany(x => x.ProjectionsInShoppingCart)
                .HasForeignKey(x => x.ShoppingCartId)
                .IsRequired();

            //ShoppingCart-User 1-1 (ShoppingCart ja cuvame vo User)
            builder.Entity<ShoppingCart>()
                .HasOne<AppUser>(x => x.User)
                .WithOne(x => x.ShoppingCart)
                .HasForeignKey<ShoppingCart>(x => x.UserId)
                .IsRequired();


            //Projection-ProjectionOrder 1-N (obratnata vrska ne e potrebna)
            builder.Entity<ProjectionOrder>()
                .HasOne(z => z.Projection)
                .WithMany(z => z.ProjectionsInOrders)
                .HasForeignKey(z => z.ProjectionId)
                .IsRequired();

            //Order-ProjectionOrder 1-N (obratnata vrska ne e potrebna)
            builder.Entity<ProjectionOrder>()
                .HasOne(z => z.Order)
                .WithMany(z => z.ProjectionsInOrders)
                .HasForeignKey(z => z.OrderId)
                .IsRequired();
        }
    }
}
