using System;
using System.Collections.Generic;

namespace UseCar.Models
{
    public partial class provinces
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name_th { get; set; }
        public string name_en { get; set; }
        public int geography_id { get; set; }
    }
}
