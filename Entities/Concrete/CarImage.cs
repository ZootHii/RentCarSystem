using System;
using System.IO;
using Core.Entities;

namespace Entities.Concrete
{
    public class CarImage : IEntity
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string ImageName { get; set; }
        public DateTime? UploadDate { get; set; }
        public byte[] ImagePath { get; set; }
    }
}