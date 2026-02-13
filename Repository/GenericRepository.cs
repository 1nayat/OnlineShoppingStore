using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingStore.Models;

namespace OnlineShoppingStore.Repository
{
    public class GenericRepository<Tbl_Entity> : IRepository<Tbl_Entity> where Tbl_Entity : class
    {
        private DbSet<Tbl_Entity> _dbSet;
        private SafainDbContext _DBEntity;

        public GenericRepository(SafainDbContext dbEntity)
        {
            _DBEntity = dbEntity;
            _dbSet = _DBEntity.Set<Tbl_Entity>();
        }
    
        public void Add(Tbl_Entity entity)
        {
            _dbSet.Add(entity);
            _DBEntity.SaveChanges();
        }

        public int GetAllrecordCount()
        {
            return _dbSet.Count();
        }

        public IEnumerable<Tbl_Entity> GetAllRecords()
        {
            return _dbSet.ToList();
        }

        public IQueryable<Tbl_Entity> GetAllRecordsIQueryable()
        {
            return _dbSet;
        }

        public Tbl_Entity GetFirstorDeafultByParameter(Expression<Func<Tbl_Entity, bool>> wherePredict)
        {
            return _dbSet.Where(wherePredict).FirstOrDefault();
        }

        public Tbl_Entity GetFirstorDefault(int recordId)
        {
            return _dbSet.Find(recordId);
        }

        public IEnumerable<Tbl_Entity> GetListParameter(Expression<Func<Tbl_Entity, bool>> wherePredict)
        {
            return _dbSet.Where(wherePredict).ToList();
        }

        public IEnumerable<Tbl_Entity> GetProduct()
        {
            return _dbSet.ToList();
        }

       

        public IEnumerable<Tbl_Entity> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<Tbl_Entity, bool>> wherePredict, Expression<Func<Tbl_Entity, int>> orderByPredict)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tbl_Entity> GetResultBySqlProcedure(string query, params object[] parameters)
        {
            if (parameters != null && parameters.Length > 0)
            {
                return _DBEntity.Set<Tbl_Entity>().FromSqlRaw(query, parameters).ToList();
            }
            else
            {
                return _DBEntity.Set<Tbl_Entity>().FromSqlRaw(query).ToList();
            }
        }

        // Fixed the method name and implementation as requested.
        public IEnumerable<Tbl_Entity> GetResultBySqlprocedure(string query, params object[] parameters)
        {
            if (parameters != null && parameters.Length > 0)
            {
                return _DBEntity.Set<Tbl_Entity>().FromSqlRaw(query, parameters).ToList();
            }
            else
            {
                return _DBEntity.Set<Tbl_Entity>().FromSqlRaw(query).ToList();
            }
        }

        public void InactiveAndDeleteMarkByWhereCaluse(Expression<Func<Tbl_Entity, bool>> wherePredict, Action<Tbl_Entity> ForEachPredict)
        {
            _dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }

        public void Remove(Tbl_Entity entity)
        {
            if (_DBEntity.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            _dbSet.Remove(entity);
            _DBEntity.SaveChanges();
        }

        public void RemoveByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict)
        {
            Tbl_Entity entity = _dbSet.Where(wherePredict).FirstOrDefault();
            Remove(entity);
        }

        public void RemoveRangeByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict)
        {
            List<Tbl_Entity> entity = _dbSet.Where(wherePredict).ToList();
            foreach (var ent in entity)
            {
                Remove(ent);
            }
        }

        public void Update(Tbl_Entity entity)
        {
            _dbSet.Attach(entity); 
            _DBEntity.Entry(entity).State = EntityState.Modified; 
            _DBEntity.SaveChanges(); 
        }


        public void updateByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict, Action<Tbl_Entity> ForEachPredict)
        {
            _dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }
    }
}
