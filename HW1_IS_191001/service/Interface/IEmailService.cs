using domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace service.Interface
{
	public interface IEmailService
	{
		Task SendEmailAsync(List<EmailMessage> allMails);
	}
}
