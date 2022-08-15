using System;
namespace alfirdawsmanager.Service.Models
{
    public class RoleModel
    {
        public int? RoleId { get; set; }
        public string? Name { get; set; }
        public bool? IsStatic { get; set; }
        public string? Description { get; set; }

        public List<PermissionsModel>? Permissions { get; set; }
    }
}

