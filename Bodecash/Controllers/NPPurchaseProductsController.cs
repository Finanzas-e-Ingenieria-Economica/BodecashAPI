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
public class NPPurchaseProductsController : ControllerBase
{
    private readonly INPPurchaseProductService _nPPurchaseProductService;
    private readonly IMapper _mapper;

    public NPPurchaseProductsController(INPPurchaseProductService nPPurchaseProductService, IMapper mapper)
    {
        _nPPurchaseProductService = nPPurchaseProductService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<NPPurchaseProductResource>> GetAllAsync()
    {
        var nPPurchaseProducts = await _nPPurchaseProductService.ListAsync();
        var resources = _mapper.Map<IEnumerable<NPPurchaseProduct>, IEnumerable<NPPurchaseProductResource>>(nPPurchaseProducts);
        return resources;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var nPPurchaseProduct = await _nPPurchaseProductService.GetByIdAsync(id);
        if (nPPurchaseProduct == null)
            return BadRequest(new NPPurchaseProductResponse("Not found."));
        
        var resource = _mapper.Map<NPPurchaseProduct, NPPurchaseProductResource>(nPPurchaseProduct);
        return Ok(resource);
    }
    
    [HttpGet("nPPurchase/{nPPurchaseId:int}")]
    public async Task<IActionResult> GetByNPPurchaseIdAsync(int nPPurchaseId)
    {
        var nPPurchaseProducts = await _nPPurchaseProductService.GetByNPPurchaseIdAsync(nPPurchaseId);
        if (nPPurchaseProducts == null)
            return BadRequest(new NPPurchaseProductResponse("Not found."));
        
        var resources = _mapper.Map<IEnumerable<NPPurchaseProduct>, IEnumerable<NPPurchaseProductResource>>(nPPurchaseProducts);
        return Ok(resources);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveNPPurchaseProductResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var nPPurchase = _mapper.Map<SaveNPPurchaseProductResource, NPPurchaseProduct>(resource);
        var result = await _nPPurchaseProductService.SaveAsync(nPPurchase);

        if (!result.Success)
            return BadRequest(result.Message);

        var nPPurchaseResource = _mapper.Map<NPPurchaseProduct, NPPurchaseProductResource>(result.Resource);

        return Ok(nPPurchaseResource);
    }
}