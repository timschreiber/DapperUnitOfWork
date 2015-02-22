using DapperUnitOfWork.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperUnitOfWork.Domain;
using DapperUnitOfWork.Domain.Repositories;

namespace DapperUnitOfWork.Data.Repositories
{
    internal class CatRepository : RepositoryBase, ICatRepository
    {
        public CatRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        #region ICatRepository Members
        public IList<Cat> GetAll()
        {
            return Connection.Query<Cat>(
                "SELECT * FROM Cat",
                transaction: Transaction
            ).ToList();
        }

        public Cat GetById(int id)
        {
            return Connection.Query<Cat>(
                "SELECT * FROM Cat WHERE CatId = @CatId",
                param: new { CatId = id },
                transaction: Transaction
            ).FirstOrDefault();
        }

        public void Insert(Cat cat)
        {
            var catId = Connection.ExecuteScalar<int>(
                "INSERT INTO Cat(BreedId, Name, Age) VALUES(@BreedId, @Name, @Age); SELECT SCOPE_IDENTITY()",
                param: new { BreedId = cat.BreedId, Name = cat.Name, Age = cat.Age },
                transaction: Transaction
            );
            cat.CatId = catId;
        }

        public void Update(Cat cat)
        {
            Connection.Execute(
                "UPDATE Cat SET BreedId = @BreedId, Name = @Name, Age = @Age WHERE CatId = @CatId",
                param: new { CatId = cat.CatId, BreedId = cat.BreedId, Name = cat.Name, Age = cat.Age },
                transaction: Transaction
            );
        }

        public void Delete(Cat cat)
        {
            Connection.Execute(
                "DELETE FROM Cat WHERE CatId = @CatId",
                param: new { CatId = cat.CatId },
                transaction: Transaction
            );
        }

        public IList<Cat> GetByBreedId(int breedId)
        {
            return Connection.Query<Cat>(
                "SELECT * FROM Cat WHERE BreedId = @BreedId",
                param: new { BreedId = breedId },
                transaction: Transaction
            ).ToList();
        }
        #endregion
    }
}
