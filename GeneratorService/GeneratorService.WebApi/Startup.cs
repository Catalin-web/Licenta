using GeneratorService.WebApi.Settings;

namespace GeneratorService.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            ISettingsProvider settingsProvider = services
                .SetupSettings();
            services.SetupOpenTelemetry(settingsProvider);
            services.SetupSwagger();
            services.SetupDataContext(settingsProvider);
            services.SetupDatabase();
            services.SetupServices(settingsProvider);
            services.AddCors();

            services.AddControllers(options =>
            {
                options.AllowEmptyInputInBodyModelBinding = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            app.UseWebSockets();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // LIVE ON: http://localhost:12800/generatorservice/swagger/index.html
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "generatorservice/swagger/{documentName}/generatorservice.json";
            });
            app.UseSwaggerUI(
                options =>
                {
                    options.RoutePrefix = "generatorservice/swagger";
                    options.SwaggerEndpoint("v1/generatorservice.json", "Generator Service");
                });
        }
    }
}
