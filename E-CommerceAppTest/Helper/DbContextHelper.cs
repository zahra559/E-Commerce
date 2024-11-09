using E_CommerceApp.Data;
using E_CommerceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceApp.Test.Helper
{
    public class DbContextHelper
    {
        public ApplicationDBContext GetDBContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var databaseContext = new ApplicationDBContext(options);

            databaseContext.Database.EnsureCreated();
            if (databaseContext.Products.Count() > 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Products.Add(
                    new Product
                    {
                        Name = "Product" + i,
                        Description = "Desc",
                        Price = 3,
                        Stock = "Food"
                    });
                }
                databaseContext.SaveChanges();
            }
            return databaseContext;
        }

    }
}
