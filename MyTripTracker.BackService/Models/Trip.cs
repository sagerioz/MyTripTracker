using System;
using System.ComponentModel.DataAnnotations;

namespace MyTripTracker.BackService.Models
{
    public class Trip
    {
        // Entity framework will assume that anything called ID or type of Id (ex "TripId") 
        // is automatically going to be the unique key. If this column was called "TheId", this breaks the convention and
        //then you must configure to help EF understand that you want it to be the unique key:
       
        // public int IanyWeidNameForYourCustomKeyIdThatBreaksConventiond { get; set; }


        //EF and MVC share data annotations such as "key", "String Length".
        // 

        // this is data annotation ex:[Key] to declare this as a key, and as required:
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
