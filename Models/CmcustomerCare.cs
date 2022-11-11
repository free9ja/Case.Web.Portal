using System;
using System.Collections.Generic;

namespace Case.Web.Portal.Models
{
    public partial class CmcustomerCare
    {
        public CmcustomerCare()
        {
            Cmcases = new HashSet<Cmcase>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? PhoneNumber { get; set; }

        public virtual ICollection<Cmcase> Cmcases { get; set; }
    }
}
