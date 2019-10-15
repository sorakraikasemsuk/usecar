using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UseCar.Helper
{
    public static class Resource
    {

    }
    public static class Permission
    {
        public const string view = "view";
        public const string add = "add";
        public const string edit = "edit";
        public const string delete = "delete";
    }
    public static class CarStatus
    {
        public const int ReceiveCar = 1;
        public const int MaintenanceCar = 2;
        public const int ReadySell = 3;
    }
    public static class ReceiveCarStatus
    {
        public const int Waiting = 1;
        public const int Success = 2;
        public const int Cancel = 3;
    }
    public static class MenuId
    {
        public const int ReceiveCar = 11;
        public const int CheckupCar = 4;
    }
    public static class MenuName
    {
        public const string ReceiveCar = "ReceiveCar";
        public const string CheckupCar = "CheckupCar";
    }
}
