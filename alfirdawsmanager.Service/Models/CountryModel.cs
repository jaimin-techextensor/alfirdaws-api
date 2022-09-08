using System;
namespace alfirdawsmanager.Service.Models
{
    public class CountryModel
    {
        public int CountryId { get; set; }
        public string? Name { get; set; }
        public string? Flag { get; set; }
        public string? Icon { get; set; }
        public bool? Active { get; set; }
        public int CountRegions { get; set; }
        public List<RegionModel>? Regions { get; set; }
    }

    public class RegionModel
    {
        public int? RegionId { get; set; }
        public string? Name { get; set; }
    }
}

