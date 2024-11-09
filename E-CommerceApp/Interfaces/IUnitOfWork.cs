using E_CommerceApp.Repositories;

namespace E_CommerceApp.Interfaces
{
    public interface IUnitOfWork
    {
        //Define the Specific Repositories
        ProductRepository Products { get; }
        OrderItemRepository OrderItems { get; }
        OrderRepository Orders { get; }


        void CreateTransaction();

        void Commit();

        void Rollback();

        Task Save();
    }
}
