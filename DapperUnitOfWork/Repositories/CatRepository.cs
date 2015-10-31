using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DapperUnitOfWork.Entities;

namespace DapperUnitOfWork.Repositories
{
    internal class CatRepository : RepositoryBase, ICatRepository
    {
        public CatRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public IEnumerable<Cat> All()
        {
            return Connection.Query<Cat>(
                "SELECT * FROM Cat"
            );
        }

        public Cat Find(int id)
        {
            return Connection.Query<Cat>(
                "SELECT * FROM Cat WHERE CatId = @CatId",
                param: new { CatId = id },
                transaction: Transaction
            ).FirstOrDefault();
        }

        public void Add(Cat entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            entity.CatId = Connection.ExecuteScalar<int>(
                "INSERT INTO Cat(BreedId, Name, Age) VALUES(@BreedId, @Name, @Age); SELECT SCOPE_IDENTITY()",
                param: new { BreedId = entity.BreedId, Name = entity.Name, Age = entity.Age },
                transaction: Transaction
            );
        }

        public void Update(Cat entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Connection.Execute(
                "UPDATE Cat SET BreedId = @BreedId, Name = @Name, Age = @Age WHERE CatId = @CatId",
                param: new { CatId = entity.CatId, BreedId = entity.BreedId, Name = entity.Name, Age = entity.Age },
                transaction: Transaction
            );
        }

        public void Remove(Cat entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Remove(entity.CatId);
        }

        public void Remove(int id)
        {
            Connection.Execute(
                "DELETE FROM Cat WHERE CatId = @CatId",
                param: new { CatId = id },
                transaction: Transaction
            );
        }

        public IEnumerable<Cat> FindByBreedId(int breedId)
        {
            return Connection.Query<Cat>(
                "SELECT * FROM Cat WHERE BreedId = @BreedId",
                param: new { BreedId = breedId },
                transaction: Transaction
            );
        }
    }
}
