namespace ClientPropertyWebApp1.Server.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "User 1",
                    Address = "Address",
                    Telephone = "0957737020",
                    InitialSumOfUserProperty = 90000000,
                    CurrentSumOfUserProperty = 100000000
                },

                new User
                {
                    Id = 2,
                    Name = "User 2",
                    Address = "Address",
                    Telephone = "0957737020",
                    InitialSumOfUserProperty = 90000000,
                    CurrentSumOfUserProperty = 100000000
                });

            modelBuilder.Entity<Property>().HasData(
                 new Property
                 {
                     Id = 1,
                     NameProperty = "Property 1",
                     TypeOfProperty = "TypeOfProperty 1",
                     PurchaseDate = DateTime.Now,
                     InitialValue = 9000,
                     PriceLossPeriod = "Year",
                     PriceLossSelectedPeriod = 10,
                     CurrentValue = 1000,
                     DaysOfPropertyOwnership = 10,
                     UserId = 1
                 },

                new Property
                {
                    Id = 2,
                    NameProperty = "Property 2",
                    TypeOfProperty = "TypeOfProperty 2",
                    PurchaseDate = DateTime.Now,
                    InitialValue = 9000000,
                    PriceLossPeriod = "Year",
                    PriceLossSelectedPeriod = 10,
                    CurrentValue = 10000000,
                    DaysOfPropertyOwnership = 10,
                    UserId = 2
                });

            modelBuilder.Entity<Property>().HasKey(p => p.Id);

            modelBuilder.Entity<Property>()
                .HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(u => u.UserId)
                .IsRequired();

            modelBuilder.Entity<User>().HasKey(u => u.Id);
        }
    }
}
