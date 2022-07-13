using Microsoft.AspNetCore.Mvc;
using ProteinBankApi.Controllers.Entities;
using System;
using System.Collections.Generic;

namespace ProteinInterestApi.Controllers
{
    [Route("protein/api/[controller]")]
    [ApiController]
    public partial class CreditController : ControllerBase
    {

        double interestRate = 0.01;

        [HttpGet]
        [Route("GetAmount")]
        public CommonResponse<Amount> GetAmount([FromQuery] int expiry, int desiredAmount)
        {
            
            Amount amount = new Amount();
            amount.TotalAmount = Math.Round(desiredAmount * interestRate * (Math.Pow(1 + interestRate, expiry)) / (Math.Pow(1 + interestRate, expiry) - 1) * expiry, 2);
            amount.InterestAmount = Math.Round(amount.TotalAmount - desiredAmount, 2);
            return new CommonResponse<Amount>(amount);

        }

        [HttpGet]
        [Route("GetPaymentPlan")]
        public CommonResponse<List<PaymentPlan>> GetPaymentPlan([FromQuery] int expiry, int desiredAmount)
        {

            List<PaymentPlan> list = new List<PaymentPlan>();
            double temp = desiredAmount;

            for (int i = 0; i < expiry; i++)
            {
                PaymentPlan plan = new PaymentPlan();
                plan.Payment = Math.Round(desiredAmount * interestRate * (Math.Pow(1 + interestRate, expiry)) / (Math.Pow(1 + interestRate, expiry) - 1), 2);
                plan.PaymentNo = i + 1;
                plan.InterestPaid = Math.Round(temp * interestRate,2);
                plan.MoneyPaid = Math.Round(plan.Payment - plan.InterestPaid, 2);
                temp -= plan.MoneyPaid;
                plan.RemainingDebt = Math.Round(temp, 2);
                list.Add(plan);
            }
                
            return new CommonResponse<List<PaymentPlan>>(list);

        }
    }
}
