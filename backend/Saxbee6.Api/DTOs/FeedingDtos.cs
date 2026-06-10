using Saxbee6.Api.Data.Models;

namespace Saxbee6.Api.DTOs;

public record FeedingDto(int Id, FeedType FeedType, double QuantityKg, DateTime FedAt, string? Notes, int HiveId, string HiveName);
public record CreateFeedingDto(FeedType FeedType, double QuantityKg, DateTime FedAt, string? Notes, int HiveId);
