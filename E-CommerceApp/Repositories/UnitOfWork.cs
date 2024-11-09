using E_CommerceApp.Data;
using E_CommerceApp.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceApp.Repositories
{
    public  class UnitOfWork : IUnitOfWork, IDisposable
    {
        public ApplicationDBContext Context = null;

        private IDbContextTransaction? _objTran = null;
        public ProductRepository Products { get; private set; }
        public OrderItemRepository OrderItems { get; private set; }
        public OrderRepository Orders { get; private set; }

        public UnitOfWork(ApplicationDBContext _Context)
        {
            Context = _Context;
            Products = new ProductRepository(Context);
            OrderItems = new OrderItemRepository(Context);
            Orders = new OrderRepository(Context);
        }

        public void CreateTransaction()
        {
            _objTran = Context.Database.BeginTransaction();
        }
        public void Commit()
        {
            _objTran?.Commit();
        }
        public void Rollback()
        {
            _objTran?.Rollback();
            _objTran?.Dispose();
        }
        public async Task Save()
        {
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
