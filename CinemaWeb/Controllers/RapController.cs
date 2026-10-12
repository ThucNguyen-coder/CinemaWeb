using CinemaWeb.Dto;
using CinemaWeb.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWeb.Controllers
{
    [Route("api/rap")]
    [ApiController]
    public class RapController : ControllerBase
    {
        private readonly IRapService rapService;

        public RapController(IRapService rapService)
        {
            this.rapService = rapService;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<RapDto>>> GetAll()
        {
            var danhSachRap = await rapService.GetAllAsync();
            return Ok(danhSachRap);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<RapDto>> GetById(int id)
        {
            var rap = await rapService.GetByIdAsync(id);
            if (rap == null)
            {
                return NotFound(new { message = $"Không tìm thấy rạp với mã {id}" });
            }
            return Ok(rap);
        }

        [HttpGet("get-phong-chieu/{rapId}")]
        public async Task<ActionResult<List<PhongChieuDto>>> GetPhongChieu(int rapId)
        {
            var danhSachPhong = await rapService.GetPhongChieuByRapIdAsync(rapId);
            return Ok(danhSachPhong);
        }
    }
}
