namespace Application.Dtos;

public class LoginResponseDto
{
    public required string Token { get; set; }
    public required string Name { get; set; }
    public required string EMail { get; set; }
    public Guid UserId { get; set; }

}