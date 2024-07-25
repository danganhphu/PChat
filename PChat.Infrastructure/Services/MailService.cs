using System.Net.Mail;
using GenericEmailService;
using PChat.Application.Services;

namespace PChat.Infrastructure.Services;

public  class MailService 
{
    // public async Task SendMailAsync(List<string> emails, string subject, string body, List<Attachment> attachments = null)
    // {
    //     EmailModel<Attachment> model = new(
    //         Configurations: configurations,
    //         FromEmail: "mymail@gmail.com",
    //         ToEmails: ["sendmail1@gmail.com", "sendmail2@gmail.com"],
    //         Subject: "Subjects",
    //         Body: "<b>Body</b>"
    //     );
    //     await EmailService.SendEmailWithSmtpClientAsync(model);
    // }
}