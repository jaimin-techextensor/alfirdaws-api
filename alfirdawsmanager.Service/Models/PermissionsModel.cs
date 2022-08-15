using System;
namespace alfirdawsmanager.Service.Models
{
    public class PermissionsModel
    {
        public int? PermissionId { get; set; }
        public int? ModuleId { get; set; }
        public string? ModuleName { get; set; }
        public bool? Create { get; set; }
        public bool? Read { get; set; }
        public bool? Update { get; set; }
        public bool? Delete { get; set; }
    }
}

