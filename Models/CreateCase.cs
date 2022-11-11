namespace Case.Web.Portal.Models
{
    public class CreateCase
    {

        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public string? CustomerEmail { get; set; } = null!;
        public int CaseTypeId { get; set; }
        public int? CustomerId { get; set; }
        public string? Status { get; set; }
       // public virtual CmcaseType? CMcaseType { get; set; }
        public virtual Cmcustomer? CMcustomer { get; set; } = null!;
        public virtual CmcustomerCare? CustomerCare { get; set; } = null!;//ResolvedAt
        public DateTime? ResolvedAt { get; set; }
        public string? CustomerCareEmail { get; set; }
        public string? CaseTypeName { get; set; }
    }
}
