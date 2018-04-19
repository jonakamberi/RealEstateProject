using System;
namespace RealEstateProject.Models
{
    public class Penthouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
