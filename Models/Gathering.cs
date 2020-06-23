using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace csharpexam.Models
    {
        public class Gathering
        {
            [Key]
            public int gatheringID {get;set;}
            
            [Required(ErrorMessage = "Date is required")]
            [DataType(DataType.Date)]
            [Futuredate]
            public DateTime date {get;set;}

            [Required(ErrorMessage = "Time is required")]
            [DataType(DataType.Time)]
            public DateTime time {get;set;}

            [Required(ErrorMessage = "Duration in hours is required")]
            public int duration {get;set;}

            [Required(ErrorMessage = "Event title is required")]
            public string title {get;set;}

            [Required(ErrorMessage = "Event description is required")]
            public string description {get;set;}
            public int userID {get;set;}
            public User coordinator {get;set;}
            public DateTime CreatedAt { get; set; } = DateTime.Now;
            public DateTime UpdatedAt { get; set; } = DateTime.Now;

            public List<Association> participants {get;set;}
        }
       
        

    }