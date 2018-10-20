using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using MyTripTracker.BackService.Models;

// this is test data - that's why there's an id listed here. For SQL no neeed to add this property

namespace MyTripTracker.BackService.Models
{
    public class Repository
    {
        // private list of type <Trip>
        private List<Trip> MyTrips = new List<Trip>
        {
            new Trip {
                Id = 1,
                Name = "Mt.Sopris",
                StartDate = new DateTime(2018, 3, 27),
                EndDate = new DateTime(2018, 4, 22)
            },
            new Trip {
                Id = 2,
                Name = "Dead Man Lake",
                StartDate = new DateTime(2018, 4, 30),
                EndDate = new DateTime(2018, 5, 22)
            },
            new Trip {
                Id = 3,
                Name = "Marble",
                StartDate = new DateTime(2018, 6, 27),
                EndDate = new DateTime(2018, 7, 22)
            }
        };
        public List<Trip> Get()
        {
            return MyTrips;
        }
        public Trip Get(int id)
        {
            return MyTrips.First(t => t.Id == id);
        }
        public void Add(Trip newTrip)
        {
            MyTrips.Add(newTrip);
        }
        public void Update(Trip tripToUpdate)
        {
            MyTrips.Remove(MyTrips.First(t => t.Id == tripToUpdate.Id));
            Add(tripToUpdate);
        }
        public void Remove(int id)
        {
            MyTrips.Remove(MyTrips.First(t => t.Id == id));
        }
    }
};
