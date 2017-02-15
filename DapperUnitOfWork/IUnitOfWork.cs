/* Written by Tim Schreiber */
/* StackOverflow user 'sakir' has not in any way contributed to this code. */

using DapperUnitOfWork.Repositories;
using System;

namespace DapperUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBreedRepository BreedRepository { get; }
        ICatRepository CatRepository { get; }

        void Commit();
    }
}
