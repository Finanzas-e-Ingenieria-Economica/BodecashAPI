using AutoMapper;
using BodecashAPI.Bodecash.Domain.Communication;
using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Bodecash.Domain.Services;
using BodecashAPI.Bodecash.Resources.Get;
using BodecashAPI.Bodecash.Resources.Post;
using BodecashAPI.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BodecashAPI.Bodecash.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class ShopkeepersController : ControllerBase
{
    private readonly IShopkeeperService _shopkeeperService;
    private readonly IMapper _mapper;

    public ShopkeepersController(IShopkeeperService shopkeeperService, IMapper mapper)
    {
        _shopkeeperService = shopkeeperService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<ShopkeeperResource>> GetAllAsync()
    {
        var shopkeepers = await _shopkeeperService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Shopkeeper>, IEnumerable<ShopkeeperResource>>(shopkeepers);
        return resources;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var shopkeeper = await _shopkeeperService.GetByIdAsync(id);
        if (shopkeeper == null)
            return BadRequest(new ShopkeeperResponse("Not found."));
        
        var resource = _mapper.Map<Shopkeeper, ShopkeeperResource>(shopkeeper);
        return Ok(resource);
    }
    
    [HttpGet("personaldata/{id:int}")]
    public async Task<IActionResult> GetByPersonalDataIdAsync(int id)
    {
        var shopkeeper = await _shopkeeperService.GetByPersonalDataIdAsync(id);
        if (shopkeeper == null)
            return BadRequest(new ShopkeeperResponse("Not found."));
        
        var resource = _mapper.Map<Shopkeeper, ShopkeeperResource>(shopkeeper);
        return Ok(resource);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveShopkeeperResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var shopkeeper = _mapper.Map<SaveShopkeeperResource, Shopkeeper>(resource);
        var personalData = _mapper.Map<SaveShopkeeperResource, PersonalData>(resource);
        var result = await _shopkeeperService.SaveAsync(shopkeeper, personalData);

        if (!result.Success)
            return BadRequest(result.Message);

        var shopkeeperResource = _mapper.Map<Shopkeeper, ShopkeeperResource>(result.Resource);

        return Ok(shopkeeperResource);
    }
}