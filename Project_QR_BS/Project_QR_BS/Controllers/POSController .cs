using Microsoft.AspNetCore.Mvc;
using Project_QR_BS.Data;
using Project_QR_BS.Models;
using Project_QR_BS.Services;

namespace Project_QR_BS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class POSController : ControllerBase
    {
        private readonly POSService _posService;

        public POSController(POSService posService)
        {
            _posService = posService;
        }

        [HttpGet("connectToBS")]
        public ActionResult<ApiResponseStatus> ConnectToBS()
        {
            var result = _posService.ConnectToBS();
            return Ok(result);
        }

        [HttpPost("displayQRCode")]
        public ActionResult<ApiResponseStatus> DisplayQRCode([FromBody] DisplayQRCodeRequest request)
        {
            var result = _posService.DisplayQRCode(request);
            return Ok(result);
        }

        [HttpPost("displayPaymentStatus")]
        public ActionResult<ApiResponseStatus> DisplayPaymentStatus([FromBody] DisplayPaymentStatusRequest request)
        {
            var result = _posService.DisplayPaymentStatus(request.PaymentStatus);
            return Ok(result);
        }

        [HttpPost("reset")]
        public IActionResult ResetPOS()
        {
            var result = _posService.ResetPOS();
            if (result == ApiResponseStatus.Success)
            {
                return Ok(new { message = "POS reset successfully." });
            }
            else
            {
                return BadRequest(new { message = "Failed to reset POS." });
            }
        }

        [HttpGet("check-connection")]
        public IActionResult CheckConnection()
        {
            var status = _posService.CheckConnection();
            if (status == ApiResponseStatus.Success)
            {
                return Ok(new { ConnectionStatus = "Connected" });
            }
            else
            {
                return StatusCode(500, new { ConnectionStatus = "Not Connected" });
            }
        }
    }
}

