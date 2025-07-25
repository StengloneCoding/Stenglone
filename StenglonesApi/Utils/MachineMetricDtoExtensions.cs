using StenglonesApi.DTOs;
using StenglonesApi.Models;

namespace StenglonesApi.Utils;


public static class MachineMetricDtoExtensions
{
    public static MachineMetric ToEntity(this MachineMetricDto dto)
    {
        return new MachineMetric
        {
            Temperature = dto.Temperature,
            RotationSpeed = dto.RotationSpeed,
            CreatedAtTimestamp = dto.CreatedAtTimestamp
        };
    }

    public static void MapToExisting(this MachineMetricDto dto, MachineMetric entity)
    {
        entity.Temperature = dto.Temperature;
        entity.RotationSpeed = dto.RotationSpeed;
        entity.UpdatedAtTimestamp = DateTime.UtcNow; 
    }
}
