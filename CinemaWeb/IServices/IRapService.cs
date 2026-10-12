using CinemaWeb.Dto;

namespace CinemaWeb.IServices
{
    public interface IRapService
    {
        Task<List<RapDto>> GetAllAsync();
        Task<RapDto?> GetByIdAsync(int rapId);
        Task<List<PhongChieuDto>> GetPhongChieuByRapIdAsync(int rapId);
    }
}
