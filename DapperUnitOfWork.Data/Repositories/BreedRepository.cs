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

        public Breed GetById(Guid id)
        {
            return Connection.Query<Breed>(
                "SELECT * FROM Breed WHERE BreedId = @BreedId",
                param: new { BreedId = id },
                transaction: Transaction
            ).FirstOrDefault();
        }

        public Guid Insert(Breed cat)
        {
            return Connection.Query<Guid>(
                "INSERT INTO Breed(Name) VALUES(@Name); SELECT SCOPE_IDENTITY()",
                param: new { Name = cat.Name },
                transaction: Transaction
            ).FirstOrDefault();
        }

        public void Update(Breed cat)
        {
            Connection.Execute(
                "UPDATE Breed SET Name = @Name WHERE BreedId = @BreedId",
                param: new { Name = cat.Name, BreedId = cat.BreedId },
                transaction: Transaction
            );
        }

        public void Delete(Breed cat)
        {
            Connection.Execute(
                "DELETE FROM Breed WHERE BreedId = @BreedId",
                param: new { BreedId = cat.BreedId },
                transaction: Transaction
            );
        }
    }
}
