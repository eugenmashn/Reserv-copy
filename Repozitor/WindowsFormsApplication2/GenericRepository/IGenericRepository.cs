using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFAplicationVacation
{
    public interface IGenericRepository<TEntity> where TEntity:class 
    {
        void Create(TEntity item);
        TEntity FindById(Guid id);
        TEntity FindById(Func<TEntity, bool> predicate);
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        void Remove(TEntity item);
        int Count(Func<TEntity, bool> predicate);
        int Count();
        IEnumerable<TEntity> GetSort(Func<TEntity, string> predicate);
        void Update(TEntity item);
    }
}
