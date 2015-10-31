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