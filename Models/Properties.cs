using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rentisha.Models
{
    public class Properties
    {
        [Key]
        public int listing_id { get; set; }
       
        [DisplayName("County:")]
        [Required]
        public string County { get; set; }
      
        [DisplayName("Town/Area")]
        [Required]
        public string Town { get; set; }
      
        [DisplayName("Number of Rooms ")]
        [Required]
        public int Rooms { get; set; }
      
        [DisplayName("Rent/Price")]
        [Required]
        public int Price { get; set; }
       
        [DisplayName("Property Name")]
        [Required]
        public string PropertyName { get; set; }
       
        [DisplayName("Property Type")]
        [Required]
         public string PropertyType { get; set; }
       
        [Required]
        public int Rent { get; set; }
    }
}