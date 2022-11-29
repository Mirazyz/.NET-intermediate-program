using CatalogService.Domain.Interfaces.Repositories;

namespace CatalogService.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public T Create(T entity)
        {
            throw new NotImplementedException();
        }

        public void CreateRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EntityExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindAllAsync(int pageSize = 15, int pageNumber = 0)
        {
            throw new NotImplementedException();
        }

        public Task<T?> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}
