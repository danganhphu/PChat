using PChat.Application.Model.Email;

namespace PChat.Application.Contracts.Email;

public interface IEmailSender
{
    Task<bool> SendEmail(EmailMessage email);
}