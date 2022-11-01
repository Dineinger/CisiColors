using Cisi.CisiColors.Infrastructure.Models;

namespace Cisi.CisiColors.Infrastructure.DB
{
    public interface IColorDataAccess
    {
        Task<List<ColorModel>> GetAllColorsAsync();
        List<ColorModel> GetAllColors();
        Task<List<ColorCollectionAndStatus>> GetAllColorCollectionsAsync();
        List<ColorCollectionAndStatus> GetAllColorCollections();
    }
}