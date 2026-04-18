using AssetFlow.API.Data;
using AssetFlow.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetFlow.API.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly ApplicationDbContext _context;

        public AssetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Asset>> GetAllAsync()
        {
            return await _context.Assets.Include(a => a.Category).ToListAsync();
        }

        public async Task<Asset?> GetByIdAsync(int id)
        {
            return await _context.Assets.Include(a => a.Category).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Asset asset)
        {
            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Asset asset)
        {
            _context.Assets.Update(asset);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var asset = await GetByIdAsync(id);
            if (asset != null)
            {
                _context.Assets.Remove(asset);
                await _context.SaveChangesAsync();
            }
        }
    }
}
