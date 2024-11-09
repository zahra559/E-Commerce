namespace E_CommerceApp.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int Id);
        Task<T> InsertAsync(T Entity);
        Task<T?> UpdateAsync(T Entity);
        Task<T?> DeleteAsync(int Id);
        Task SaveAsync();

    }
}
