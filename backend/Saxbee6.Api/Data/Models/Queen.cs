namespace Saxbee6.Api.Data.Models;

public enum QueenStatus { Present, Missing, Laying, NotLaying, Superseded, Replaced }

public class Queen
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? YearBorn { get; set; }
    public QueenStatus Status { get; set; } = QueenStatus.Present;
    public string? Origin { get; set; }
    public string? MarkingColor { get; set; }
    public bool IsMarked { get; set; }
    public bool IsClipped { get; set; }
    public string? Notes { get; set; }

    public int HiveId { get; set; }
    public Hive Hive { get; set; } = null!;
}
