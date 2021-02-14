using System;
using Entities.Abstract;

namespace Entities.Concrete
{
    public class Car : IEntity
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public DateTime ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return "ID = " + Id +
                   " BrandID = " + BrandId +
                   " ColorID = " + ColorId +
                   " ModelYear = " + ModelYear.Year +
                   " DailyPrice = " + DailyPrice +
                   " Description = " + Description;
        }
    }
}