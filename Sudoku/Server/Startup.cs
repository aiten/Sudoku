/*
  This file is part of Sudoku - A library to solve a sudoku.

  Copyright (c) Herbert Aitenbichler

  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
  to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
  and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

  The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
  WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
*/

namespace Sudoku.Server
{
    using System;
    using System.Reflection;
    using System.Threading;

    using Framework.Dependency;
    using Framework.Localization;
    using Framework.Localization.Abstraction;
    using Framework.Logic.Abstraction;
    using Framework.Startup;
    using Framework.Tools.Password;
    using Framework.WebAPI.Filter;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;

    using Sudoku.WebAPI;
    using Sudoku.WebAPI.Controllers;
    using Sudoku.WebAPI.Hubs;

    using Newtonsoft.Json.Serialization;

    using NLog;

    using Swashbuckle.AspNetCore.Filters;

    public class Startup
    {
        private const string CorsAllowAllName     = "AllowAll";
        private const string AuthenticationScheme = "BasicAuthentication";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

//            string connectString = SqliteDatabaseTools.SetEnvironment(Microsoft.Azure.Web.DataProtection.Util.IsAzureEnvironment());

//            GlobalDiagnosticsContext.Set("connectionString", connectString);
            GlobalDiagnosticsContext.Set("version",     Assembly.GetExecutingAssembly().GetName().Version?.ToString());
            GlobalDiagnosticsContext.Set("application", "Sudoku.WebAPI.Server");
            GlobalDiagnosticsContext.Set("username",    Environment.UserName);
        }

        public        IConfiguration                           Configuration { get; }
        public static IServiceProvider                         Services      { get; private set; }
        public static IHubContext<SudokuHub, ISudokuHubClient> Hub           => Services.GetService<IHubContext<SudokuHub, ISudokuHubClient>>();

        public void ConfigureServices(IServiceCollection services)
        {
            var localizationCollector = new LocalizationCollector();
            var moduleInit            = new InitializationManager();

            moduleInit.Add(new Framework.Tools.ModuleInitializer());
            /*
                       moduleInit.Add(new Framework.Schedule.ModuleInitializer());
                       moduleInit.Add(new Framework.Logic.ModuleInitializer()
                       {
                           MapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile<LogicAutoMapperProfile>(); })
                       });
           */
            var controllerAssembly = typeof(InfoController).Assembly;

            services.AddSingleton<ILocalizationCollector>(localizationCollector);

            services.AddControllers();

            services.AddCors(options => options.AddPolicy(CorsAllowAllName, options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()));

            services.AddSignalR(hu => hu.EnableDetailedErrors = true);

            services.AddTransient<UnhandledExceptionFilter>();
            services.AddTransient<MethodCallLogFilter>();
            services.AddMvc(
                    options =>
                    {
                        options.EnableEndpointRouting = false;
                        options.Filters.AddService<UnhandledExceptionFilter>();
                        options.Filters.AddService<MethodCallLogFilter>();
                    })
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddNewtonsoftJson(
                    options =>
                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver())
                .AddApplicationPart(controllerAssembly);

            services.AddAuthentication(AuthenticationScheme)
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(AuthenticationScheme, null);

            services.AddAuthorization(options => { options.AddPolicy(Policies.IsAdmin, policy => policy.RequireClaim("CNCLibClaimTypes.IsAdmin")); });

            services.AddScoped<IAuthenticationManager, WebAPI.Manager.UserManager>();
            services.AddTransient<IOneWayPasswordProvider, Pbkdf2PasswordProvider>();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sudoku API", Version = "v1" });
                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name        = "Authorization",
                    Type        = SecuritySchemeType.Http,
                    Scheme      = "basic",
                    In          = ParameterLocation.Header,
                    Description = "Basic Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id   = "basic"
                            }
                        },
                        new string[] { }
                    }
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>(true, "basic");
            });

            moduleInit.Initialize(services, localizationCollector);

//            services.AddScoped<ICNCLibUserContext, CNCLibUserContext>();

            AppService.ServiceCollection = services;
            AppService.BuildServiceProvider();
        }

        private void OnShutdown()
        {
//            _flushCallStatisticsJob?.Execute().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            var applicationLifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
            applicationLifetime.ApplicationStopping.Register(OnShutdown);

            Services = app.ApplicationServices;

            //          CNCLibContext.InitializeDatabase(Services);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseCors(CorsAllowAllName);

            app.UseAuthentication();
            app.UseAuthorization();

            void callback(object x)
            {
                Hub.Clients.All.HeartBeat();
            }

            var timer = new Timer(callback);
            timer.Change(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(30));

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sudoku API V1"); });

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapHub<SudokuHub>("/sudokuSignalR");
                    endpoints.MapDefaultControllerRoute();
                });
/*
            app.UseSpa(
                spa =>
                {
                    spa.Options.SourcePath = "ClientApp";

                    if (env.IsDevelopment())
                    {
                        spa.UseAngularCliServer(npmScript: "start");
                    }
                });
*/
        }
    }
}