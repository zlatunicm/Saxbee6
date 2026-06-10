using Saxbee6.Api.Data.Models;

namespace Saxbee6.Api.DTOs;

public record HarvestDto(int Id, HarvestType Type, double QuantityKg, DateTime HarvestedAt, string? Notes, int HiveId, string HiveName);
public record CreateHarvestDto(HarvestType Type, double QuantityKg, DateTime HarvestedAt, string? Notes, int HiveId);
