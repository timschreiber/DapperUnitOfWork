using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperUnitOfWork.Domain.Entities
{
    public class Cat
    {
        public Guid CatId { get; set; }
        public Guid BreedId { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
    }
}
