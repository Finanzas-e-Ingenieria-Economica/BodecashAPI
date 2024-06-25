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
public class InstallmentPlansController : ControllerBase
{
    private readonly IInstallmentPlanService _installmentPlanService;
    private readonly IMapper _mapper;

    public InstallmentPlansController(IInstallmentPlanService installmentPlanService, IMapper mapper)
    {
        _installmentPlanService = installmentPlanService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<InstallmentPlanResource>> GetAllAsync()
    {
        var installmentPlans = await _installmentPlanService.ListAsync();
        var resources = _mapper.Map<IEnumerable<InstallmentPlan>, IEnumerable<InstallmentPlanResource>>(installmentPlans);
        return resources;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var installmentPlan = await _installmentPlanService.GetByIdAsync(id);
        if (installmentPlan == null)
            return BadRequest(new InstallmentPlanResponse("Not found."));
        
        var resource = _mapper.Map<InstallmentPlan, InstallmentPlanResource>(installmentPlan);
        return Ok(resource);
    }
    
    [HttpGet("creditId/{creditId:int}")]
    public async Task<IActionResult> GetAllByCreditIdAsync(int creditId)
    {
        var installmentPlans = await _installmentPlanService.ListByCreditIdAsync(creditId);
        var resources = _mapper.Map<IEnumerable<InstallmentPlan>, IEnumerable<InstallmentPlanResource>>(installmentPlans);
        return Ok(resources);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveInstallmentPlanResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var installmentPlan = _mapper.Map<SaveInstallmentPlanResource, InstallmentPlan>(resource);
        var credit = _mapper.Map<SaveInstallmentPlanResource, Credit>(resource);
        var result = await _installmentPlanService.SaveAsync(installmentPlan, credit);

        if (!result.Success)
            return BadRequest(result.Message);

        var installmentPlanResource = _mapper.Map<InstallmentPlan, InstallmentPlanResource>(result.Resource);

        return Ok(installmentPlanResource);
    }
}