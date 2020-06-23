using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace csharpexam.Models
    {
        public class User
        {
            [Key]
            public int userID {get;set;}

            [Required(ErrorMessage = "Name is required")]
            [MinLength(2, ErrorMessage="Name must be at least 2 characters")]
            public string name {get;set;}


            [Required(ErrorMessage = "Email is required")]
            [EmailAddress]
            public string email {get;set;}
            
            [DataType(DataType.Password)]
            [Required(ErrorMessage = "Password is required")]
            [PasswordValidations]
            public string password {get;set;}
            public DateTime CreatedAt {get;set;} = DateTime.Now;
            public DateTime UpdatedAt {get;set;} = DateTime.Now;
            public List<Association> attendinggatherings {get;set;}
            public List<Gathering> plannedgatherings {get;set;}

            [NotMapped]
            [DataType(DataType.Password)]
            [Compare("password", ErrorMessage="Does not match")]
            public string confirm {get;set;}

            
            
        }
    }