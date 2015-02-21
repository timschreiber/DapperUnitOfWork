using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperUnitOfWork.Domain.Repositories
{
    public interface IRepository<TEntity, TId> where TEntity : class
    {
        IList<TEntity> GetAll();
        TEntity GetById(TId id);
        void Insert(TEntity cat);
        void Update(TEntity cat);
        void Delete(TEntity cat);
    }
}
