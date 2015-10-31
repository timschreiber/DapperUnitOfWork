using DapperUnitOfWork.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace DapperUnitOfWork.Repositories
{
    internal class BreedRepository : RepositoryBase, IBreedRepository
    {
        public BreedRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public IEnumerable<Breed> All()
        {
            return Connection.Query<Breed>(
                "SELECT * FROM Breed",
                transaction: Transaction
            ).ToList();
        }

        public Breed Find(int id)
        {
            return Connection.Query<Breed>(
                "SELECT * FROM Breed WHERE BreedId = @BreedId",
                param: new { BreedId = id },
                transaction: Transaction
            ).FirstOrDefault();
        }

        public void Add(Breed entity)
        {
            entity.BreedId = Connection.ExecuteScalar<int>(
                "INSERT INTO Breed(Name) VALUES(@Name); SELECT SCOPE_IDENTITY()",
                param: new { Name = entity.Name },
                transaction: Transaction
            );
        }

        public void Update(Breed entity)
        {
            Connection.Execute(
                "UPDATE Breed SET Name = @Name WHERE BreedId = @BreedId",
                param: new { Name = entity.Name, BreedId = entity.BreedId },
                transaction: Transaction
            );
        }

        public void Delete(int id)
        {
            Connection.Execute(
                "DELETE FROM Breed WHERE BreedId = @BreedId",
                param: new { BreedId = id },
                transaction: Transaction
            );
        }

        public void Delete(Breed entity)
        {
            Delete(entity.BreedId);
        }

        public Breed FindByName(string name)
        {
            return Connection.Query<Breed>(
                "SELECT * FROM Breed WHERE Name = @Name",
                param: new { Name = name },
                transaction: Transaction
            ).FirstOrDefault();
        }
    }
}
