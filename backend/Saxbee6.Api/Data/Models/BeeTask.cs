namespace Saxbee6.Api.Data.Models;

public enum TaskPriority { Low, Medium, High }

public class BeeTask
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public DateTime? DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int? HiveId { get; set; }
    public int? ApiaryId { get; set; }
}
