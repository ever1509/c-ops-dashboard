namespace ContractorOps.Api.Dtos;

public class ClientResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;
}