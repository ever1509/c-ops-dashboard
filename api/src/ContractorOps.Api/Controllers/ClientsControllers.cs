using System.Globalization;
using ContractorOps.Api.Data;
using ContractorOps.Api.Domain;
using ContractorOps.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContractorOps.Api.Controllers;

[Route("api/[controller]")]
public class ClientsControllers : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public ClientsControllers(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetClients()
    {
        var clients = await _dbContext.Clients
            .Select(c => new ClientResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                Website = c.Website ?? string.Empty,
                Notes = c.Notes ?? string.Empty,
                CreatedAt = c.CreatedAt.ToString(CultureInfo.InvariantCulture)
            })
            .ToListAsync();
        return Ok(clients);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetClientById(int id)
    {
        var client = await _dbContext.Clients.FindAsync(id);
        if (client == null)
        {
            return NotFound();
        }

        return Ok(client);
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient([FromBody] ClientRequestDto clientDto)
    {
        if (string.IsNullOrWhiteSpace(clientDto.Name))
            return BadRequest("Client name is required.");
        
        var client = new Client
        {
            Name = clientDto.Name,
            Website = clientDto.Website,
            Notes = clientDto.Notes,
            CreatedAt = DateTime.UtcNow
        };
        _dbContext.Clients.Add(client);
         await _dbContext.SaveChangesAsync();
        return CreatedAtAction(nameof(GetClientById), new {id = client.Id}, client);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClient(int id, [FromBody] ClientRequestDto updatedClient)
    {
        var client = _dbContext.Clients.Find(id);
        if (client == null)
        {
            return NotFound();
        }

        client.Name = updatedClient.Name;
        client.Website = updatedClient.Website;
        client.Notes = updatedClient.Notes;
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var client = _dbContext.Clients.Find(id);
        if (client == null)
        {
            return NotFound();
        }

        _dbContext.Clients.Remove(client);
       await _dbContext.SaveChangesAsync();
        return NoContent();
    }
}