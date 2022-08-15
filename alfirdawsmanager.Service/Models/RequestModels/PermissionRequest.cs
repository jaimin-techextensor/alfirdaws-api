using System;
namespace alfirdawsmanager.Service.Models.RequestModels
{
    public class PermissionCreateRequest
    {
        public int? ModuleId { get; set; }
        public bool? Create { get; set; }
        public bool? Read { get; set; }
        public bool? Update { get; set; }
        public bool? Delete { get; set; }
    }

    public class PermissionUpdateRequest
    {
        public int? PermissionId { get; set; }
        public int? ModuleId { get; set; }
        public bool? Create { get; set; }
        public bool? Read { get; set; }
        public bool? Update { get; set; }
        public bool? Delete { get; set; }
    }
}

