using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace POS.Api.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection service)
    {
        var openApi = new OpenApiInfo
        {
            Title = "POS API",
            Version = "V1",
            Description = "Punto de Login Api 2023",
            TermsOfService = new Uri("https://example.com/terms"),
            Contact = new OpenApiContact
            {
                Name = "CES TECH",
                Email = "Chismaquis@hotmail.com",
                Url = new Uri("https://example.com/contact")
            },
            License = new OpenApiLicense
            {
                Name = "Use under LICX",
                Url = new Uri("https://example.com/license")
            },
        };
        service.AddSwaggerGen(x =>
        {
            openApi.Version = "v1";
            x.SwaggerDoc("v1", openApi);
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "JWT Bearer Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            x.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            x.AddSecurityRequirement(
                new OpenApiSecurityRequirement { { securityScheme, new string[] { } } }
            );
        });
        return service;
    }
}
