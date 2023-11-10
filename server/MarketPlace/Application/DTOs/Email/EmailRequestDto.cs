#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Application.DTOs.Email;

public class EmailRequestDto
{
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public string From { get; set; }
}