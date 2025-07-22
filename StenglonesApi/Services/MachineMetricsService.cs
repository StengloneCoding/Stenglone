namespace StenglonesApi.Services
{
    using StenglonesApi.DTOs;
    using StenglonesApi.Models;
    using StenglonesApi.Data;
    using Microsoft.EntityFrameworkCore;

    public class MachineMetricsService
    {
        private readonly AppDbContext _context;

        public MachineMetricsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveMetricAsync(MachineMetricDto dto)
        {
            var entity = new MachineMetric
            {
                Temperature = dto.Temperature,
                RotationSpeed = dto.RotationSpeed,
                Timestamp = dto.Timestamp
            };

            _context.MachineMetrics.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<MachineMetric>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.MachineMetrics.ToListAsync(cancellationToken);
        }
    }

}
