using Microsoft.AspNetCore.Mvc;
using ReceiptProcessor.Models;
using ReceiptProcessor.Services;

namespace ReceiptProcessor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReceiptsController : ControllerBase
    {
        private readonly ReceiptService _receiptService;
        public ReceiptsController(ReceiptService receiptService)
        {
            _receiptService = receiptService;
        }

        [HttpPost("process")]
        public ActionResult<ReceiptResponse> ProcessReceipt([FromBody] Receipt receipt)
        {
            var id = _receiptService.ProcessReceipt(receipt);
            return Ok(new ReceiptResponse { Id = id });
        }

        [HttpGet("{id}/points")]
        public ActionResult<PointsResponse> GetPoints(string id)
        {
            var points = _receiptService.GetPoints(id);
            return Ok(new PointsResponse { Points = points });
        }
    }
}
