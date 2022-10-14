namespace alfirdawsmanager.Service.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Picture { get; set; }
        public string Email { get; set; }
        public bool? Active { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public bool? IsPasswordChanged { get; set; }
        public bool? SendActivationEmail { get; set; }
        public bool? ChangePwdAtNextLogin { get; set; }
    }


    public class UserModelResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Picture { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public DateTime LastLoginTime { get; set; }
        public bool IsPasswordChanged { get; set; }
        public bool SendActivationEmail { get; set; }
        public bool ChangePwdAtNextLogin { get; set; }
        public List<AssignedRoles>? AssignedRoles { get; set; }
    }

    public class AssignedRoles
    {
        public int RoleId { get; set; }
        public string? Name { get; set; }
    }
}
