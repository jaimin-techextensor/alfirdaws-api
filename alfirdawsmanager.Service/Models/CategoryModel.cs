using System;
namespace alfirdawsmanager.Service.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public byte[]? Icon { get; set; }
        public bool? Active { get; set; }
        public int CountSubcategories { get; set; }
    }
}

