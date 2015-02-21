using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperUnitOfWork.Domain.Entities
{
    public class Cat
    {
        public int CatId { get; set; }
        public int BreedId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
