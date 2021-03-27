using System;
using System.Collections.Generic;
using Core.Entities;

namespace Entities.Concrete.DTOs
{
    public class CarDetailDto : IDto
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public string BrandName { get; set; }
        public string ColorName { get; set; }
        public DateTime ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }
        //public List<string> ImageName { get; set; }
        //public List<byte[]> ImagePath { get; set; }
        
        /*public override string ToString()
        {
            return "CarID = " + CarId +
                   " / BrandName = " + BrandName +
                   " / ColorName = " + ColorName +
                   " / ModelYear = " + ModelYear.Year +
                   " / DailyPrice = " + DailyPrice +
                   " / Description = " + Description;
        }*/
    }
}