using Microsoft.OpenApi.Models;
using RainfallApi.Services;

namespace RainfallApi
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;

        public Startup(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpClient();
            services.AddEndpointsApiExplorer();
            services.AddScoped<IExternalApiService, ExternalApiService>();
            services.AddScoped<IRainfallService, RainfallService>();

            services.AddSwaggerGen(c =>
            {
                /*
                 * Approach below invalid for this particular version of OpenAPI - left in for future reference
                var openApiFilePath = Path.Combine(_environment.ContentRootPath, "openapi", "openapi.yaml");
                if (File.Exists(openApiFilePath))
                {
                    
                    var stream = new FileStream(openApiFilePath, FileMode.Open, FileAccess.Read);
                    var reader = new OpenApiStreamReader();
                    var openApiDocument = reader.Read(stream, out var diagnostic);
                    services.AddSingleton(openApiDocument);
                }
                */
                
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Rainfall API",
                    Version = "1.0",
                    Description = "An API which provides rainfall reading data",
                    Contact = new OpenApiContact
                    {
                        Name = "Sorted",
                        Url = new Uri("https://www.sorted.com")
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            if (_environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rainfall API v1");
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