using System.IO;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Project.Api.Helpers;
using Project.Application.Commands.RoleCommands;
using Project.Application.Commands.UserCommands;
using Project.Application.Queries.RoleQueries;
using Project.Application.Queries.UserQueries;
using Project.Domain;
using Project.Domain.Common;
using Project.Infrastructure;
using Project.Webframeworks.Configurations;
using WebFramework.Configuration;
using Microsoft.AspNetCore.HttpOverrides;

namespace Project.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly PublicSettings _settings;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _settings = configuration.GetSection("PublicSettings").Get<PublicSettings>();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<PublicSettings>(Configuration.GetSection("PublicSettings"));

            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder => builder.AllowAnyOrigin()
                            //.WithMethods("PUT", "DELETE", "GET", "POST")
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });

            services.AddDbContext(Configuration);

            //services.AddCustomIdentity(_settings.CustomIdentityOptions);

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });

            services.AddSwaggerService(_settings.JwtOptions);

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IFileManager, FileManagerHelper>();

            services.AddMediatR(typeof(Startup));
            

            services.AddMediatR(typeof(ValidateUserCommand).Assembly);
            services.AddMediatR(typeof(GenerateAccessTokenCommand).Assembly);
            services.AddMediatR(typeof(UpdateSecurityStampCommand).Assembly);
            services.AddMediatR(typeof(RegisterCommand).Assembly);
            services.AddMediatR(typeof(GetUserQuery).Assembly);
            services.AddMediatR(typeof(GetAllUsersQuery).Assembly);

            services.AddMediatR(typeof(GetAllRolesQuery).Assembly);
            services.AddMediatR(typeof(GetRoleQuery).Assembly);
            services.AddMediatR(typeof(AddRoleCommand).Assembly);
            services.AddMediatR(typeof(EditRoleCommand).Assembly);
            services.AddMediatR(typeof(DeleteRoleCommand).Assembly);



            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddCustomApiVersioning();


            services.AddJwtAuth(_settings.JwtOptions);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseExceptionLogger();

            app.UseHttpsRedirection();

            app.UseCustomSwagger();

            app.MigrateDatabase();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                //DatabaseSeederExtension.Seed(userManager, roleManager, app);
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Talaye-Sefid API v1"));
            }
            else
            {
                //app.UseExceptionHandler();
                app.UseHsts();
                //Delete in production
                //DatabaseSeederExtension.Seed(userManager, roleManager, app);
            }

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
