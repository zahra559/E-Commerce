namespace E_CommerceApp.Dtos.Requests.Product
{
    public class GetProductsDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? ProductName { get; set; }
        public string? StockName { get; set; }
    }
}
