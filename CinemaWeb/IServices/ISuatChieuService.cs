using CinemaWeb.Dto;

namespace CinemaWeb.IServices
{
    public interface ISuatChieuService
    {
        Task<List<SuatChieuDto>> GetAllAsync();
        Task<List<SuatChieuDto>> GetByPhimAsync(int phimId, DateTime? ngayChieu = null);
        Task<List<SuatChieuDto>> GetByRapAsync(int rapId, DateTime? ngayChieu = null);
        Task<SuatChieuDto?> GetByIdAsync(int suatChieuId);
        Task<SoDoGheSuatChieuDto?> GetSoDoGheAsync(int suatChieuId);
        Task<bool> AddAsync(SuatChieuAddDto req);
        Task<bool> DeleteAsync(int suatChieuId);
    }
}
