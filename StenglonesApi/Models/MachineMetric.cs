namespace StenglonesApi.Models
{
    public class MachineMetric
    {
        public int Id { get; set; }
        public double Temperature { get; set; }
        public int RotationSpeed { get; set; }
        public DateTime Timestamp { get; set; }
    }

}
