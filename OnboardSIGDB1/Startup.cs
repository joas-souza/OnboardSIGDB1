using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Dominio.Servicos;
using OnboardSIGDB1.Dominio.Servicos.Notificacoes;
using OnboardSIGDB1.Dominio.Utils;
using OnboardSIGDB1.Infraestrutura.Consultas;
using OnboardSIGDB1.Infraestrutura.Contexto;
using OnboardSIGDB1.Infraestrutura.MapperProjeto;
using OnboardSIGDB1.Infraestrutura.Repositorios;

namespace OnboardSIGDB1
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
            services.AddRouting();

            AutoMapperConfiguration.Initialize();

            services.AddDbContext<OnboardDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "OnboardSIGDB1", Description = "A simple example ASP.NET Core Web API", TermsOfService = new Uri("https://example.com/terms"), Contact = new OpenApiContact { Name = "Shayne Boyer", Email = string.Empty, Url = new Uri("https://twitter.com/spboyer"), }, License = new OpenApiLicense { Name = "Use under LICX", Url = new Uri("https://example.com/license"), } }); });
           
            services.AddMvc(options => options.Filters.Add<NotificationFilter>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddScoped<NotificationContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IArmazenadorDeEmpresa, ArmazenadorDeEmpresa>();
            services.AddScoped<IAlteradorDeEmpresa, AlteradorDeEmpresa>();
            services.AddScoped<IRemovedorDeEmpresa, RemovedorDeEmpresa>();
            services.AddScoped<IRepositorioDeEmpresa, RepositorioDeEmpresa>();
            services.AddScoped<IConsultasDeEmpresa, ConsultasDeEmpresa>();

            services.AddScoped<IArmazenadorDeCargo, ArmazenadorDeCargo>();
            services.AddScoped<IAlteradorDeCargo, AlteradorDeCargo>();
            services.AddScoped<IRemovedorDeCargo, RemovedorDeCargo>();
            services.AddScoped<IRepositorioDeCargo, RepositorioDeCargo>();
            services.AddScoped<IConsultasDeCargo, ConsultasDeCargo>();

            services.AddScoped<IArmazenadorDeFuncionario, ArmazenadorDeFuncionario>();
            services.AddScoped<IAlteradorDeFuncionario, AlteradorDeFuncionario>();
            services.AddScoped<IVinculadorDeEmpresaAoFuncionario, VinculadorDeEmpresaAoFuncionario>();
            services.AddScoped<IVinculadorDeCargoAoFuncionario, VinculadorDeCargoAoFuncionario>();
            services.AddScoped<IRemovedorDeFuncionario, RemovedorDeFuncionario>();
            services.AddScoped<IRepositorioDeFuncionario, RepositorioDeFuncionario>();
            services.AddScoped<IConsultasDeFuncionario, ConsultasDeFuncionario>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                await next.Invoke();
                string method = context.Request.Method;
                var allowedMethodsToCommit = new string[] { "POST", "PUT", "DELETE" };
                if (allowedMethodsToCommit.Contains(method))
                {
                    var notificationContext = (NotificationContext)context.RequestServices.GetService(typeof(NotificationContext));

                    if (!notificationContext.HasNotifications)
                    {
                        var unitOfWork = (IUnitOfWork)context.RequestServices.GetService(typeof(IUnitOfWork));
                        
                        await unitOfWork.Commit();
                    }
                }
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", "OnboardSIGDB1 v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
