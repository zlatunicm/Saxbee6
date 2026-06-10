namespace Saxbee6.Api.Data.Models;

public enum HiveStatus { Active, Inactive, Dead, Swarmed }
public enum HiveType { Langstroth, Dadant, Farrar, Warré, TopBar, Other }

public class Hive
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public HiveType Type { get; set; } = HiveType.Langstroth;
    public HiveStatus Status { get; set; } = HiveStatus.Active;
    public DateTime EstablishedDate { get; set; } = DateTime.UtcNow;
    public string? Color { get; set; }
    public string? Notes { get; set; }

    public int ApiaryId { get; set; }
    public Apiary Apiary { get; set; } = null!;

    public Queen? Queen { get; set; }
    public ICollection<Inspection> Inspections { get; set; } = new List<Inspection>();
    public ICollection<Treatment> Treatments { get; set; } = new List<Treatment>();
    public ICollection<Harvest> Harvests { get; set; } = new List<Harvest>();
    public ICollection<Feeding> Feedings { get; set; } = new List<Feeding>();
}
