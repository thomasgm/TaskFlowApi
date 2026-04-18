using AssetFlow.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetFlow.API.Repositories
{
    public interface IAssetRepository
    {
        Task<IEnumerable<Asset>> GetAllAsync();
        Task<Asset?> GetByIdAsync(int id);
        Task AddAsync(Asset asset);
        Task UpdateAsync(Asset asset);
        Task DeleteAsync(int id);
    }
}