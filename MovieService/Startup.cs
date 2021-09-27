using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieService.Database;
using MovieService.Extensions;
using MovieService.Jobs;

namespace MovieService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.AddControllers();

            services.AddDbContext<MoviesDb>(options => { options.UseSqlServer(Configuration.GetConnectionString("MoviesDatabase")); });

            // custom services
            services.AddImDbApiService(Configuration);
            services.AddMovieService();
            services.AddMailSender(Configuration);
            
            services.AddJob<SuggestionSenderJob>("SuggestionSender", Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMigrateDatabaseOnStartup(app);

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie API V1"); });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private static void AutoMigrateDatabaseOnStartup(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbService = scope.ServiceProvider.GetRequiredService<MoviesDb>();
            dbService.Database.Migrate();
        }
    }
}