using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class User
    {
        public User()
        {
            AssignedRoles = new HashSet<AssignedRole>();
            Cases = new HashSet<Case>();
        }

        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public byte[]? Picture { get; set; }
        public string? Email { get; set; }
        public bool? Active { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public bool? IsPasswordChanged { get; set; }
        public bool? SendActivationEmail { get; set; }
        public bool? ChangePwdAtNextLogin { get; set; }

        public virtual ICollection<AssignedRole> AssignedRoles { get; set; }
        public virtual ICollection<Case> Cases { get; set; }
    }
}
