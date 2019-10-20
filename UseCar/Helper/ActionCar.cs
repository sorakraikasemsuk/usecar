using System;
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
        public void UpdateCarStatus(int carId,int carStatusId,int carProcessId)
        {
            using(var Transaction = context.Database.BeginTransaction())
            {
                try
                {
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
