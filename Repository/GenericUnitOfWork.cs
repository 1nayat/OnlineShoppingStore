using OnlineShoppingStore.Models;

namespace OnlineShoppingStore.Repository
{
    public class GenericUnitOfWork:IDisposable
    {
        private SafainDbContext DBEntity = new SafainDbContext();
        public IRepository <Tbl_EntityType>GetRepositoryInstances<Tbl_EntityType>() where Tbl_EntityType : class
        {
            return new GenericRepository<Tbl_EntityType>(DBEntity);
        }
        public void SaveChanges()
        {
            DBEntity.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DBEntity.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
         Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;
    }
}
