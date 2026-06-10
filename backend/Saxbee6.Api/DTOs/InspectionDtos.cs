using Saxbee6.Api.Data.Models;

namespace Saxbee6.Api.DTOs;

public record InspectionDto(int Id, DateTime InspectedAt, ColonyStrength Strength, BroodPattern BroodPattern, Temperament Temperament,
    bool QueenSeen, bool EggsSeen, bool QueenCellsPresent, bool SwarmingSign,
    int? HoneyStoresFrames, int? BroodFrames, int? BeesFrames, double? VarroaCount,
    string? Notes, string? Weather, double? TemperatureCelsius, int HiveId, string HiveName);

public record CreateInspectionDto(DateTime InspectedAt, ColonyStrength Strength, BroodPattern BroodPattern, Temperament Temperament,
    bool QueenSeen, bool EggsSeen, bool QueenCellsPresent, bool SwarmingSign,
    int? HoneyStoresFrames, int? BroodFrames, int? BeesFrames, double? VarroaCount,
    string? Notes, string? Weather, double? TemperatureCelsius, int HiveId);
