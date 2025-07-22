namespace StenglonesApi.DTOs
{
    public class MachineMetricDto
    {
        public double Temperature { get; set; }
        public int RotationSpeed { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
