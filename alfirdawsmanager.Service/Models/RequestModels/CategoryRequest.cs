using System;
namespace alfirdawsmanager.Service.Models.RequestModels
{
    public class CategoryCreateRequest
    {
        public string? Name { get; set; }
        public int Sequence { get; set; }
        public string? Icon { get; set; }
        public bool? Active { get; set; }
    }

    public class CategoryUpdateRequest
    {
        public int? CategoryId { get; set; }
        public string? Name { get; set; }
        public int Sequence { get; set; }
        public string? Icon { get; set; }
        public bool? Active { get; set; }
    }

    public class SubCategoryCreateRequest
    {
        public string? Name { get; set; }
        public int Sequence { get; set; }
        public string? Icon { get; set; }
        public bool? Active { get; set; }
    }

    public class SubCategoryUpdateRequest
    {
        public int SubCategoryId { get; set; }
        public string? Name { get; set; }
        public int Sequence { get; set; }
        public string? Icon { get; set; }
        public bool? Active { get; set; }
    }
}

