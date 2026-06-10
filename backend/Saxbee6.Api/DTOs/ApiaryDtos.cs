using Saxbee6.Api.Data.Models;

namespace Saxbee6.Api.DTOs;

public record ApiaryDto(int Id, string Name, string? Description, string? Location, double? Latitude, double? Longitude, DateTime CreatedAt, int HiveCount);
public record CreateApiaryDto(string Name, string? Description, string? Location, double? Latitude, double? Longitude);
public record UpdateApiaryDto(string Name, string? Description, string? Location, double? Latitude, double? Longitude);
