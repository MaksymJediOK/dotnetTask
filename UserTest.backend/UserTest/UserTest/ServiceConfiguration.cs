using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace UserTest.Api
{
    public static class ServiceConfiguration
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(configuration["Jwt:Key"])),

                    ValidateIssuer = true,

                    ValidIssuer = configuration["Jwt:Issuer"],

                    ValidateAudience = true,

                    ValidAudience = configuration["Jwt:Audience"],

                    ValidateLifetime = true,

                    ClockSkew = TimeSpan.Zero
                };
            });
        }
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                  {
                    new OpenApiSecurityScheme
                    {
                      Reference = new OpenApiReference
                        {
                           Type = ReferenceType.SecurityScheme,
                           Id= JwtBearerDefaults.AuthenticationScheme
                        }
                      }, new string[]{}
                   }
                });

                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "UserTest API",
                    Description = "API for UserTest",
                    Contact = new OpenApiContact
                    {
                        Email = "UserTest@gmail.com",
                        Name = "Me :)"
                    }
                });
                //string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //options.IncludeXmlComments(xmlPath);
            });
        }
    }
}
