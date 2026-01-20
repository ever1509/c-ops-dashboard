using ContractorOps.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace ContractorOps.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Client> Clients => Set<Client>();
    
}