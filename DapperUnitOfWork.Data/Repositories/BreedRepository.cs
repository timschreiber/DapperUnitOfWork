using DapperUnitOfWork.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperUnitOfWork.Domain.Entities;

namespace DapperUnitOfWork.Data.Repositories
{
    public class BreedRepository : RepositoryBase, IBreedRepository
    {
        public BreedRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public IList<Breed> GetAll()
        {
            return Connection.Query<Breed>(
                "SELECT * FROM Breed",
                transaction: Transaction
            ).ToList();
        }

        public Breed GetById(int id)
        {
            return Connection.Query<Breed>(
                "SELECT * FROM Breed WHERE BreedId = @BreedId",
                param: new { BreedId = id },
                transaction: Transaction
            ).FirstOrDefault();
        }

        public void Insert(Breed breed)
        {
            var breedId = Connection.ExecuteScalar<int>(
                "INSERT INTO Breed(Name) VALUES(@Name); SELECT SCOPE_IDENTITY()",
                param: new { Name = breed.Name },
                transaction: Transaction
            );
            breed.BreedId = breedId;
        }

        public void Update(Breed breed)
        {
            Connection.Execute(
                "UPDATE Breed SET Name = @Name WHERE BreedId = @BreedId",
                param: new { Name = breed.Name, BreedId = breed.BreedId },
                transaction: Transaction
            );
        }

        public void Delete(Breed breed)
        {
            Connection.Execute(
                "DELETE FROM Breed WHERE BreedId = @BreedId",
                param: new { BreedId = breed.BreedId },
                transaction: Transaction
            );
        }

        public Breed GetByName(string name)
        {
            return Connection.Query<Breed>(
                "SELECT * FROM Breed WHERE Name = @Name", 
                param: new { Name = name }, 
                transaction: Transaction
            ).FirstOrDefault();
        }
    }
}
