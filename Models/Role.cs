using System;
using System.Collections.Generic;

namespace Case.Web.Portal.Models
{
    public partial class Role
    {
        public Role()
        {
            Cmusers = new HashSet<Cmuser>();
        }

        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastUpdateTime { get; set; }

        public virtual ICollection<Cmuser> Cmusers { get; set; }
    }
}
