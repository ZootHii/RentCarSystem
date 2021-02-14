using System;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace ConsoleUI
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            //Test1();
            Test2();
        }

        private static void Test2()
        {
            var carManager = new CarManager(new EFCarDal());
            Console.WriteLine("-----GET ALL-----");
            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine(car.ToString());
            }

            Console.WriteLine("-----GET ALL BY BRAND-----");
            foreach (var car in carManager.GetAllByBrandId(1))
            {
                Console.WriteLine(car.ToString());
            }

            Console.WriteLine("-----GET ALL BY COLOR-----");
            foreach (var car in carManager.GetAllByColorId(3))
            {
                Console.WriteLine(car.ToString());
            }

            Console.WriteLine("-----GET BY ID-----");
            Console.WriteLine(carManager.GetById(4).ToString());
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