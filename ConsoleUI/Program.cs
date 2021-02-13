using System;
using Business.Concrete;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace ConsoleUI
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            Test1();
        }

        private static void Test1()
        {
            Console.WriteLine("-----BEFORE UPDATE-----");
            var carManager = new CarManager(new InMemoryCarDal());
            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine(car.ToString());
            }

            var toUpdate = new Car
            {
                Id = 1,
                Description = "Kirli"
            };
            carManager.Update(toUpdate);

            Console.WriteLine("-----AFTER UPDATE-----");
            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine(car.ToString());
            }
        }
    }
}