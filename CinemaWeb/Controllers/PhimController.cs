using CinemaWeb.Dto;
using CinemaWeb.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWeb.Controllers
{
    [Route("api/phim")]
    [ApiController]
    public class PhimController : ControllerBase
    {
        private readonly IPhimService phimService;

        public PhimController(IPhimService phimService)
        {
            this.phimService = phimService;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<PhimDto>>> GetAll([FromQuery] string? trangThai)
        {
            var danhSachPhim = await phimService.GetAllAsync(trangThai);
            return Ok(danhSachPhim);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<PhimDto>> GetById(int id)
        {
            var phim = await phimService.GetByIdAsync(id);
            if (phim == null)
            {
                return NotFound(new { message = $"Không tìm thấy phim với mã {id}" });
            }
            return Ok(phim);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] PhimAddDto req)
        {
            var result = await phimService.AddAsync(req);
            if (!result)
            {
                return BadRequest(new { message = "Thêm phim thất bại! Phim có thể đã tồn tại." });
            }
            return Ok(new { message = "Thêm phim mới thành công!" });
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] PhimEditDto req)
        {
            var result = await phimService.EditAsync(req);
            if (!result)
            {
                return NotFound(new { message = $"Không tìm thấy phim với mã {req.PhimId} để cập nhật." });
            }
            return Ok(new { message = "Cập nhật thông tin phim thành công!" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await phimService.DeleteAsync(id);
            if (!result)
            {
                return NotFound(new { message = $"Không tìm thấy phim với mã {id} để xóa." });
            }
            return Ok(new { message = "Xóa/Ngưng chiếu phim thành công!" });
        }
    }
}
