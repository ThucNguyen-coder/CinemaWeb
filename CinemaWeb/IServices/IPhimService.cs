using CinemaWeb.Dto;

namespace CinemaWeb.IServices
{
    public interface IPhimService
    {
        Task<List<PhimDto>> GetAllAsync(string? trangThai = null);
        Task<PhimDto?> GetByIdAsync(int phimId);
        Task<bool> AddAsync(PhimAddDto req);
        Task<bool> EditAsync(PhimEditDto req);
        Task<bool> DeleteAsync(int phimId);
    }
}
