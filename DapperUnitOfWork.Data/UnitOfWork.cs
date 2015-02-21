using DapperUnitOfWork.Data.Repositories;
using DapperUnitOfWork.Domain;
using DapperUnitOfWork.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperUnitOfWork.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        private ICatRepository _catRepository;
        private IBreedRepository _breedRepository;

        public UnitOfWork(string connectionName)
        {
            var connectionFactory = new ConnectionFactory(connectionName);
            _connection = connectionFactory.Create();
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        #region IUnitOfWork Members
        public ICatRepository CatRepository
        {
            get { return _catRepository ?? (_catRepository = new CatRepository(_transaction)); }
        }

        public IBreedRepository BreedRepository
        {
            get { return _breedRepository ?? (_breedRepository = new BreedRepository(_transaction)); }
        }

        public void SaveChanges()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if(!disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Close();
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                disposed = true;
            }
        }

        private void resetRepositories()
        {
            _catRepository = null;
            _breedRepository = null;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
