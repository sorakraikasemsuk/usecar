using System;
using System.Collections.Generic;

namespace UseCar.Models
{
    public partial class m_menu
    {
        public int menuId { get; set; }
        public string menuName { get; set; }
        public string menuControllerName { get; set; }
        public string icon { get; set; }
        public int parentId { get; set; }
        public int ord { get; set; }
        public bool isEnable { get; set; }
    }
}
