namespace Saxbee6.Api.Data.Models;

public enum TreatmentType { Varroa, Nosema, AmericanFoulbrood, EuropeanFoulbrood, Chalkbrood, Pesticide, Other }

public class Treatment
{
    public int Id { get; set; }
    public TreatmentType Type { get; set; }
    public string Product { get; set; } = string.Empty;
    public string? Dosage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Notes { get; set; }

    public int HiveId { get; set; }
    public Hive Hive { get; set; } = null!;
}
