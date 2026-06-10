namespace Saxbee6.Api.Data.Models;

public enum FeedType { SugarSyrup, DrysugarCake, Pollen, Fondant, Other }

public class Feeding
{
    public int Id { get; set; }
    public FeedType FeedType { get; set; }
    public double QuantityKg { get; set; }
    public DateTime FedAt { get; set; }
    public string? Notes { get; set; }

    public int HiveId { get; set; }
    public Hive Hive { get; set; } = null!;
}
