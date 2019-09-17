using System;
using System.Collections.Generic;

namespace UseCar.Models
{
    public partial class category
    {
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public DateTime createDate { get; set; }
        public int createUser { get; set; }
        public DateTime? updateDate { get; set; }
        public DateTime? updateUser { get; set; }
        public bool isEnable { get; set; }
    }
}
