namespace StenglonesApi.DTOs;
public class MachineMetricDto
{
    required public double Temperature { get; init; }
    required public int RotationSpeed { get; init; }
    public DateTime CreatedAtTimestamp { get; init; } = DateTime.UtcNow;
}

