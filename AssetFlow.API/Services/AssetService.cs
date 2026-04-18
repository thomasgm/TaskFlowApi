using AssetFlow.API.Models;
using AssetFlow.API.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetFlow.API.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;

        public AssetService(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public async Task<IEnumerable<Asset>> GetAllAssetsAsync()
        {
            return await _assetRepository.GetAllAsync();
        }

        public async Task<Asset?> GetAssetByIdAsync(int id)
        {
            return await _assetRepository.GetByIdAsync(id);
        }

        public async Task CreateAssetAsync(Asset asset)
        {
            if (string.IsNullOrEmpty(asset.Name))
                throw new ArgumentException("O nome do ativo é obrigatório.", nameof(asset));

            if (asset.Value < 0)
                throw new ArgumentException("O valor do ativo não pode ser negativo.", nameof(asset));

            await _assetRepository.AddAsync(asset);
        }
        public async Task UpdateAssetAsync(Asset asset)
        {
            if (asset == null)
                throw new ArgumentNullException(nameof(asset));

            await _assetRepository.UpdateAsync(asset);
        }

        public async Task DeleteAssetAsync(int id)
        {
            await _assetRepository.DeleteAsync(id);
        }
    }
}