using CommerceHub.Application.Interfaces;
using CommerceHub.Infrastructure.Options;
using CommerceHub.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CommerceHub.Infrastructure.Extensions;

public static class InfrastructureServiceExtensions
{
	public static IServiceCollection AddInfrastructureServices(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		// Options
		services.Configure<JwtOptions>(
			configuration.GetSection(JwtOptions.SectionName));

		services.Configure<EmailOptions>(
			configuration.GetSection(EmailOptions.SectionName));

		// Services
		services.AddScoped<IJwtTokenService, JwtTokenService>();
		services.AddScoped<IEmailService, EmailService>();
		services.AddScoped<CommerceHub.Application.Interfaces.Payment.IPaymentProviderFactory, CommerceHub.Infrastructure.Services.Payment.PaymentProviderFactory>();

		return services;
	}
}