using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HotelProject.EntityLayer.Concrete;

namespace HotelProject.DataAccessLayer.Concrete
{
    public class Context:IdentityDbContext<AppUser,AppRole,int> //normalde identityde id stringdir biz key olarak int verdik
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-EQJBFL9\\SQLEXPRESS;initial catalog=ApiDb ; integrated security=true ;TrustServerCertificate=True");
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Room>(entry =>
            {
                entry.ToTable("Rooms", tb =>
                {
                    tb.HasTrigger("RoomDecrease");
                    tb.HasTrigger("RoomIncrease");
                });    
            });
            builder.Entity<Staff>(entry =>
            {
                entry.ToTable("Staffs", tb =>
                {
                    tb.HasTrigger("StaffDecrease");
                    tb.HasTrigger("StaffIncrease");
                });
            });
            builder.Entity<Guest>(entry =>
            {
                entry.ToTable("Guests", tb =>
                {
                    tb.HasTrigger("GuestDecrease");
                    tb.HasTrigger("GuestIncrease");
                });
            });

        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<SendMessage> SendMessages { get; set; }


    }
}
