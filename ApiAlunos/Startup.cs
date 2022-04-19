using ApiAlunos.Application.Pipelines;
using ApiAlunos.Application.Repositories.AlunosRepository;
using ApiAlunos.Application.Services.DbSession;
using ApiAlunos.Application.Services.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using System.Data;
using ApiAlunos.Application.HostedServices.AddAluno;
using ApiAlunos.Application.Queries.AlunosQuery;

namespace ApiAlunos
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

            services.AddControllers();

            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(Startup));
            services.AddSingleton<IAlunosRepository, AlunosRepository>();

            services.AddTransient<IDbConnection, MySqlConnection>(_ => new MySqlConnection(Configuration["ConnectionStrings:Default"]));
            services.AddTransient<IDbSession, DbSession>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IAlunosQuery, AlunosQuery>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidatorPipeline<,>));

            services.AddHostedService<AddAlunoHostedService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiAlunos", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiAlunos v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
