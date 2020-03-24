using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contoso.Expenses.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contoso.Expenses.API.Controllers
{
    [ApiController]
    public class CostCenterController : ControllerBase
    {
        /// <summary>
        /// Returns expense approval information for the submitter
        /// Fill out a case in the switch statement below in order to provide email address
        /// to send mail to.
        /// </summary>
        /// <param name="submitterEmail"></param>
        /// <returns></returns>
        [Route("api/[controller]/getcostcenterinfo/{submitterEmail}")]
        [HttpGet]
        public CostCenterInfo GetCostCenterInfo(string submitterEmail)
        {
            switch (submitterEmail)
            {
                // The email address entered by the submitter on the Expense
                // Create page is used to look up and return the cost center
                // and expense approver. Email will be sent to the address in 
                // the approver field.
                case "faisalm@microsoft.com": // this is the email address entered in the Expense Create page
                    return new CostCenterInfo()
                    {
                        SubmitterEmail = submitterEmail,
                        ApproverEmail = "faisalm@microsoft.com", // this is the address email will be sent to
                        CostCenter = "123E42"
                    };
                case "pgibson@microsoft.com":
                    return new CostCenterInfo()
                    {
                        SubmitterEmail = submitterEmail,
                        ApproverEmail = "pgibson@microsoft.com",
                        CostCenter = "456C14"
                    };
                case "srpadala@microsoft.com":
                    return new CostCenterInfo()
                    {
                        SubmitterEmail = submitterEmail,
                        ApproverEmail = "srpadala@microsoft.com",
                        CostCenter = "456C14"
                    };
                default:
                    return new CostCenterInfo()
                    {
                        SubmitterEmail = submitterEmail,
                        ApproverEmail = "unknown",
                        CostCenter = "unknown"
                    };
            }
        }
    }
}
