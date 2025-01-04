using E_CommerceApp.Data;
using E_CommerceApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace E_CommerceApp.Test.Helper
{
    public class DbContextHelper
    {
        public async Task<ApplicationDBContext> GetDBContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)).Options;

            var databaseContext = new ApplicationDBContext(options);

            databaseContext.Database.EnsureCreated();
            if (databaseContext.Products.Count() > 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Products.Add(
                    new Product
                    {
                        ProductId = i,
                        Name = "Product" + i,
                        Description = "Desc",
                        Price = 3,
                        Stock = "Food"
                    });
                    databaseContext.Orders.Add(
                    new Order
                    {
                        OrderId = i,
                        OrderSummary = string.Empty,
                        ApplicantId = 1.ToString(),
                        OrderDate = null,
                        CheckedOut = false,
                        TotalAmount = 0,
                        OrderItems = new List<OrderItem>
                        {
                            new OrderItem
                            {
                                OrderId = i,
                                ProductId = i,
                                Quantity = 1,
                                Price = 100,
                            }
                        }
                    });
                }
               await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }

    }
}
