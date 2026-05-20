using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSR.DAL.Repository.SemesterRepo;

namespace RSR.BLL.Service.semesterService
{
    public class SemesterBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public SemesterBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        protected override async System.Threading.Tasks.Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var semesterRepo = scope.ServiceProvider.GetRequiredService<ISemesterRepository>();
                    var semesterActive = await semesterRepo.GetActiveSemester();
                    if (semesterActive != null && semesterActive.EndDate <= DateTime.UtcNow)
                    {
                        semesterActive.IsActive = false;
                        await semesterRepo.UpdateSemester(semesterActive);
                    }
                }
                await System.Threading.Tasks.Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
