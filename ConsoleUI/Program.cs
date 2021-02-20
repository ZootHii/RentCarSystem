using System;
using System.Collections.Generic;
using System.Linq;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace ConsoleUI
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            //Test1();
            //Test2();
            //Test3();
            //Test4();
            //Test5AddUser();
            //Test6GetUser();
            //Test7Rent();

        }

        private static void Test7Rent()
        {
            // rental works perfect for 2 hours limit and in use situation
            var rentalManager = new RentalManager(new EfRentalDal());

            /*var rental2 = new Rental
            {
                Id = 3,
                CarId = 3,
                CustomerId = 4,
                RentDate = rentalManager.GetRentalById(3).Data.RentDate - new TimeSpan(3,0,0),
                ReturnDate = DateTime.Now + new TimeSpan(0, 14, 0)
            };


            var result = rentalManager.Update(rental2);
            Console.WriteLine(result.Message);*/

            /*var rental2 = new Rental
            {
                CarId = 3,
                CustomerId = 1,
                RentDate = DateTime.Now,
                ReturnDate = DateTime.Now + new TimeSpan(5,0,0)
            };
            
            var result = rentalManager.Add(rental2);
            Console.WriteLine(result.Message);*/

            var rental2 = new Rental
            {
                CarId = 3,
                CustomerId = 1,
                RentDate = DateTime.Now - new TimeSpan(3,0,0),
                ReturnDate = DateTime.Now + new TimeSpan(0, 14, 0)
            };

            var result = rentalManager.Add(rental2);
            Console.WriteLine(result.Message);
        }

        private static void Test6GetUser()
        {
            var userManager = new UserManager(new EfUserDal());
            Console.WriteLine("-----GetUsersByFirstName 'ah'-----");
            var result = userManager.GetUsersByFirstName("ah".ToLower());
            if (result.Success)
            {
                foreach (var user in result.Data)
                {
                    Console.WriteLine(user.ToString());
                }

                Console.WriteLine("\n" + result.Message);
            }
            else
            {
                Console.WriteLine("\n" + result.Message);
            }

            Console.WriteLine("-----GetUsersByLastName 'yağ'-----");
            var result2 = userManager.GetUsersByLastName("yağ".ToLower());
            if (result2.Success)
            {
                foreach (var user in result2.Data)
                {
                    Console.WriteLine(user.ToString());
                }

                Console.WriteLine("\n" + result2.Message);
            }
            else
            {
                Console.WriteLine("\n" + result2.Message);
            }

            Console.WriteLine("-----GetUsersByEMail 'im'-----");
            var result3 = userManager.GetUsersByEMail("im".ToLower());
            if (result3.Success)
            {
                foreach (var user in result3.Data)
                {
                    Console.WriteLine(user.ToString());
                }

                Console.WriteLine("\n" + result3.Message);
            }
            else
            {
                Console.WriteLine("\n" + result3.Message);
            }
        }

        private static void Test5AddUser()
        {
            var userManager = new UserManager(new EfUserDal());
            var user1 = new User
            {
                FirstName = "Ahmet",
                LastName = "Yıldırım",
                EMail = "ahmet.zoothii@gmail.com",
                Password = "Ahmet123"
            };
            var user2 = new User
            {
                FirstName = "Fatih",
                LastName = "Yıldırım",
                EMail = "fatih@gmail.com",
                Password = "Fatih123"
            };
            var user3 = new User
            {
                FirstName = "Selim",
                LastName = "Yıldırım",
                EMail = "selim@gmail.com",
                Password = "Selim123"
            };
            var user4 = new User
            {
                FirstName = "Burak",
                LastName = "Yağmur",
                EMail = "burak@gmail.com",
                Password = "Burak123"
            };
            var user5 = new User
            {
                FirstName = "Yıldırım",
                LastName = "Ahmet",
                EMail = "yıldırım@gmail.com",
                Password = "Yıldırım123"
            };
            var user6 = new User
            {
                FirstName = "Ahmet",
                LastName = "Yıldırım",
                EMail = "ahmet.zoothii@",
                Password = "Ahmet123"
            };
            var user7 = new User
            {
                FirstName = "Ahmet",
                LastName = "Yıldırım",
                EMail = "ahmet.@gmailom",
                Password = "Ahmet123"
            };
            var user8 = new User
            {
                FirstName = "Ahmet",
                LastName = "Yıldırım",
                EMail = "ahmet.zoothii@gmail.com",
                Password = "hmet123"
            };
            var user9 = new User
            {
                FirstName = "Ahmet",
                LastName = "Yıldırım",
                EMail = "ahmet.zoothii@gmail.com",
                Password = "Ahmet"
            };
            var user10 = new User
            {
                FirstName = "Ahmet2",
                LastName = "Yıldırım",
                EMail = "ahmet.zoothii@gmail.com",
                Password = "Ahmet123"
            };
            var user11 = new User
            {
                FirstName = "Ahmet",
                LastName = "Yıldırım3",
                EMail = "ahmet.zoothii@gmail.com",
                Password = "Ahmet123"
            };

            var addUserList = new List<User>
            {
                user1,
                user2,
                user3,
                user4,
                user5,
                user6,
                user7,
                user8,
                user9,
                user10,
                user11
            };
            Console.WriteLine("-----ADD USER-----");
            foreach (var result in addUserList.Select(user => userManager.Add(user)))
            {
                if (result.Success)
                {
                    Console.WriteLine("\n" + result.Message);
                }
                else
                {
                    Console.WriteLine("\n" + result.Message);
                }
            }
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