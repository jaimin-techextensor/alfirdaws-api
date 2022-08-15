using System;
namespace alfirdawsmanager.Service.Models.RequestModels
{
    public class RoleCreateRequest
    {
        public string? Name { get; set; }
        public bool? IsStatic { get; set; }
        public string? Description { get; set; }

        public List<PermissionCreateRequest>? Permissions { get; set; }
    }

    public class RoleUpdateRequest
    {
        public int? RoleId { get; set; }
        public string? Name { get; set; }
        public bool? IsStatic { get; set; }
        public string? Description { get; set; }

        public List<PermissionUpdateRequest>? Permissions { get; set; }
    }
}

