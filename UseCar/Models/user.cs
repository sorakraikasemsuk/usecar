using System;
using System.Collections.Generic;

namespace UseCar.Models
{
    public partial class user
    {
        public int userId { get; set; }
        public string code { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int departmentId { get; set; }
        public string tel { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
        public bool isActive { get; set; }
        public bool isAdmin { get; set; }
        public DateTime createDate { get; set; }
        public int createUser { get; set; }
        public DateTime? updateDate { get; set; }
        public int? updateUser { get; set; }
        public bool isEnable { get; set; }
    }
}
