using System;
using Core.Entities;

namespace Entities.Concrete.DTOs
{
    public class RentalDetailDto : IDto
    {
        public int RentalId { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string BrandName { get; set; }
        public DateTime ModelYear { get; set; }
        public string ColorName { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string CompanyName { get; set; }

        public override string ToString()
        {
            return "RentalID = " + RentalId +
                   " / CarId = " + CarId +
                   " / CustomerId = " + CustomerId +
                   " / RentDate = " + RentDate.Date +
                   " / ReturnDate = " + ReturnDate.Date +
                   " / BrandName = " + BrandName +
                   " / ModelYear = " + ModelYear.Year +
                   " / ColorName = " + ColorName +
                   " / DailyPrice = " + DailyPrice +
                   " / Description = " + Description +
                   " / FirstName = " + FirstName +
                   " / LastName = " + LastName +
                   " / EMail = " + EMail +
                   " / CompanyName = " + CompanyName;
        }
    }
}