using CsvHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Stereograph.TechnicalTest.Api.Entities;
using Stereograph.TechnicalTest.Api.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Stereograph.TechnicalTest.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<TesttechniqueContext>(options =>
        {
            options
                .UseSqlite("Data Source=testtechnique.db")
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        });

        services.AddSwaggerGen(options =>
        {
            options.DescribeAllParametersInCamelCase();
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Stereograph.TechTechnique.Api", Version = "v1" });
            options.CustomSchemaIds(schema => schema.FullName);
        });

        services
            .AddControllers();
    }

    private List<Project> ReadCsv()
    {
        using (var reader = new StreamReader("Ressources\\projects.csv"))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = new List<Project>();
            csv.Read();
            csv.ReadHeader();
            while (csv.Read())
            {
                var record = new Project
                {
                    Id = csv.GetField<int>("projects/id"),
                    Name = csv.GetField<string>("projects/nom"),
                    Description = csv.GetField<string>("projects/description"),
                    Comment = csv.GetField<string>("projects/commentaire"),
                    Step = csv.GetField<string>("projects/etape"),
                };

                records.Add(record);
            }

            return records;
        }
    }

    public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            application
                .UseDeveloperExceptionPage()
                .UseSwagger()
                .UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Stereograph.TechTechnique.Api V1"));
        }

        application
            .UseHttpsRedirection()
            .UseRouting()
            .UseCors(opts => opts.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin())
            .UseEndpoints(endpoints => endpoints.MapControllers());

        using IServiceScope scope = application.ApplicationServices.CreateScope();
        IServiceProvider services = scope.ServiceProvider;
        TesttechniqueContext appDbContext = services.GetRequiredService<TesttechniqueContext>();
        appDbContext.Database.Migrate();

        try
        {
            appDbContext.AddRange(ReadCsv());
            appDbContext.SaveChanges();
        }
        catch (Exception e)
        {
            // empty catch to avoid crash when adding csv data with specific id
        }
    }
}
