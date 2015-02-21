using DapperUnitOfWork.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperUnitOfWork.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        ICatRepository CatRepository { get; }
        IBreedRepository BreedRepository { get; }
        void SaveChanges();
    }
}
