using Testes.src.domain.dto;

namespace Testes.src.domain.entities
{
  public class User(CreateUserDto dto)
  {
    public int Id { get; } = dto.Id;

    public string Email { get; } = dto.Email;
    public string Password { get; } = dto.Password;

    public static User Create(string email, string password)
    {
      return new User(new CreateUserDto
      {
        Email = email,
        Password = password,
        Id = 0
      });
    }
  }
}