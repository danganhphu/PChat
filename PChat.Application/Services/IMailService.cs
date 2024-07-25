using System.Net.Mail;

namespace PChat.Application.Services;

public interface IMailService
{
    Task SendMailAsync(List<string> emails, string subject, string body, List<Attachment> attachments = null);
}