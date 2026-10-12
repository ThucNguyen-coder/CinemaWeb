using CinemaWeb.Dto;
using CinemaWeb.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWeb.Controllers
{
    [Route("api/suat-chieu")]
    [ApiController]
    public class SuatChieuController : ControllerBase
    {
        private readonly ISuatChieuService suatChieuService;

        public SuatChieuController(ISuatChieuService suatChieuService)
        {
            this.suatChieuService = suatChieuService;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<SuatChieuDto>>> GetAll()
        {
            var result = await suatChieuService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("get-by-phim/{phimId}")]
        public async Task<ActionResult<List<SuatChieuDto>>> GetByPhim(int phimId, [FromQuery] DateTime? ngayChieu)
        {
            var result = await suatChieuService.GetByPhimAsync(phimId, ngayChieu);
            return Ok(result);
        }

        [HttpGet("get-by-rap/{rapId}")]
        public async Task<ActionResult<List<SuatChieuDto>>> GetByRap(int rapId, [FromQuery] DateTime? ngayChieu)
        {
            var result = await suatChieuService.GetByRapAsync(rapId, ngayChieu);
            return Ok(result);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<SuatChieuDto>> GetById(int id)
        {
            var result = await suatChieuService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound(new { message = $"Không tìm thấy suất chiếu với mã {id}" });
            }
            return Ok(result);
        }

        [HttpGet("so-do-ghe/{suatChieuId}")]
        public async Task<ActionResult<SoDoGheSuatChieuDto>> GetSoDoGhe(int suatChieuId)
        {
            var result = await suatChieuService.GetSoDoGheAsync(suatChieuId);
            if (result == null)
            {
                return NotFound(new { message = $"Không tìm thấy suất chiếu với mã {suatChieuId}" });
            }
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] SuatChieuAddDto req)
        {
            var result = await suatChieuService.AddAsync(req);
            if (!result)
            {
                return BadRequest(new { message = "Thêm suất chiếu thất bại! Thời gian không hợp lệ hoặc bị trùng lịch với suất chiếu khác tại cùng phòng." });
            }
            return Ok(new { message = "Thêm suất chiếu thành công!" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await suatChieuService.DeleteAsync(id);
            if (!result)
            {
                return BadRequest(new { message = "Xóa suất chiếu thất bại! Suất chiếu không tồn tại hoặc đã có khách đặt vé." });
            }
            return Ok(new { message = "Xóa suất chiếu thành công!" });
        }
    }
}
