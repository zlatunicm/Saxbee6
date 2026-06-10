using Saxbee6.Api.Data.Models;

namespace Saxbee6.Api.DTOs;

public record TreatmentDto(int Id, TreatmentType Type, string Product, string? Dosage, DateTime StartDate, DateTime? EndDate, string? Notes, int HiveId, string HiveName);
public record CreateTreatmentDto(TreatmentType Type, string Product, string? Dosage, DateTime StartDate, DateTime? EndDate, string? Notes, int HiveId);
public record UpdateTreatmentDto(TreatmentType Type, string Product, string? Dosage, DateTime StartDate, DateTime? EndDate, string? Notes);
