using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyTripTracker.BackService.Models;

namespace MyTripTracker.BackService.Data
{
    public class TripContext : DbContext
    {
        public TripContext(DbContextOptions<TripContext> options) : base(options) { }
        // this next line is called reflection:
        public TripContext() { }
        public DbSet<Trip> Trips { get; set; }
        // Allows you to use a custom id as primary key, using a lambda:
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // { modelBuilder.Entity<Trip>().HasKey(t => t.anyWeidNameForYourCustomKeyIdThatBreaksConvention); }

        public static void SeedData(IServiceProvider serviceProvider)
        // http://thedatafarm.com/uncategorized/seeding-ef-with-json-data


        {

            using (var serviceScope = serviceProvider
                  .GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                // this next line grants you access to the context:
                var context = serviceScope
                   .ServiceProvider.GetService<TripContext>();

                context.Database.EnsureCreated();


                if (context.Trips.Any()) return;
                context.Trips.AddRange(new Trip[]
                 {
                new Trip
                {
                    Id = 1,
                    Name = "Mt.Sopris",
                    StartDate = new DateTime(2018, 3, 27),
                    EndDate = new DateTime(2018, 4, 22)
                },
            new Trip
            {
                Id = 2,
                Name = "Dead Man Lake",
                StartDate = new DateTime(2018, 4, 30),
                EndDate = new DateTime(2018, 5, 22)
            },
            new Trip
            {
                Id = 3,
                Name = "Marble",
                StartDate = new DateTime(2018, 6, 27),
                EndDate = new DateTime(2018, 7, 22)
            }
               }
              );
                context.SaveChanges();
            }
        }


    }
}