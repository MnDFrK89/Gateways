using Gateways.API.Utils.JsonLocalization;
using Gateways.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.IO.Compression;
using System.Reflection;

namespace Gateways.API
{
    public static class ServicesExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration config)
        {
            _ = services.AddDbContextPool<CoreDbContext>(
                dbContextOptions => dbContextOptions
             .UseSqlServer(config.GetConnectionString("ApiConnection2")));
             //.UseInMemoryDatabase("Gateways")
             //.EnableSensitiveDataLogging()
           //.EnableDetailedErrors());
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                // define swagger docs and other options
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Core",
                    Version = "v1",
                    Description = "API Core"
                });
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter JWT Bearer authorization token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lowercase!!!
                    BearerFormat = "Bearer {token}",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        // defines scope - without a protocol use an empty array for global scope
                        { securityScheme, Array.Empty<string>() }
                    }
                );
                var basePath = AppContext.BaseDirectory;
                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var fileName = Path.Combine(basePath, xmlFile);
                options.IncludeXmlComments(fileName);
            });
        }

        public static void ConfigurePerformance(this IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/svg+xml" });
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                    builder.SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithExposedHeaders(new string[] { "PagingData", "Authorization", "RefreshToken" })
                        );
            });
        }

        public static void ConfigureI18N(this IServiceCollection services)
        {
            services.AddLocalization(o =>
            {
                o.ResourcesPath = "i18n";
            });
            services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
            services.AddSingleton<IStringLocalizer, JsonStringLocalizer>();
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix,
                opts => { opts.ResourcesPath = "i18n"; })
            .AddDataAnnotationsLocalization(options =>
            {
            });
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

        }

        public static void ConfigureHealthChecks(this IServiceCollection services, IConfiguration config)
        {
            services.AddHealthChecks()
                   .AddMySql(config.GetConnectionString("ApiConnection"));
        }

        public static void ConfigureDetection(this IServiceCollection services)
        {
            services.AddDetection();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }

        public static void ConfigureHsts(this IServiceCollection services)
        {
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });
        }
    }
}