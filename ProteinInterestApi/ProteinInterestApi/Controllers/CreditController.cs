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
        [HttpGet]
        [Route("GetAmount")]
        public CommonResponse<Amount> GetAmount([FromQuery] int maturityAmount, int desiredAmount)
        {
            double faiz = 0.01;
            Amount amount = new Amount();
            amount.TotalAmount = Math.Round(desiredAmount * faiz * (Math.Pow(1 + faiz, maturityAmount)) / (Math.Pow(1 + faiz, maturityAmount) - 1) * maturityAmount, 2);
            amount.InterestAmount = Math.Round(amount.TotalAmount - desiredAmount, 2);
            return new CommonResponse<Amount>(amount);
        }

        [HttpGet]
        [Route("GetPaymentPlan")]
        public CommonResponse<List<PaymentPlan>> GetPlan([FromQuery] int maturityAmount, int desiredAmount)
        {
            double faiz = 0.01;
            List<PaymentPlan> list = new List<PaymentPlan>();
            double temp = desiredAmount;

            for (int i = 0; i < maturityAmount; i++)
            {
                PaymentPlan plan = new PaymentPlan();
                plan.Payment = Math.Round(desiredAmount * faiz * (Math.Pow(1 + faiz, maturityAmount)) / (Math.Pow(1 + faiz, maturityAmount) - 1), 2);
                plan.PaymentNo = i + 1;
                plan.InterestPaid = Math.Round(temp * faiz,2);
                plan.MoneyPaid = Math.Round(plan.Payment - plan.InterestPaid, 2);
                temp -= plan.MoneyPaid;
                plan.RemainingDebt = Math.Round(temp, 2);
                list.Add(plan);
            }
                
            return new CommonResponse<List<PaymentPlan>>(list);
        }

    }
}
