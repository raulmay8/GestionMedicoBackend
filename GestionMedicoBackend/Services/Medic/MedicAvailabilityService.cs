using GestionMedicoBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GestionMedicoBackend.Services
{
    public class MedicAvailabilityService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MedicAvailabilityService> _logger;

        public MedicAvailabilityService(IServiceProvider serviceProvider, ILogger<MedicAvailabilityService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Medic Availability Service running.");

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var medics = await context.Medics.ToListAsync();

                    foreach (var medic in medics)
                    {
                        medic.Availability = !medic.Availability;
                        _logger.LogInformation($"Medic {medic.Id} availability changed to {medic.Availability}.");
                    }

                    await context.SaveChangesAsync();
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }

            _logger.LogInformation("Medic Availability Service stopping.");
        }
    }
}
