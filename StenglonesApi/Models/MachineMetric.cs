using System.ComponentModel.DataAnnotations;

namespace StenglonesApi.Models;
public class MachineMetric
{
    [Key]
    public int Id { get; set; }
    public double Temperature { get; set; }
    public int RotationSpeed { get; set; }
    public DateTime CreatedAtTimestamp { get; set; }
    public DateTime? UpdatedAtTimestamp { get; set; }
}


