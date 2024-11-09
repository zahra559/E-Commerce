namespace E_CommerceApp.Dtos.Requests.Order
{
    public class GetOrdersDto
    {
        public string ApplicantId { get; set; }
        public bool? CheckedOut { get; set; }
        public int? OrderId { get; set;}
    }
}
