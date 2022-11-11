using Case.Web.Portal.Models;

namespace Case.Web.Portal.ViewModels
{
    public class AdminCase
    {   public int ID { get; set; }
        public Cmcase? Case { get; set; }
        public CmcaseType? CaseType { get; set; }
        public Cmcustomer? Customer { get; set; }
        public CmcustomerCare? CustomerCare { get; set; }
       // public string?  { get; set; }
       // public string? CaseId { get; set; }
    }
}
