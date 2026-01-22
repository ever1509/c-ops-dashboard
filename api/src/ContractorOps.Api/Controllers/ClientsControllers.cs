using ContractorOps.Api.Data;
using ContractorOps.Api.Domain;
using ContractorOps.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContractorOps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClientsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Client>>> GetClients()
    {
        return await _context.Clients.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetClient(int id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client == null)
        {
            return NotFound();
        }
        return client;
    }

    [HttpPost]
    public async Task<ActionResult<Client>> CreateClient(ClientRequestDto client)
    {
        var newClient = new Client
        {
            Name = client.Name,
            Website = client.Website,
            Notes = client.Notes,
            CreatedAt = DateTime.UtcNow
        };
        _context.Clients.Add(newClient);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetClient), new { id = newClient.Id }, newClient);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClient(int id, ClientRequestDto client)
    {
        if (id <=0)
        {
            return BadRequest();
        }

        var existingClient = await _context.Clients.FindAsync(id);
        if (existingClient == null)
        {
            return NotFound();
        }
        existingClient.Name = client.Name;
        existingClient.Website = client.Website;
        existingClient.Notes = client.Notes;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client == null)
        {
            return NotFound();
        }

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}