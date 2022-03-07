using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Project.Domain.Shared.Utilities;
using System;
using System.Linq;
using Project.Webframeworks.SwaggerConfig;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Project.Domain.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Exceptions;
using Project.Domain.Shared;
using Project.Domain.Shared.Exceptions;
using System.Net;
using Microsoft.AspNetCore.Identity;
using MediatR;
using Project.Application.Queries.UserQueries;
using Project.Application.Commands.UserCommands;
using Project.EntityFrameworkCore;

namespace WebFramework.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMinimalMvc(this IServiceCollection services)
        {
            //https://github.com/aspnet/Mvc/blob/release/2.2/src/Microsoft.AspNetCore.Mvc/MvcServiceCollectionExtensions.cs
            services.AddMvcCore(options =>
            {
                options.Filters.Add(new AuthorizeFilter());

                //Like [ValidateAntiforgeryToken] attribute but dose not validatie for GET and HEAD http method
                //You can ingore validate by using [IgnoreAntiforgeryToken] attribute
                //Use this filter when use cookie 
                //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

                //options.UseYeKeModelBinder();
            })
            .AddApiExplorer()
            .AddAuthorization()
            .AddFormatterMappings()
            .AddDataAnnotations()
            .AddCors();
        }

        public static void AddCustomApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProjectDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServerConnectionString"),
                    x => x.MigrationsAssembly("Project.EntityFrameworkCore"));
                //options.UseNpgsql(configuration.GetConnectionString("PostgresConnection"),
                //    x => x.MigrationsAssembly("Project.Data"));
            });
        }

        public static void AddJwtAuth(this IServiceCollection services, JwtOptions settings)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                var secretKey = Encoding.UTF8.GetBytes(settings.SecretKey);
                var secretKey2 = Encoding.UTF8.GetBytes(settings.SecretKey2);

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    RequireSignedTokens = true,

                    ValidateIssuer = true,
                    ValidIssuer = settings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = settings.Audience,

                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                     

                    //TokenDecryptionKey = new SymmetricSecurityKey(secretKey2)
                };

                options.Events = new JwtBearerEvents
                {

                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }

                        if (context.Exception != null)
                            throw new AppException(ApiResultStatusCode.AuthenticationFailed, "خطا در احراز هویت", HttpStatusCode.Unauthorized);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = async context =>
                    {
                        var _mediator = context.HttpContext.RequestServices.GetRequiredService<IMediator>();

                        var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                        if (claimsIdentity.Claims?.Any() != true)
                            context.Fail("No claims has been found.");

                        var securityStamp = claimsIdentity.FindFirstValue(new ClaimsIdentityOptions().SecurityStampClaimType);

                        if (!securityStamp.HasValue())
                            throw new AppException(ApiResultStatusCode.ServerError, "مهر امنیتی یافت نشد", HttpStatusCode.Unauthorized);

                        var userIdString = claimsIdentity.GetUserId();
                        var userId = new Guid();
                        var userIdConvertResult = Guid.TryParse(userIdString, out userId);

                        var getUserByIdQuery = new GetUserQuery(userId);
                        var user = await _mediator.Send(getUserByIdQuery);

                        //var user = await userService.FindByIdAsync(userIdString);


                        //Validate by securitystamp
                        var validateSecurityStampCommand = new ValidateSecurityStampCommand(securityStamp);
                        var validateSecurityStampResult = await _mediator.Send(validateSecurityStampCommand);

                        if (!validateSecurityStampResult)
                            throw new AppException(ApiResultStatusCode.ServerError, "مهر امنیتی معتبر نمی باشد", HttpStatusCode.Unauthorized);

                        //Custom validation like IsActive
                        if (!user.IsActive)
                            context.Fail("کاربر فعال نیست");

                        var updateLastLoginDateCommand = new UpdateLastLoginDateCommand(user.Id);
                        await _mediator.Send(updateLastLoginDateCommand);
                    },
                    OnChallenge = context =>
                    {
                        // Skip the default logic.
                        context.HandleResponse();

                        var payload = new JObject
                        {
                            ["status"] = false,
                            ["message"] = "کاربر احراز نشده است",
                            ["code"] = 401
                        };

                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = 401;

                        return context.Response.WriteAsync(payload.ToString());

                        //if (context.AuthenticateFailure != null)
                        //    throw new AuthenticationException(context.AuthenticateFailure.Message, context.AuthenticateFailure.InnerException);
                        //throw new UnauthorizedAccessException("OnChallenge exception" + context.Error + "--" + context.ErrorDescription);

                    }
                };
            });
        }

        //public static void AddCustomIdentity(this IServiceCollection services, CustomIdentityOptions identityOptions)
        //{
        //    //    services.AddIdentity<User, Role>(options =>
        //    //    {
        //    //        //options.Password.RequireDigit = identityOptions.PasswordOptions.RequireDigit;
        //    //        options.Password.RequiredLength = identityOptions.PasswordOptions.RequiredLength;
        //    //        //options.Password.RequiredUniqueChars = identityOptions.PasswordOptions.RequiredUniqueChars;
        //    //        //options.Password.RequireLowercase = identityOptions.PasswordOptions.RequireLowercase;
        //    //        //options.Password.RequireNonAlphanumeric = identityOptions.PasswordOptions.RequireNonAlphanumeric;
        //    //        //options.Password.RequireUppercase = identityOptions.PasswordOptions.RequireUppercase;


        //    //        options.Password.RequireUppercase = false;
        //    //        options.Password.RequireLowercase = false;
        //    //        options.Password.RequireNonAlphanumeric = false;

        //    //        options.User.RequireUniqueEmail = identityOptions.UserOptions.RequireUniqueEmail;
        //    //        //options.User.AllowedUserNameCharacters = identityOptions.UserOptions.AllowedUserNameCharacters;

        //    //        ////SignIn options
        //    //        //options.SignIn.RequireConfirmedAccount = identityOptions.SigninOptions.RequireConfirmedAccount;
        //    //        //options.SignIn.RequireConfirmedEmail = identityOptions.SigninOptions.RequireConfirmedEmail;
        //    //        //options.SignIn.RequireConfirmedPhoneNumber = identityOptions.SigninOptions.RequireConfirmedPhoneNumber;

        //    //        ////Lockout options
        //    //        //options.Lockout.AllowedForNewUsers = identityOptions.LockoutOptions.AllowedForNewUsers;
        //    //        //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(identityOptions.LockoutOptions.DefaultLockoutMinutes);
        //    //        //options.Lockout.MaxFailedAccessAttempts = identityOptions.LockoutOptions.MaxFailedAccessAttempts;

        //    //    })
        //    //        .AddEntityFrameworkStores<ProjectDbContext>()
        //    //        .AddTokenProvider("ApiTokenProvider", typeof(DataProtectorTokenProvider<User>));
        //}

        public static void AddSwaggerService(this IServiceCollection services, JwtOptions jwtOptions)
        {
            Assert.NotNull(services, nameof(services));

            //Add services to use Example Filters in swagger
            //services.AddSwaggerExamples();
            //Add services and configuration to use swagger
            services.AddSwaggerGen(options =>
            {

                //var xmlDocPath = Path.Combine(AppContext.BaseDirectory, "Project.Api.xml");
                ////show controller XML comments like summary
                //options.IncludeXmlComments(xmlDocPath, true);

                //options.EnableAnnotations();
                //options.DescribeAllEnumsAsStrings();
                //options.DescribeAllParametersInCamelCase();
                //options.DescribeStringEnumsInCamelCase()
                //options.UseReferencedDefinitionsForEnums()
                //options.IgnoreObsoleteActions();
                //options.IgnoreObsoleteProperties();

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "OnlineExchange API",
                    //Description = "Talaye-Sefid API Swagger Surface",
                    //Contact = new OpenApiContact
                    //{
                    //    Name = "Kamran Sadin",
                    //    Email = "MrSadin@gmail.com",
                    //    Url = new Uri("https://www.linkedin.com/in/mrsadin/")
                    //},
                    //License = new OpenApiLicense
                    //{
                    //    Name = "MIT",
                    //    Url = new Uri("https://github.com/a-Programmer/Talaye-Sefid")
                    //}
                });
                //options.SwaggerDoc("v2", new OpenApiInfo { Version = "v2", Title = "IADT Api V2" });

                #region Filters
                //Enable to use [SwaggerRequestExample] & [SwaggerResponseExample]
                //options.ExampleFilters();

                //Adds an Upload button to endpoints which have [AddSwaggerFileUploadButton]
                //options.OperationFilter<AddFileParamTypesOperationFilter>();

                //Set summary of action if not already set
                //options.OperationFilter<ApplySummariesOperationFilter>();




                #region Add UnAuthorized to Response

                //Add 401 response and security requirements (Lock icon) to actions that need authorization
                options.OperationFilter<UnauthorizedResponsesOperationFilter>(true, "Bearer");


                //options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Description = @"JWT Token, Please enter token in this format: Bearer [space] token <br/> Example: 'Bearer 12345abcdef'",
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer"
                //});

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Token, Please enter token in this format: Bearer [space] token <br/> Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.OAuth2,
                    BearerFormat = "JWT",
                    Scheme = "Bearer",
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri(jwtOptions.BaseUrl + "/api/v1/account/login"),
                            //AuthorizationUrl = new Uri(jwtOptions.BaseUrl + "/api/v1/Account/login")
                        }

                    }
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new string[]{}
                    }
                });


                #endregion


                #region Versioning
                // Remove version parameter from all Operations
                options.OperationFilter<RemoveVersionParameters>();

                //set version "api/v{version}/[controller]" from current swagger doc verion
                options.DocumentFilter<SetVersionInPaths>();

                //Seperate and categorize end-points by doc version
                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                    var versions = methodInfo.DeclaringType
                        .GetCustomAttributes<ApiVersionAttribute>(true)
                        .SelectMany(attr => attr.Versions);

                    return versions.Any(v => $"v{v.ToString()}" == docName);
                });
                #endregion

                //If use FluentValidation then must be use this package to show validation in swagger (MicroElements.Swashbuckle.FluentValidation)
                //options.AddFluentValidationRules();
                #endregion
            });
        }

    }
}
