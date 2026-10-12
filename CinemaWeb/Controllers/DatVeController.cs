using CinemaWeb.Dto;
using CinemaWeb.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWeb.Controllers
{
    [Route("api/dat-ve")]
    [ApiController]
    public class DatVeController : ControllerBase
    {
        private readonly IDatVeService datVeService;

        public DatVeController(IDatVeService datVeService)
        {
            this.datVeService = datVeService;
        }

        [HttpPost("dat-ve")]
        public async Task<IActionResult> DatVe([FromBody] DatVeRequestDto req)
        {
            var result = await datVeService.DatVeAsync(req);
            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }
            return Ok(result);
        }

        [HttpGet("hoa-don/{hoaDonId}")]
        public async Task<ActionResult<HoaDonDto>> GetHoaDon(int hoaDonId)
        {
            var result = await datVeService.GetHoaDonByIdAsync(hoaDonId);
            if (result == null)
            {
                return NotFound(new { message = $"Không tìm thấy hóa đơn với mã {hoaDonId}" });
            }
            return Ok(result);
        }

        [HttpGet("lich-su/{userId}")]
        public async Task<ActionResult<List<HoaDonDto>>> GetLichSu(int userId)
        {
            var result = await datVeService.GetLichSuDatVeByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpGet("danh-sach-bap-nuoc")]
        public async Task<ActionResult<List<BapNuocDto>>> GetDanhSachBapNuoc()
        {
            var result = await datVeService.GetDanhSachBapNuocAsync();
            return Ok(result);
        }
    }
}
