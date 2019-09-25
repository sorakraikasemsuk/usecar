using System;
using System.Collections.Generic;

namespace UseCar.Models
{
    public partial class car_image
    {
        public int imageId { get; set; }
        public int carId { get; set; }
        public string name { get; set; }
        public string contenType { get; set; }
        public string path { get; set; }
        public DateTime createDate { get; set; }
        public int createUser { get; set; }
        public DateTime? updateDate { get; set; }
        public int? updateUser { get; set; }
        public bool isEnable { get; set; }
    }
}
