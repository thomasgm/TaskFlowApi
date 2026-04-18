using AssetFlow.API.Models;
using AssetFlow.API.Repositories;
using AssetFlow.API.Services;
using Moq;
using Xunit;

namespace AssetFlow.Tests;

public class AssetServiceTests
{
    private readonly Mock<IAssetRepository> _mockRepo;
    private readonly AssetService _service;

    public AssetServiceTests()
    {
        _mockRepo = new Mock<IAssetRepository>();
        _service = new AssetService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllAssetsAsync_ReturnsAssetsFromRepository()
    {
        // Arrange
        var assets = new List<Asset> { new Asset { Id = 1, Name = "Asset1" } };
        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(assets);

        // Act
        var result = await _service.GetAllAssetsAsync();

        // Assert
        Assert.Equal(assets, result);
        _mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetAssetByIdAsync_ReturnsAssetFromRepository()
    {
        // Arrange
        var asset = new Asset { Id = 1, Name = "Asset1" };
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(asset);

        // Act
        var result = await _service.GetAssetByIdAsync(1);

        // Assert
        Assert.Equal(asset, result);
        _mockRepo.Verify(r => r.GetByIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task GetAssetByIdAsync_AssetNotFound_ReturnsNull()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Asset?)null);

        // Act
        var result = await _service.GetAssetByIdAsync(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAssetAsync_ValidAsset_CallsRepositoryAdd()
    {
        // Arrange
        var asset = new Asset
        {
            Name = "Valid Asset",
            AcquisitionDate = DateTime.Now,
            Value = 100.0m,
            CategoryId = 1
        };

        // Act
        await _service.CreateAssetAsync(asset);

        // Assert
        _mockRepo.Verify(r => r.AddAsync(asset), Times.Once);
    }

    [Fact]
    public async Task CreateAssetAsync_NameEmpty_ThrowsArgumentException()
    {
        // Arrange
        var asset = new Asset { Name = "", Value = 100.0m };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAssetAsync(asset));
        Assert.Contains("nome do ativo é obrigatório", exception.Message);
    }

    [Fact]
    public async Task CreateAssetAsync_ValueNegative_ThrowsArgumentException()
    {
        // Arrange
        var asset = new Asset { Name = "Asset", Value = -1 };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAssetAsync(asset));
        Assert.Contains("não pode ser negativo", exception.Message);
    }

    [Fact]
    public async Task UpdateAssetAsync_ValidAsset_CallsRepositoryUpdate()
    {
        // Arrange
        var asset = new Asset { Id = 1, Name = "Updated Asset" };

        // Act
        await _service.UpdateAssetAsync(asset);

        // Assert
        _mockRepo.Verify(r => r.UpdateAsync(asset), Times.Once);
    }

    [Fact]
    public async Task UpdateAssetAsync_NullAsset_ThrowsArgumentNullException()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _service.UpdateAssetAsync(null));
    }

    [Fact]
    public async Task DeleteAssetAsync_CallsRepositoryDelete()
    {
        // Act
        await _service.DeleteAssetAsync(1);

        // Assert
        _mockRepo.Verify(r => r.DeleteAsync(1), Times.Once);
    }
}