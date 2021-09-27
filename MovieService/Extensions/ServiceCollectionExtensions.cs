using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieService.Services.Abstractions;
using MovieService.Services.ImdbApi;
using MovieService.Services.MailSender;
using Quartz;

namespace MovieService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMovieService(this IServiceCollection services)
        {
            services.AddScoped<IMovieService, Services.MovieService.MovieService>();
        }

        public static void AddImDbApiService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ImDbClientOptions>(configuration.GetSection("ImDb"));
            services.AddHttpClient<IImDbApiService, ImDbApiService>();
        }

        public static void AddMailSender(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<MailSenderOptions>(configuration.GetSection("MailSender"));
            service.AddScoped<IMailSender, MailSender>();
        }

        public static void AddJob<TJob>(this IServiceCollection services, string jobName, IConfiguration configuration)
            where TJob : IJob
        {
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                var jobKey = new JobKey(jobName);

                q.AddJob<TJob>(opts => opts.WithIdentity(jobKey));
                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithCronSchedule(configuration[$"Jobs:{jobKey.Name}:Schedule"]));
            });
        }
    }
}