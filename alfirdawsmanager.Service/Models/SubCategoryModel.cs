using System;
namespace alfirdawsmanager.Service.Models
{
    public class SubCategoryModel
    {
        public int SubCategoryId { get; set; }
        public string? Name { get; set; }
        public int Sequence { get; set; }
        public string? Icon { get; set; }
        public bool? Active { get; set; }
    }
}

