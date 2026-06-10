using Saxbee6.Api.Data.Models;

namespace Saxbee6.Api.DTOs;

public record BeeTaskDto(int Id, string Title, string? Description, TaskPriority Priority, DateTime? DueDate, bool IsCompleted, DateTime CreatedAt, int? HiveId, int? ApiaryId);
public record CreateBeeTaskDto(string Title, string? Description, TaskPriority Priority, DateTime? DueDate, int? HiveId, int? ApiaryId);
public record UpdateBeeTaskDto(string Title, string? Description, TaskPriority Priority, DateTime? DueDate, bool IsCompleted);
