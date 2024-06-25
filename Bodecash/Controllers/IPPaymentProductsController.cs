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
public class IPPaymentProductProductsController : ControllerBase
{
    private readonly IIPPaymentProductService _iPPaymentProductService;
    private readonly IMapper _mapper;

    public IPPaymentProductProductsController(IIPPaymentProductService iPPaymentProductService, IMapper mapper)
    {
        _iPPaymentProductService = iPPaymentProductService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<IPPaymentProductResource>> GetAllAsync()
    {
        var iPPaymentProducts = await _iPPaymentProductService.ListAsync();
        var resources = _mapper.Map<IEnumerable<IPPaymentProduct>, IEnumerable<IPPaymentProductResource>>(iPPaymentProducts);
        return resources;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var iPPaymentProduct = await _iPPaymentProductService.GetByIdAsync(id);
        if (iPPaymentProduct == null)
            return BadRequest(new IPPaymentProductResponse("Not found."));
        
        var resource = _mapper.Map<IPPaymentProduct, IPPaymentProductResource>(iPPaymentProduct);
        return Ok(resource);
    }
    
    [HttpGet("ippayment/{iPPaymentId:int}")]
    public async Task<IActionResult> GetByIPPaymentIdAsync(int iPPaymentId)
    {
        var iPPaymentProducts = await _iPPaymentProductService.GetByIPPaymentIdAsync(iPPaymentId);
        if (iPPaymentProducts == null)
            return BadRequest(new IPPaymentProductResponse("Not found."));
        
        var resources = _mapper.Map<IEnumerable<IPPaymentProduct>, IEnumerable<IPPaymentProductResource>>(iPPaymentProducts);
        return Ok(resources);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveIPPaymentProductResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var iPPayment = _mapper.Map<SaveIPPaymentProductResource, IPPaymentProduct>(resource);
        var result = await _iPPaymentProductService.SaveAsync(iPPayment);

        if (!result.Success)
            return BadRequest(result.Message);

        var iPPaymentResource = _mapper.Map<IPPaymentProduct, IPPaymentProductResource>(result.Resource);

        return Ok(iPPaymentResource);
    }
}