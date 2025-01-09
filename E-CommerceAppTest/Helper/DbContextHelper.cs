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
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)).Options;

            var databaseContext = new ApplicationDBContext(options);
            databaseContext.Database.EnsureCreated();
            for (int i = 0; i < 10; i++)
            {
                if (databaseContext.Orders.Count() < 10)
                {
                    databaseContext.Orders.Add(
                        new Order
                        {
                            OrderId = i + 1,
                            OrderSummary = string.Empty,
                            ApplicantId = (i + 1).ToString(),
                            Applicant = new AppUser
                            {
                                Id = (i + 1).ToString(),
                                Email = "test.gmail.com",
                                UserName = "test",
                                NormalizedUserName = "test"
                            },
                            OrderDate = null,
                            CheckedOut = false,
                            TotalAmount = 0,
                            OrderItems = new List<OrderItem>
                            {
                                new OrderItem
                                {
                                    OrderItemId = i + 1,
                                    OrderId = i + 1,
                                    ProductId = i + 1 ,
                                    Product= new Product
                                    {
                                        ProductId = i + 1,
                                        Name = "Product" + (i + 1).ToString(),
                                        Description = "Desc",
                                        Price = 3,
                                        Stock = "Food",
                                        //ImageUrl ="Resources\\Files\\test.png"
                                    },
                                    Quantity = 1,
                                    Price = 100,
                                }
                            }
                        });
                }
            }
               await databaseContext.SaveChangesAsync();
               return databaseContext;
        }

    }
}
