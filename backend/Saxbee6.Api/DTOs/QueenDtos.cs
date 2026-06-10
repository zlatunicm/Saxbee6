using Saxbee6.Api.Data.Models;

namespace Saxbee6.Api.DTOs;

public record QueenDto(int Id, string? Name, int? YearBorn, QueenStatus Status, string? Origin, string? MarkingColor, bool IsMarked, bool IsClipped, string? Notes, int HiveId);
public record CreateQueenDto(string? Name, int? YearBorn, QueenStatus Status, string? Origin, string? MarkingColor, bool IsMarked, bool IsClipped, string? Notes, int HiveId);
public record UpdateQueenDto(string? Name, int? YearBorn, QueenStatus Status, string? Origin, string? MarkingColor, bool IsMarked, bool IsClipped, string? Notes);
