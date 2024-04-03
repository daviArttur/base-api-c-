using Moq;
using NUnit.Framework;
using Testes.src.app.contracts.usecases;
using Testes.src.app.interfaces;
using Testes.src.domain.entities;
using Testes.test.stub;

namespace Testes.src.app.usecases.user
{
  public class CreateUserUseCaseTest()
  {

    private CreateUserUseCase _usecase = null!;
    private Mock<IUserRepository> _repositoryMock = null!;
    private Mock<IHashService> _hashService = null!;

    [SetUp]
    public void SetUp()
    {
      this._hashService = new Mock<IHashService>();
      this._repositoryMock = new Mock<IUserRepository>();
      this._usecase = new CreateUserUseCase(this._repositoryMock.Object, this._hashService.Object);
    }

    [Test]
    [Category("unit")]
    public async Task DeveCriarUsuario()
    {
      // Arrange
      string email = "test@mail.com";
      string password = "123123";
      string hashedPassword = "12481749nigh9g8h34";
      User expectedUser = null!;
      this._repositoryMock.Setup(repo => repo.Save(It.IsAny<User>())).Callback<User>(user => expectedUser = user);
      this._hashService.Setup(service => service.Hash(password)).Returns(hashedPassword);
      // Act
      await this._usecase.Perform(email, password);
      // Assert
      this._repositoryMock.Verify(repo => repo.FindOneByEmail(email), Times.Once);
      this._hashService.Verify(service => service.Hash(password), Times.Once);
      this._repositoryMock.Verify(repo => repo.Save(It.IsAny<User>()), Times.Once);
      Assert.Multiple(() =>
      {
        Assert.That(expectedUser.Id, Is.EqualTo(0));
        Assert.That(expectedUser.Email, Is.EqualTo(email));
        Assert.That(expectedUser.Password, Is.EqualTo(hashedPassword));
      });
    }

    [Test]
    [Category("unit")]
    public async Task DeveLancarErroUsuarioEmailExistente()
    {
      // Arrange
      string email = "test@mail.com";
      string password = "123123";
      User userStub = new User(CreateUserDtoStub.GetData());
      this._repositoryMock.Setup(repo => repo.FindOneByEmail(email)).ReturnsAsync(userStub);
      // Act & Assert
      Assert.ThrowsAsync<UserEmailAlreadyUsedException>(async () =>
      {
        await this._usecase.Perform(email, password);
      });
    }
  }
}