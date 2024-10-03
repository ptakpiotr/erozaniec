using ERozaniec.Models;

namespace ERozaniec.Data
{
    public interface IDatabaseService
    {
        Task AddManyPartsAsync(IList<PartModel> parts);
        Task AddPartAsync(PartModel part);
        Task DeletePartAsync(int id);
        Task EditPartAsync(PartModel part);
        Task<IList<PartModel>?> GetPartsAsync(RosaryPart part);
    }
}