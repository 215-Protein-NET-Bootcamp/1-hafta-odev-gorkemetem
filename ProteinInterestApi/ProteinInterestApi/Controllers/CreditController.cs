using Microsoft.AspNetCore.Mvc;
using ProteinBankApi.Controllers.Entities;
using ProteinBankApi.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace ProteinInterestApi.Controllers
{

    /*
     * Controller Class where credit calculations and planning are made.
     */
    [Route("protein/api/[controller]")]
    [ApiController]
    public class CreditController : ControllerBase
    {

        double interestRate = Convert.ToDouble(ConfigurationManager.AppSettings["interestRate"]);


        /*
         * Post Method returning the total amount to be paid and interest.
         */
        [HttpPost]
        [Route("Post/Amount")]
        public CommonResponse<Amount> PostAmount([FromBody] RequestItem requestItem )
        {
            
            Amount amount = new Amount();
            amount.TotalAmount = Math.Round(requestItem.DesiredAmount * interestRate * (Math.Pow(1 + interestRate, requestItem.Expiry)) / (Math.Pow(1 + interestRate, requestItem.Expiry) - 1) * requestItem.Expiry, 2);
            amount.InterestAmount = Math.Round(amount.TotalAmount - requestItem.DesiredAmount, 2);
            return new CommonResponse<Amount>(amount);

        }

        /*
         * Post Method returning payment schedule.
         */
        [HttpPost]
        [Route("Post/PaymentPlan")]
        public CommonResponse<List<PaymentPlan>> PostPaymentPlan([FromBody] RequestItem requestItem)
        {

            List<PaymentPlan> list = new List<PaymentPlan>();
            double remainingDebt = requestItem.DesiredAmount;

            for (int i = 0; i < requestItem.Expiry; i++)
            {
                PaymentPlan plan = new PaymentPlan();
                plan.Payment = Math.Round(requestItem.DesiredAmount * interestRate * (Math.Pow(1 + interestRate, requestItem.Expiry)) / (Math.Pow(1 + interestRate, requestItem.Expiry) - 1), 2);
                plan.PaymentNo = i + 1;
                plan.InterestPaid = Math.Round(remainingDebt * interestRate,2);
                plan.MoneyPaid = Math.Round(plan.Payment - plan.InterestPaid, 2);
                remainingDebt -= plan.MoneyPaid;
                plan.RemainingDebt = Math.Round(remainingDebt, 2);
                list.Add(plan);
            }
                
            return new CommonResponse<List<PaymentPlan>>(list);

        }
    }
}
