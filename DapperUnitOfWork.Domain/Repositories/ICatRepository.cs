using DapperUnitOfWork.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperUnitOfWork.Domain.Repositories
{
    public interface ICatRepository : IRepository<Cat, Guid>
    {
    }
}
