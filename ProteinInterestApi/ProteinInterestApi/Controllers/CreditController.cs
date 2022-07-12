using Microsoft.AspNetCore.Mvc;

namespace ProteinInterestApi.Controllers
{
    [Route("protein/api/[controller]")]
    [ApiController]
    public partial class CreditController : ControllerBase
    {
        [HttpGet]
        [Route("GetAmount")]
        public CommonResponse<Amount> Get([FromQuery] int maturityAmount, int desiredAmount)
        {
            Amount amount = new Amount();
            amount.InterestAmount = 5;
            amount.RefundAmount = 5;
            return new CommonResponse<Amount>(amount);
        }
    }
}
