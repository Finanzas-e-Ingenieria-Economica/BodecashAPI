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
public class NormalPurchasesController : ControllerBase
{
    private readonly INormalPurchaseService _normalPurchaseService;
    private readonly IMapper _mapper;

    public NormalPurchasesController(INormalPurchaseService normalPurchaseService, IMapper mapper)
    {
        _normalPurchaseService = normalPurchaseService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<NormalPurchaseResource>> GetAllAsync()
    {
        var normalPurchases = await _normalPurchaseService.ListAsync();
        var resources = _mapper.Map<IEnumerable<NormalPurchase>, IEnumerable<NormalPurchaseResource>>(normalPurchases);
        return resources;
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var normalPurchase = await _normalPurchaseService.GetByIdAsync(id);
        if (normalPurchase == null)
            return BadRequest(new NormalPurchaseResponse("Not found."));
        
        var resource = _mapper.Map<NormalPurchase, NormalPurchaseResource>(normalPurchase);
        return Ok(resource);
    }
    
    [HttpGet("creditId/{creditId:int}")]
    public async Task<IActionResult> GetAllByCreditIdAsync(int creditId)
    {
        var normalPurchases = await _normalPurchaseService.ListByCreditIdAsync(creditId);
        var resources = _mapper.Map<IEnumerable<NormalPurchase>, IEnumerable<NormalPurchaseResource>>(normalPurchases);
        return Ok(resources);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveNormalPurchaseResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var normalPurchase = _mapper.Map<SaveNormalPurchaseResource, NormalPurchase>(resource);
        var credit = _mapper.Map<SaveNormalPurchaseResource, Credit>(resource);
        var result = await _normalPurchaseService.SaveAsync(normalPurchase, credit);

        if (!result.Success)
            return BadRequest(result.Message);

        var normalPurchaseResource = _mapper.Map<NormalPurchase, NormalPurchaseResource>(result.Resource);

        return Ok(normalPurchaseResource);
    }
}