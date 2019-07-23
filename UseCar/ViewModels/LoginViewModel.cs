using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Car_Somchai.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="กรุณากรอกข้อมูล")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
