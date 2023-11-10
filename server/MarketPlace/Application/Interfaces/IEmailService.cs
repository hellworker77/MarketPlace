using Application.DTOs.Email;

namespace Application.Interfaces;

public interface IEmailService
{
    Task SendAsync(EmailRequestDto request);
}