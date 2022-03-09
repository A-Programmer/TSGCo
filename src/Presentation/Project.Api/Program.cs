using System;
using System.IO;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Project.Api.Helpers;
using Project.Application;
using Project.Application.Commands.RoleCommands;
using Project.Application.Commands.UserCommands;
using Project.Application.Contracts.Repositories.PostRepositories;
using Project.Application.Handlers.PostHandlers;
using Project.Application.Queries.PostQueries;
using Project.Application.Queries.RoleQueries;
using Project.Application.Queries.UserQueries;
using Project.Domain.Shared;
using Project.EntityFrameworkCore;
using Project.EntityFrameworkCore.Repositories.BlogRepositories;
using Project.Webframeworks.Configurations;
using WebFramework.Configuration;



var builder = WebApplication.CreateBuilder(args);

IConfiguration Configuration;

if(builder.Environment.IsProduction())
{
    Configuration = Configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.Production.json")
                            .Build();
}
else
{
    Configuration = Configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.Development.json")
                            .Build();
}

PublicSettings _settings = Configuration.GetSection("PublicSettings").Get<PublicSettings>();

// Add services to the container.
builder.Services.Configure<PublicSettings>(Configuration.GetSection("PublicSettings"));

builder.Services.Configure<FormOptions>(o => {
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder => builder.AllowAnyOrigin()
                //.WithMethods("PUT", "DELETE", "GET", "POST")
                .AllowAnyMethod()
                .AllowAnyHeader());
});

builder.Services.AddDbContext(Configuration);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.IgnoreNullValues = true;
    });

builder.Services.AddSwaggerService(_settings.JwtOptions);

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IFileManager, FileManagerHelper>();

builder.Services.RegisterMediatR();

// builder.Services.RegisterRepositories();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddCustomApiVersioning();


builder.Services.AddJwtAuth(_settings.JwtOptions);





var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseExceptionLogger();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCustomSwagger();

app.MigrateDatabase();


app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});
app.UseStaticFiles();

app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
