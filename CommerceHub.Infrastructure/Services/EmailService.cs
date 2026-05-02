using CommerceHub.Application.Interfaces;
using CommerceHub.Domain.Entities;
using CommerceHub.Persistence.UnitOfWorks;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Infrastructure.Services
{
	public class EmailService : IEmailService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger<EmailService> _logger;
		public EmailService(IUnitOfWork unitOfWork, ILogger<EmailService> logger)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
		}


		public async Task SendAsync(string to, string subject, string body)
		{
			_logger.LogInformation("[Email] To:{To} | Subject:{Subject}", to, subject);
			var log = new EmailLog
			{
				To = to,
				Subject = subject,
				Body = body,
				IsSent = true,
				SentAt = DateTime.UtcNow
			};
			await _unitOfWork.EmailLogs.AddAsync(log);
			await _unitOfWork.SaveChangesAsync();
		}
	}
}
