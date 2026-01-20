using System.ComponentModel.DataAnnotations;

namespace ContractorOps.Api.Dtos;

public class ClientRequestDto
{
    [MaxLength(200)]
    [Required]
    public string Name { get; set; } = string.Empty;
    [MaxLength(500)]
    public string? Website { get; set; } 
    [MaxLength(2000)]
    public string? Notes { get; set; }
}