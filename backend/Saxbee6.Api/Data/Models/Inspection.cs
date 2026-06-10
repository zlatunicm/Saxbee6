namespace Saxbee6.Api.Data.Models;

public enum BroodPattern { Excellent, Good, Fair, Poor }
public enum ColonyStrength { Strong, Medium, Weak, VeryWeak }
public enum Temperament { Calm, Gentle, Normal, Defensive, Aggressive }

public class Inspection
{
    public int Id { get; set; }
    public DateTime InspectedAt { get; set; }
    public ColonyStrength Strength { get; set; }
    public BroodPattern BroodPattern { get; set; }
    public Temperament Temperament { get; set; }

    public bool QueenSeen { get; set; }
    public bool EggsSeen { get; set; }
    public bool QueenCellsPresent { get; set; }
    public bool SwarmingSign { get; set; }

    public int? HoneyStoresFrames { get; set; }
    public int? BroodFrames { get; set; }
    public int? BeesFrames { get; set; }

    public double? VarroaCount { get; set; }
    public string? Notes { get; set; }
    public string? Weather { get; set; }
    public double? TemperatureCelsius { get; set; }

    public int HiveId { get; set; }
    public Hive Hive { get; set; } = null!;
}
