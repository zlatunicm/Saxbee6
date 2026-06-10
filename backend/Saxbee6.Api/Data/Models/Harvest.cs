namespace Saxbee6.Api.Data.Models;

public enum HarvestType { Honey, Beeswax, Propolis, RoyalJelly, Pollen }

public class Harvest
{
    public int Id { get; set; }
    public HarvestType Type { get; set; } = HarvestType.Honey;
    public double QuantityKg { get; set; }
    public DateTime HarvestedAt { get; set; }
    public string? Notes { get; set; }

    public int HiveId { get; set; }
    public Hive Hive { get; set; } = null!;
}
