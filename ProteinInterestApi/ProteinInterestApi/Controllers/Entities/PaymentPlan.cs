namespace ProteinBankApi.Controllers.Entities
{
    public class PaymentPlan
    {
        public int PaymentNo { get; set; }
        public double Payment { get; set; }
        public double InterestPaid { get; set; }
        public double MoneyPaid { get; set; }
        public double RemainingDebt { get; set; }
    }
}
