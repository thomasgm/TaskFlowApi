using AssetFlow.API.Models;
using AssetFlow.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetService _assetService;

        public AssetsController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAll()
        {
            var assets = await _assetService.GetAllAssetsAsync();
            return Ok(assets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> GetById(int id)
        {
            var asset = await _assetService.GetAssetByIdAsync(id);
            if (asset == null)
                return NotFound();

            return Ok(asset);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Asset asset)
        {
            try
            {
                await _assetService.CreateAssetAsync(asset);
                return CreatedAtAction(nameof(GetById), new { id = asset.Id }, asset);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Asset asset)
        {
            if (id != asset.Id)
                return BadRequest("ID do ativo não corresponde ao ID da URL.");

            try
            {
                await _assetService.UpdateAssetAsync(asset);
                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _assetService.DeleteAssetAsync(id);
            return NoContent();
        }
    }
}