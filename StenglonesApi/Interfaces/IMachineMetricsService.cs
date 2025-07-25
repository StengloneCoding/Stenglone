namespace StenglonesApi.Interfaces;

using StenglonesApi.DTOs;
using StenglonesApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;



public interface IMachineMetricsService
{
    Task SaveMetricAsync(MachineMetricDto dto, CancellationToken cancellationToken = default);
    Task UpdateMetricAsync(int id, MachineMetricDto dto, CancellationToken cancellationToken = default);
    Task<List<MachineMetric>> GetAllAsync(CancellationToken cancellationToken = default);
}

