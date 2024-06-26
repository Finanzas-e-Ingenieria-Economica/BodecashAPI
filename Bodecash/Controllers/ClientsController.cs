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
public class ClientsController :  ControllerBase
{
    private readonly IClientService _clientService;
    private readonly IMapper _mapper;

    public ClientsController(IClientService clientService, IMapper mapper)
    {
        _clientService = clientService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ClientResource>> GetAllAsync()
    {
        var clients = await _clientService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Client>, IEnumerable<ClientResource>>(clients);
        return resources;
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var client = await _clientService.GetByIdAsync(id);
        if (client == null)
            return BadRequest(new ClientResponse("Not found."));
        
        var resource = _mapper.Map<Client, ClientResource>(client);
        return Ok(resource);
    }
    
    [HttpGet("personaldata/{id:int}")]
    public async Task<IActionResult> GetByPersonalDataIdAsync(int id)
    {
        var client = await _clientService.GetByPersonalDataIdAsync(id);
        if (client == null)
            return BadRequest(new ClientResponse("Not found."));
        
        var resource = _mapper.Map<Client, ClientResource>(client);
        return Ok(resource);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveClientResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var client = _mapper.Map<SaveClientResource, Client>(resource);
        var personalData = _mapper.Map<SaveClientResource, PersonalData>(resource);
        var result = await _clientService.SaveAsync(client, personalData);

        if (!result.Success)
            return BadRequest(result.Message);

        var clientResource = _mapper.Map<Client, ClientResource>(result.Resource);

        return Ok(clientResource);
    }
}