using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WorkTimeTracker.Application.Employees.Commands.RegisterEmployee;

namespace WorkTimeTracker.Application
{
    public static class ServiceCollectionInstall
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<RegisterEmployeeCommand>());
        }
    }
}
