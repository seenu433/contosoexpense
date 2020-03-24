using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Contoso.Expenses.Common.Models;
using Contoso.Expenses.Web.Models;
using Contoso.Expenses.APIClient;
using Contoso.Expenses.APIClient.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace Contoso.Expenses.Web.Pages.Expenses
{
    public class CreateModel : PageModel
    {
        private readonly ContosoExpensesWebContext _context;
        private readonly IContosoExpensesAPI _apiClient;
        private readonly QueueInfo _queueInfo;

        public CreateModel(ContosoExpensesWebContext context, IContosoExpensesAPI apiClient, QueueInfo queueInfo)
        {
            _context = context;
            _apiClient = apiClient;
            _queueInfo = queueInfo;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Expense Expense { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Look up cost center
            CostCenterInfo costCenterInfo = await _apiClient.GetCostCenterInfoAsync(Expense.SubmitterEmail);
            Expense.CostCenter = costCenterInfo.CostCenter;
            Expense.ApproverEmail = costCenterInfo.ApproverEmail;

            // Write to DB, but don't wait right now
            _context.Expense.Add(Expense);
            Task t = _context.SaveChangesAsync();

            // Serialize the expense and write it to the Azure Storage Queue
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_queueInfo.ConnectionString);
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference(_queueInfo.QueueName);
            await queue.CreateIfNotExistsAsync();
            CloudQueueMessage queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(Expense));
            await queue.AddMessageAsync(queueMessage);

            // Ensure the DB write is complete
            t.Wait();

            return RedirectToPage("./Index");
        }
    }
}