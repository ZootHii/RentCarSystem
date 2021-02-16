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
            //Test2();
            //Test3();
            Test4();
        }

        private static void Test4()
        {
            var carManager = new CarManager(new EfCarDal());
            Console.WriteLine("-----GET ALL CARS-----");
            var resultGetAllCars = carManager.GetAllCars();
            if (resultGetAllCars.Success)
            {
                foreach (var car in resultGetAllCars.Data)
                {
                    Console.WriteLine(car.ToString());
                }

                Console.WriteLine("\n" + resultGetAllCars.Message);
            }
            else
            {
                Console.WriteLine("\n" + resultGetAllCars.Message);
            }

            Console.WriteLine("-----GET CAR DETAILS-----");
            var resultGetCarDetails = carManager.GetCarDetails();
            if (resultGetCarDetails.Success)
            {
                foreach (var car in resultGetCarDetails.Data)
                {
                    Console.WriteLine(car.ToString());
                }

                Console.WriteLine("\n" + resultGetCarDetails.Message);
            }
            else
            {
                Console.WriteLine("\n" + resultGetCarDetails.Message);
            }

            Console.WriteLine("-----GET ALL BY BRAND-----");
            var resultGetCarsByBrandId = carManager.GetCarsByBrandId(1);
            if (resultGetCarsByBrandId.Success)
            {
                foreach (var car in resultGetCarsByBrandId.Data)
                {
                    Console.WriteLine(car.ToString());
                }

                Console.WriteLine("\n" + resultGetCarsByBrandId.Message);
            }
            else
            {
                Console.WriteLine("\n" + resultGetCarsByBrandId.Message);
            }

            Console.WriteLine("-----GET ALL BY COLOR-----");
            var resultGetCarsByColorId = carManager.GetCarsByColorId(3);
            if (resultGetCarsByColorId.Success)
            {
                foreach (var car in resultGetCarsByColorId.Data)
                {
                    Console.WriteLine(car.ToString());
                }

                Console.WriteLine("\n" + resultGetCarsByColorId.Message);
            }
            else
            {
                Console.WriteLine("\n" + resultGetCarsByColorId.Message);
            }

            Console.WriteLine("-----GET BY ID-----");
            var resultGetCarById = carManager.GetCarById(4);
            if (resultGetCarById.Success)
            {
                Console.WriteLine(resultGetCarById.Data.ToString());
                Console.WriteLine("\n" + resultGetCarById.Message);
            }
            else
            {
                Console.WriteLine("\n" + resultGetCarById.Message);
            }

            Console.WriteLine("-----ADD CAR-----");
            var resultAdd = carManager.Add(new Car
                {BrandId = 3, ColorId = 5, ModelYear = new DateTime(1777, 01, 01), DailyPrice = 300.0M, Description = "Boş"});
            if (resultAdd.Success)
            {
                Console.WriteLine(resultAdd.Message);
            }
            else
            {
                Console.WriteLine(resultAdd.Message);
            }

            var resultAdd2 = carManager.Add(new Car
                {BrandId = 3, ColorId = 5, ModelYear = new DateTime(2000, 01, 01), DailyPrice = 0M, Description = "Boş"});
            if (resultAdd2.Success)
            {
                Console.WriteLine(resultAdd2.Message);
            }
            else
            {
                Console.WriteLine(resultAdd2.Message);
            }
        }

        private static void Test3()
        {
            var carManager = new CarManager(new EfCarDal());
            Console.WriteLine("-----GET ALL CARS-----");
            foreach (var car in carManager.GetAllCars().Data)
            {
                Console.WriteLine(car.ToString());
            }

            Console.WriteLine("-----GET CAR DETAILS-----");
            foreach (var carDetail in carManager.GetCarDetails().Data)
            {
                Console.WriteLine(carDetail.ToString());
            }
        }

        private static void Test2()
        {
            var carManager = new CarManager(new EfCarDal());
            Console.WriteLine("-----GET ALL-----");
            foreach (var car in carManager.GetAllCars().Data)
            {
                Console.WriteLine(car.ToString());
            }

            Console.WriteLine("-----GET ALL BY BRAND-----");
            foreach (var car in carManager.GetCarsByBrandId(1).Data)
            {
                Console.WriteLine(car.ToString());
            }

            Console.WriteLine("-----GET ALL BY COLOR-----");
            foreach (var car in carManager.GetCarsByColorId(3).Data)
            {
                Console.WriteLine(car.ToString());
            }

            Console.WriteLine("-----GET BY ID-----");
            Console.WriteLine(carManager.GetCarById(4).ToString());
            
            Console.WriteLine("-----ADD CAR-----");
            carManager.Add(new Car{BrandId = 3, ColorId = 5, ModelYear = new DateTime(1777, 01, 01), DailyPrice = 300.0M, Description = "Boş"});
            carManager.Add(new Car{BrandId = 3, ColorId = 5, ModelYear = new DateTime(2000, 01, 01), DailyPrice = 0M, Description = "Boş"});
            // it works don't add again //carManager.Add(new Car{BrandId = 3, ColorId = 5, ModelYear = new DateTime(2005, 01, 01), DailyPrice = 300.0M, Description = "Boş"});
            foreach (var car in carManager.GetAllCars().Data)
            {
                Console.WriteLine(car.ToString());
            }  
        }

        private static void Test1()
        {
            Console.WriteLine("-----BEFORE UPDATE-----");
            var carManager = new CarManager(new InMemoryCarDal());
            foreach (var car in carManager.GetAllCars().Data)
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
            foreach (var car in carManager.GetAllCars().Data)
            {
                Console.WriteLine(car.ToString());
            }
        }
    }
}