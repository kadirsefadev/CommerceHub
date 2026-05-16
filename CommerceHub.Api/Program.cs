

using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using CommerceHub.Infrastructure.Options;
using CommerceHub.Persistence.Extentsions;
using CommerceHub.Infrastructure.Extensions;
using CommerceHub.Infrastructure.Services;
using CommerceHub.Application.Interfaces;
using CommerceHub.Persistence.Services;
using CommerceHub.Persistence.Seed;
using FluentValidation.AspNetCore;
using FluentValidation;
using CommerceHub.Application.Validators;

namespace CommerceHub.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

          
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CommerceHub API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Bearer{token}"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                       new OpenApiSecurityScheme
                       {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }

                       },
                       Array.Empty<string>()
                    }
                });
            });

            builder.Services.AddPersistenceService(builder.Configuration);
            builder.Services.AddInfrastructureServices(builder.Configuration);

            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<DataSeeder>();

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();

            var jwtOptions = builder.Configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>(); // bu kýsým gdip appsettings.json okuyacaktýr ve ayarlarý oradan almamýzý saglayacaktýr. 

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, // Token kim tarafýndan oluþturulmuþ, yani güvenilir bir kaynak mý?
                    ValidateAudience = true, //Token hangi uygulama veya hizmet için oluþturulmuþ, yani doðru hedefe mi yönlendirilmiþ?
                    ValidateLifetime = true, //Token'ýn süresi dolmuþ mu, yani hala geçerli mi?
                    ValidateIssuerSigningKey = true, //Token'ýn imzalanmasý için kullanýlan anahtarýn geçerli ve güvenilir olup olmadýðýný kontrol eder.
                    ValidIssuer = jwtOptions.Issuer, //beklenen deðeri belirtiyoruz. Bu deðerin token içinde bulunan issuer ile eþleþmesi gerekir.
                    ValidAudience = jwtOptions.Audience, // bekelenen deðeri belirtiyoruz. Bu deðerin token içinde bulunan audience ile eþleþmesi gerekir.
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtOptions.Secret)), // imza dogrulama

                };
            });
          
            builder.Services.AddAuthorization();

            var app = builder.Build();

           // builder.Services.AddPersistenceServices

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

			app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var seeder  = scope.ServiceProvider.GetRequiredService<DataSeeder>();
                await seeder.SeedAsync();
            }


            app.Run();
        }
    }
}
