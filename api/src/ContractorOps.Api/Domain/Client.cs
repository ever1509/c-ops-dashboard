namespace ContractorOps.Api.Domain;

public class Client
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Website { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}