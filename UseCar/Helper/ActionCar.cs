﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UseCar.Models;

namespace UseCar.Helper
{
    public class ActionCar
    {
        readonly UseCarDBContext context;
        public ActionCar(UseCarDBContext context)
        {
            this.context = context;
        } 
        public void UpdateCarStatus(int carId,int menuId,int statusId)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                try
                {
                    int carStatusId = 0, carProcessId = 0;
                    switch (menuId)
                    {
                        case MenuId.CheckupCar:
                            carStatusId = CarStatus.WaitingMaintenance;
                            carProcessId = 0;
                            break;
                        case MenuId.MaintenanceCar:
                            if (statusId == MaintenanceCarStatus.Send)
                            {
                                carStatusId = CarStatus.Maintenance;
                                carProcessId = statusId;
                            }else if (statusId == MaintenanceCarStatus.Success)
                            {
                                carStatusId = CarStatus.WaitingCleaning;
                                carProcessId = 0;
                            }else if (statusId == MaintenanceCarStatus.Cancel)
                            {
                                carStatusId = CarStatus.WaitingMaintenance;
                                carProcessId = 0;
                            }
                            break;
                    }
                    var car = (from a in context.car
                               where a.isEnable
                               && a.carId == carId
                               select a).FirstOrDefault();
                    car.carStatusId = carStatusId;
                    car.carProcessId = carProcessId;
                    context.SaveChanges();
                    Transaction.Commit();
                }catch(Exception ex)
                {
                    Transaction.Rollback();
                }
            }
        }
    }
}
