using System;
using System.ComponentModel.DataAnnotations;

namespace RealEstateProject.Models
{
    public class Property
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String title { get; set; }

        [Required]
        public String description { get; set; }

        [Url]
        [Required]
        public String URL { get; set; }

        public int rooms { get; set; }

        [Required]
        public int price { get; set; }

        [Required]
        public String address { get; set; }
    }

}
