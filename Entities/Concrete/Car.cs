﻿using Entities.Abstract;

namespace Entities.Concrete
{
    public class Car : IEntity
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public int ModelYear { get; set; }
        public double DailyPrice { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return "ID = " + Id +
                   " BrandID = " + BrandId +
                   " ColorID = " + ColorId +
                   " ModelYear = " + ModelYear +
                   " DailyPrice = " + DailyPrice +
                   " Description = " + Description;
        }
    }
}