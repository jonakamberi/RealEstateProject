using System;
using System.ComponentModel.DataAnnotations;

namespace RealEstateProject.Models
{
    public class PropertyType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String name { get; set; }
    }
}
