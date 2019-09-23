using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UseCar.Models;

namespace UseCar.Helper
{
    public class SharedData
    {
        readonly UseCarDBContext context;
        public SharedData(UseCarDBContext context)
        {
            this.context = context;
        }
        public List<SharedDataOptionViewModel> OptionData()
        {
            return (from a in context.option
                    where a.isEnable
                    select new SharedDataOptionViewModel
                    {
                        optionId = a.optionId,
                        optionName = a.optionName
                    }).ToList();
        }
    }
    public class SharedDataOptionViewModel
    {
        public int optionId { get; set; }
        public string optionName { get; set; }
    }
}
