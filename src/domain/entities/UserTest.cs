using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Testes.src.domain.dto;

namespace Testes.src.domain.entities
{

  internal class UserTest()
  {

    [Test]
    [Category("unit")]
    public void DeveSerDefinido()
    {
      // Arrange
      CreateUserDto dto = new CreateUserDto(1, "email", "password");
      // Act
      User user = new User(dto);
      // Assert
      Assert.Multiple(() =>
      {
        Assert.That(user.Id, Is.EqualTo(dto.Id));
        Assert.That(user.Email, Is.EqualTo(dto.Email));
        Assert.That(user.Password, Is.EqualTo(dto.Password));
      });
    }

    [Test]
    [Category("unit")]
    public void DeveCriarUmUsuarioComOsParametrosCorretos()
    {
      // Arrange
      string email = "email@gow.com";
      string password = "123";
      // Act
      User user = User.Create(email, password);
      // Assert
      Assert.Multiple(() =>
      {
        Assert.That(user.Id, Is.EqualTo(0));
        Assert.That(user.Email, Is.EqualTo(email));
        Assert.That(user.Password, Is.EqualTo(password));
      });
    }
  }
}