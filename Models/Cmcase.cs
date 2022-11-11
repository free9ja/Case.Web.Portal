using System;
using System.Collections.Generic;

namespace Case.Web.Portal.Models
{
    public partial class Cmcase
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public string Status { get; set; } = null!;
        public int CaseTypeId { get; set; }
        public int CMcaseTypeId { get; set; }
        public int CustomerId { get; set; }
        public int CMcustomerId { get; set; }
        public int? CustomerCareId { get; set; }
        public int State { get; set; }

        public virtual CmcaseType CMcaseType { get; set; } = null!;
        public virtual Cmcustomer CMcustomer { get; set; } = null!;
        public virtual CmcustomerCare? CustomerCare { get; set; }
    }
}
