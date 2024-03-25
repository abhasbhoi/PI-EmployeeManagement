using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EmployeeManagement.BusinessLayers;
using EmployeeManagement.BusinessLayers.Contracts;
using EmployeeManagement.Models;
using EmployeeManagement.Repository;
using EmployeeManagement.Repository.Contract;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using EmployeeManagement.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var MyAllowSpecificOrigins = "_apiBaseAllowSpecificOrigins";
builder.Services.AddCors(option => option.AddPolicy(name: MyAllowSpecificOrigins,
    policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        //policy.WithOrigins("www.testurl.com,www.programmerinside.com").WithHeaders("ACCEPT,CONTENT-TYPE").WithMethods("GET,POST");
    }));

// add services related to controller and HTTP requests
builder.Services.AddControllers();

builder.Services.AddDbContext<EmployeeDBContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

// Register instances
builder.Services.AddScoped<IEmployeeBusinessLayer, EmployeeBusinessLayer>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Employee Management"
    });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

//builder.Services.AddApiVersioning(x =>
//{
//    x.DefaultApiVersion = new ApiVersion(1, 0);
//    x.AssumeDefaultVersionWhenUnspecified = true;
//    x.ReportApiVersions = true;
//});

var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee Management");
    });

app.UseCors(MyAllowSpecificOrigins);

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

app.UseMiddleware(typeof(GlobalExceptionMiddleWarer));

app.Run();

