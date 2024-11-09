using System.ComponentModel.DataAnnotations;

namespace E_CommerceApp.Dtos.Requests.Order
{
    public class CheckoutOrderDto
    {
        [Required]
        public string Summary { get; set; }
    }
}
