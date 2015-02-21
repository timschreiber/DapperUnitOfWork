using DapperUnitOfWork.Data;
using DapperUnitOfWork.Domain;
using DapperUnitOfWork.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperUnitOfWork.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var breed1 = new Breed { Name = "Orange Mackerel" };
            var cat1 = new Cat { Name = "Cheddar", Age = 4 };

            using(var uow = new UnitOfWork("LosGatos"))
            {
                uow.BreedRepository.Insert(breed1);
                cat1.BreedId = breed1.BreedId;
                uow.CatRepository.Insert(cat1);
                uow.SaveChanges();
            }

            System.Console.WriteLine("OK");
            System.Console.ReadKey();
        }

        /*
        static void Test1()
        {
            var breed1 = new Breed { Name = "Egyptian Mau" };
            var breed2 = new Breed { Name = "Arabian Mau" };

            var cat1 = new Cat { Name = "Pharoh", Age = 4 };
            var cat2 = new Cat { Name = "Tut", Age = 2 };
            var cat3 = new Cat { Name = "Anas", Age = 8 };

            using (var uow = new UnitOfWork("LosGatos"))
            {
                uow.BreedRepository.Insert(breed1);
                uow.BreedRepository.Insert(breed2);

                cat1.BreedId = breed1.BreedId;
                cat2.BreedId = breed1.BreedId;
                cat3.BreedId = breed2.BreedId;

                uow.CatRepository.Insert(cat1);
                uow.CatRepository.Insert(cat2);
                uow.CatRepository.Insert(cat3);

                uow.SaveChanges();
            }
        }
        */
    }
}
