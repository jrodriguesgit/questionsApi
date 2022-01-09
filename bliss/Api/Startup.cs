using Application.Commands.GetQuestions;
using Configuration;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Persistence;
using Profiles;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });

            var sqlOptions = Configuration.GetSection(SqlOptions.Key).Get<SqlOptions>();

            //services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(sqlOptions.ConnectionString));
            services.AddPooledDbContextFactory<ApiDbContext>((options) =>
                {
                    options.UseSqlServer(sqlOptions.ConnectionString);
                });

            _ = services.AddMediatR(typeof(GetQuestionsCommand).Assembly);

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddHealthChecks()
                .AddCheck(
                    "BlissDB-check",
                    new SqlConnectionHealthCheck(sqlOptions.ConnectionString),
                    HealthStatus.Unhealthy,
                    new string[] { "api-questions" });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));                
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            MigrateDatabase(app, env);
        }
        private static void MigrateDatabase(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using var sqlcontext = app.ApplicationServices.GetRequiredService<IDbContextFactory<ApiDbContext>>().CreateDbContext();
            sqlcontext.Database.Migrate();
        }
    }
}
