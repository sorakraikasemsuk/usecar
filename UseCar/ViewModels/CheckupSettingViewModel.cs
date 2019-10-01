using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UseCar.ViewModels
{
    public class CheckupSettingViewModel
    {
        public int checkupId { get; set; }
        public string checkupName { get; set; }
        public int carInCheck { get; set; }
    }
    public class CheckupFilter
    {
        public string checkupName { get; set; }
    }
}
