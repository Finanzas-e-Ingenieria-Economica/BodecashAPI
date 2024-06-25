using AutoMapper;
using BodecashAPI.Bodecash.Domain.Communication;
using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Bodecash.Domain.Services;
using BodecashAPI.Bodecash.Resources.Get;
using BodecashAPI.Bodecash.Resources.Post;
using BodecashAPI.Bodecash.Resources.Put;
using BodecashAPI.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BodecashAPI.Bodecash.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class CreditsController : ControllerBase
{
    private readonly ICreditService _creditService;
    private readonly IMapper _mapper;

    public CreditsController(ICreditService creditService, IMapper mapper)
    {
        _creditService = creditService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<CreditResource>> GetAllAsync()
    {
        var credits = await _creditService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Credit>, IEnumerable<CreditResource>>(credits);
        return resources;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var credit = await _creditService.GetByIdAsync(id);
        if (credit == null)
            return BadRequest(new CreditResponse("Not found."));
        
        var resource = _mapper.Map<Credit, CreditResource>(credit);
        return Ok(resource);
    }
    
    [HttpGet("clientId/{clientId:int}")]
    public async Task<IEnumerable<CreditResource>> GetAllByClientIdAsync(int clientId)
    {
        var credits = await _creditService.ListByClientIdAsync(clientId);
        var resources = _mapper.Map<IEnumerable<Credit>, IEnumerable<CreditResource>>(credits);
        return resources;
    }
    
    [HttpGet("shopkeeperId/{shopkeeperId:int}")]
    public async Task<IEnumerable<CreditResource>> GetAllByShopkeeperIdAsync(int shopkeeperId)
    {
        var credits = await _creditService.ListByShopkeeperIdAsync(shopkeeperId);
        var resources = _mapper.Map<IEnumerable<Credit>, IEnumerable<CreditResource>>(credits);
        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveCreditResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var credit = _mapper.Map<SaveCreditResource, Credit>(resource);
        var result = await _creditService.SaveAsync(credit);

        if (!result.Success)
            return BadRequest(result.Message);

        var creditResource = _mapper.Map<Credit, CreditResource>(result.Resource);

        return Ok(creditResource);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateCreditResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var credit = _mapper.Map<UpdateCreditResource, Credit>(resource);
        var result = await _creditService.UpdateAsync(id, credit);

        if (!result.Success)
            return BadRequest(result.Message);

        var creditResource = _mapper.Map<Credit, CreditResource>(result.Resource);

        return Ok(creditResource);
    }
}