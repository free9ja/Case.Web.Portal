using System;
using System.Collections.Generic;

namespace Case.Web.Portal.Models
{
    public partial class CmcaseType
    {
        public CmcaseType()
        {
            Cmcases = new HashSet<Cmcase>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Cmcase> Cmcases { get; set; }
    }
}
