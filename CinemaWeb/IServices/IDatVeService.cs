using CinemaWeb.Dto;

namespace CinemaWeb.IServices
{
    public interface IDatVeService
    {
        Task<KetQuaDatVeDto> DatVeAsync(DatVeRequestDto req);
        Task<HoaDonDto?> GetHoaDonByIdAsync(int hoaDonId);
        Task<List<HoaDonDto>> GetLichSuDatVeByUserIdAsync(int userId);
        Task<List<BapNuocDto>> GetDanhSachBapNuocAsync();
    }
}
