namespace TVSeriesAPI.DAL.Repositories
{
    public interface IRepository<TModel>
    {
        Task<TModel> GetById(int id);
        Task<ICollection<TModel>> GetAll();
        Task AddAsync(TModel obj);
        Task DeleteAsync(TModel obj);
        Task UpdateAsync(TModel obj);
        Task<bool> SaveChanges();
    }
}
