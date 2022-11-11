using System;
using System.Collections.Generic;

namespace Case.Web.Portal.Models
{
    public partial class Cmuser
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Token { get; set; }
        public string? Password { get; set; }
        public DateTime? Created { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? LastUpdateTime { get; set; }
        public bool IsActive { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int RoleId { get; set; }
        public bool PasswordExpired { get; set; }
        public string? PhoneNumberOne { get; set; }
        public string? PhoneNumberTwo { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Role Role { get; set; } = null!;
    }
}
