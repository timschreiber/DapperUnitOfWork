using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperUnitOfWork.Domain.Entities
{
    public class Breed
    {
        public Guid BreedId { get; set; }
        public string Name { get; set; }
    }
}
