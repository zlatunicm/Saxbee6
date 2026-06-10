using Saxbee6.Api.Data.Models;

namespace Saxbee6.Api.DTOs;

public record HiveDto(int Id, string Name, string? Description, HiveType Type, HiveStatus Status, DateTime EstablishedDate, string? Color, string? Notes, int ApiaryId, string ApiaryName, QueenDto? Queen);
public record HiveSummaryDto(int Id, string Name, HiveType Type, HiveStatus Status, string? Color, int ApiaryId, QueenStatus? QueenStatus, DateTime? LastInspection);
public record CreateHiveDto(string Name, string? Description, HiveType Type, string? Color, string? Notes, int ApiaryId, DateTime? EstablishedDate);
public record UpdateHiveDto(string Name, string? Description, HiveType Type, HiveStatus Status, string? Color, string? Notes);
