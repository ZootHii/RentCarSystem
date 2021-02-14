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
            foreach (var car in carManager.GetAllCars())
            {
                Console.WriteLine(car.ToString());
            }

            Console.WriteLine("-----GET ALL BY BRAND-----");
            foreach (var car in carManager.GetCarsByBrandId(1))
            {
                Console.WriteLine(car.ToString());
            }

            Console.WriteLine("-----GET ALL BY COLOR-----");
            foreach (var car in carManager.GetCarsByColorId(3))
            {
                Console.WriteLine(car.ToString());
            }

            Console.WriteLine("-----GET BY ID-----");
            Console.WriteLine(carManager.GetCarById(4).ToString());
            
            Console.WriteLine("-----ADD CAR-----");
            carManager.Add(new Car{BrandId = 3, ColorId = 5, ModelYear = new DateTime(1777, 01, 01), DailyPrice = 300.0M, Description = "Boş"});
            carManager.Add(new Car{BrandId = 3, ColorId = 5, ModelYear = new DateTime(2000, 01, 01), DailyPrice = 0M, Description = "Boş"});
            // it works don't add again //carManager.Add(new Car{BrandId = 3, ColorId = 5, ModelYear = new DateTime(2005, 01, 01), DailyPrice = 300.0M, Description = "Boş"});
            foreach (var car in carManager.GetAllCars())
            {
                Console.WriteLine(car.ToString());
            }  
        }

        private static void Test1()
        {
            Console.WriteLine("-----BEFORE UPDATE-----");
            var carManager = new CarManager(new InMemoryCarDal());
            foreach (var car in carManager.GetAllCars())
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
            foreach (var car in carManager.GetAllCars())
            {
                Console.WriteLine(car.ToString());
            }
        }
    }
}