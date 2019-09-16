using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UseCar.Helper;

namespace UseCar.ViewModels
{
    public class UserManagementViewModel
    {
        public int userId { get; set; }
        public string code { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int departmentId { get; set; }
        public string tel { get; set; }
        public string email { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [Remote(action: "CheckUsername", controller: "UserManagement", AdditionalFields = "userId,userName", HttpMethod = "GET", ErrorMessage = "ชื่อผู้ใช้งานนี้มีอยู่ในระบบแล้ว")]
        public string userName { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [MinLength(8,ErrorMessage ="ความยาวของรหัสผ่านอย่างน้อย 8 ตัวอักษร")]
        public string password { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [Compare("password",ErrorMessage ="รหัสผ่านไม่ตรงกัน")]
        public string confirmPassword { get; set; }
        public bool isActive { get; set; }
    }
    public class UserManagementSearchResultFilter
    {
        public string code { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int departmentId { get; set; }
        public int statusId { get; set; }
    }
    public class UserManagementSearchResult
    {
        public int userId { get; set; }
        public string code { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int departmentId { get; set; }
        public string departmentName { get; set; }
        public string tel { get; set; }
        public string email { get; set; }
        public bool isActive { get; set; }
        public bool isAdmin { get; set; }
    }
}
