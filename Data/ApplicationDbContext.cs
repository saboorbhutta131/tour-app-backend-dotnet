using Microsoft.EntityFrameworkCore;
using Models;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options){

    }
    public DbSet<Booking> Booking {get;set;}    
    public DbSet<Place> Place {get;set;}    
    
    public DbSet<Review> Review {get;set;}    
    public DbSet<Tour> Tour {get;set;}    
    public DbSet<User> User {get;set;}    

    protected override void OnModelCreating(ModelBuilder builder){
        // Primary Keys
        builder.Entity<User>().HasKey(user => user.Id);
        builder.Entity<Place>().HasKey(pl => pl.Id);
        builder.Entity<Tour>().HasKey(tr => tr.Id);
        builder.Entity<Booking>().HasKey(b => new {b.TourId,b.UserId});
        builder.Entity<Review>().HasKey(r => new {r.TourId,r.UserId});

        // Relations
        builder.Entity<Tour>().HasOne(tr => tr.Place)
        .WithMany(pl => pl.Tours)
        .HasForeignKey(tr => tr.PlaceId);

        builder.Entity<Booking>()
        .HasOne(b => b.Tour)
        .WithMany(t => t.Bookings)
        .HasForeignKey(b => b.TourId);
        
        builder.Entity<Booking>()
        .HasOne(b => b.User)
        .WithMany(u => u.Bookings)
        .HasForeignKey(b => b.UserId);

        //Index
        builder.Entity<User>().HasIndex(u =>  u.Email).IsUnique();

        base.OnModelCreating(builder);
    }
}