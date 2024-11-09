using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceApp.Models
{
    [Table("Orders")]
    public class Order
    {
        public int OrderId { get; set; }
        public string ApplicantId { get; set; }
        public AppUser? Applicant { get; set; }
        public DateTime? OrderDate { get; set; }
        public string OrderSummary { get; set; } = string.Empty;
        public bool CheckedOut { get; set; } = false;
        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalAmount { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
