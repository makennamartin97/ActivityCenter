using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

    namespace csharpexam.Models
    {
        public class LoginUser
        {
            [EmailAddress]
            [Required(ErrorMessage = "Email is required")]
            public string loginemail{get;set;}

            [DataType(DataType.Password)]
            [Required(ErrorMessage = "Password is required")]
            public string loginpassword {get;set;}
        }
    }