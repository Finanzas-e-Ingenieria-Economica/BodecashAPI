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
public class IPPaymentsController : ControllerBase
{
    private readonly IIPPaymentService _iPPaymentService;
    private readonly IMapper _mapper;

    public IPPaymentsController(IIPPaymentService iPPaymentService, IMapper mapper)
    {
        _iPPaymentService = iPPaymentService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<IPPaymentResource>> GetAllAsync()
    {
        var iPPayments = await _iPPaymentService.ListAsync();
        var resources = _mapper.Map<IEnumerable<IPPayment>, IEnumerable<IPPaymentResource>>(iPPayments);
        return resources;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var iPPayment = await _iPPaymentService.GetByIdAsync(id);
        if (iPPayment == null)
            return BadRequest(new IPPaymentResponse("Not found."));
        
        var resource = _mapper.Map<IPPayment, IPPaymentResource>(iPPayment);
        return Ok(resource);
    }
    
    [HttpGet("installment-plan/{id:int}/position/{position:int}")]
    public async Task<IActionResult> GetByInstallmentPlanIdAndPositionAsync(int id, int position)
    {
        var iPPayments = await _iPPaymentService.GetByInstallmentPlanIdAndPositionAsync(id, position);
        if (iPPayments == null)
            return BadRequest(new IPPaymentResponse("Not found."));
        
        var resources = _mapper.Map<IPPayment, IPPaymentResource>(iPPayments);
        return Ok(resources);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveIPPaymentResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var iPPayment = _mapper.Map<SaveIPPaymentResource, IPPayment>(resource);
        var result = await _iPPaymentService.SaveAsync(iPPayment);

        if (!result.Success)
            return BadRequest(result.Message);

        var iPPaymentResource = _mapper.Map<IPPayment, IPPaymentResource>(result.Resource);

        return Ok(iPPaymentResource);
    }
    
    [HttpPut("pagar/{id:int}")]
    public async Task<IActionResult> Pagar(int id)
    {
        var result = await _iPPaymentService.Pagar(id);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var iPPaymentResource = _mapper.Map<IPPayment, IPPaymentResource>(result.Resource);
        
        return Ok(iPPaymentResource);
    }

    [HttpPut("aplicar-mora/{id:int}")]
    public async Task<IActionResult> AplicarTasaMoratoria(int id)
    {
        var result = await _iPPaymentService.AplicarTasaMoratoria(id);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var iPPaymentResource = _mapper.Map<IPPayment, IPPaymentResource>(result.Resource);
        
        return Ok(iPPaymentResource);
    }
}