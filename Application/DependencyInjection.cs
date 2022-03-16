using FluentValidation;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Common.Services;
using GhostWriter.Domain.Entities;
using GhostWriter.Domain.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using static GhostWriter.Domain.Entities.Project;

namespace GhostWriter.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient<IProposalService, ProposalService>();
            services.AddTransient<IBookingPaymentService, BookingPaymentService>();
            services.AddTransient<IProjectTagsService, ProjectTagsService>();
            services.AddTransient<IPriceCalculatorService, PriceCalculatorService>();
            services.AddTransient<IProjectFactory, ProjectFactory>();

            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

            return services;
        }
    }
}
