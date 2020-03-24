using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Expenses.API.Models
{
    public class CostCenterInfo
    {
        public string SubmitterEmail { get; set; }
        public string ApproverEmail { get; set; }
        public string CostCenter { get; set; }
    }
}
