using AssetFlow.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetFlow.API.Services
{
    public interface IAssetService
    {
        Task<IEnumerable<Asset>> GetAllAssetsAsync();
        Task<Asset?> GetAssetByIdAsync(int id);
        Task CreateAssetAsync(Asset asset);
        Task UpdateAssetAsync(Asset asset);
        Task DeleteAssetAsync(int id);
    }
}