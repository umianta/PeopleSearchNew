using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace PeopleSearchApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        
        [StringLength(500)]
        public string Address { get; set; }

       
        public string Interests { get; set; }
          
        public int Age { get; set; }
        public string PicPath { get; set; }
    }
}