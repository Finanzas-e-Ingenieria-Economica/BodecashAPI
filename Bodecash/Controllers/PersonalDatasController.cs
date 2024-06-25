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
public class PersonalDatasController : ControllerBase
{
    private readonly IPersonalDataService _personalDataService;
    private readonly IMapper _mapper;

    public PersonalDatasController(IPersonalDataService personalDataService, IMapper mapper)
    {
        _personalDataService = personalDataService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<PersonalDataResource>> GetAllAsync()
    {
        var personalDatas = await _personalDataService.ListAsync();
        var resources = _mapper.Map<IEnumerable<PersonalData>, IEnumerable<PersonalDataResource>>(personalDatas);
        return resources;
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var personalData = await _personalDataService.GetByIdAsync(id);
        if (personalData == null)
            return BadRequest(new PersonalDataResponse("Not found."));
        var resource = _mapper.Map<PersonalData, PersonalDataResource>(personalData);
        return Ok(resource);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SavePersonalDataResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var personalData = _mapper.Map<SavePersonalDataResource, PersonalData>(resource);
        var result = await _personalDataService.SaveAsync(personalData);

        if (!result.Success)
            return BadRequest(result.Message);

        var personalDataResource = _mapper.Map<PersonalData, PersonalDataResource>(result.Resource);

        return Ok(personalDataResource);
    }

    [HttpPost]
    public async Task<IActionResult> VerifyCredentialsAsync([FromBody] LoginResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var result = await _personalDataService.VerifyCredentialsAsync(resource.Email, resource.Password);
        if (result == null)
        {
            return NotFound("User not found.");
        }
        
        var personalDataResource = _mapper.Map<PersonalData, PersonalDataResource>(result);
        return Ok(personalDataResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _personalDataService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var personalDataResource = _mapper.Map<PersonalData, PersonalDataResource>(result.Resource);

        return Ok(personalDataResource);
    }
}