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
public class NPPurchasesController : ControllerBase
{
    private readonly INPPurchaseService _nPPurchaseService;
    private readonly IMapper _mapper;

    public NPPurchasesController(INPPurchaseService nPPurchaseService, IMapper mapper)
    {
        _nPPurchaseService = nPPurchaseService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<NPPurchaseResource>> GetAllAsync()
    {
        var nPPurchases = await _nPPurchaseService.ListAsync();
        var resources = _mapper.Map<IEnumerable<NPPurchase>, IEnumerable<NPPurchaseResource>>(nPPurchases);
        return resources;
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var nPPurchase = await _nPPurchaseService.GetByIdAsync(id);
        if (nPPurchase == null)
            return BadRequest(new NPPurchaseResponse("Not found."));
        
        var resource = _mapper.Map<NPPurchase, NPPurchaseResource>(nPPurchase);
        return Ok(resource);
    }
    
    [HttpGet("normal-purchaseId/{normalPurchaseId:int}")]
    public async Task<IActionResult> GetAllByNormalPurchaseIdAsync(int normalPurchaseId)
    {
        var nPPurchases = await _nPPurchaseService.ListByNormalPurchaseIdAsync(normalPurchaseId);
        var resources = _mapper.Map<IEnumerable<NPPurchase>, IEnumerable<NPPurchaseResource>>(nPPurchases);
        return Ok(resources);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveNPPurchaseResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var nPPurchase = _mapper.Map<SaveNPPurchaseResource, NPPurchase>(resource);
        var result = await _nPPurchaseService.SaveAsync(nPPurchase);

        if (!result.Success)
            return BadRequest(result.Message);

        var nPPurchaseResource = _mapper.Map<NPPurchase, NPPurchaseResource>(result.Resource);

        return Ok(nPPurchaseResource);
    }
}