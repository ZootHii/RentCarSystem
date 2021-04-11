using System;
using System.Numerics;
using Core.Entities;

namespace Entities.Concrete
{
    // TODO security operations HASHING will be added
    public class CreditCard : IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string NameOnCard { get; set; }
        public long CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CvvNumber { get; set; }
    }
}