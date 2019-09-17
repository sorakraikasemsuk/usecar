using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UseCar.ViewModels
{
    public class ManageBranchViewModel
    {
        public int branchId { get; set; }
        public string branchName { get; set; }
        public string branchAddress { get; set; }
        public string tel { get; set; }
        public int carInBranch { get; set; }
    }
    public class ManageBranchFilter
    {
        public string branchName { get; set; }
    }
}
