using System;
using System.Collections.Generic;

namespace UseCar.Models
{
    public partial class subface
    {
        public int subfaceId { get; set; }
        public int brandId { get; set; }
        public int generationId { get; set; }
        public int faceId { get; set; }
        public string subfaceName { get; set; }
        public DateTime createDate { get; set; }
        public int createUser { get; set; }
        public DateTime? updateDate { get; set; }
        public int? updateUser { get; set; }
        public string isEnable { get; set; }
    }
}
