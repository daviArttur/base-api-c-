using Testes.src.infra.config.db;
using Testes.src.domain.entities;
using Testes.src.app.interfaces;
using Testes.src.infra.models;
using Testes.src.domain.dto;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Testes.test.stub;

namespace Testes.src.infra.repositories
{
  internal class UserRepositoryTest()
  {
    private AppDbContext _dbContext = null!;
    private IUserRepository _userRepository = null!;
    [SetUp]
    public void SetUp()
    {
      var builder = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
      this._dbContext = new AppDbContext(builder.Options);
      this._userRepository = new UserRepository(this._dbContext);
    }


    [Test]
    [Category("unit")]
    public async Task DeveCriarUsuario()
    {
      // Arrange
      User user = new User(CreateUserDtoStub.GetData(true));
      // Act
      await this._userRepository.Save(user);
      // Assert
      var usersInDB = await this._dbContext.Users.ToListAsync();
      Assert.That(usersInDB, Has.Count.EqualTo(1));
      Assert.Multiple(() => {
        Assert.That(usersInDB[0].Id, Is.EqualTo(1));
        Assert.That(usersInDB[0].Email, Is.EqualTo(user.Email));
        Assert.That(usersInDB[0].Password, Is.EqualTo(user.Password));
      });
    }

    [Test]
    [Category("unit")]
    public async Task DeveLancarExceptionAoCriarUsuario()
    {
      // Assert
      Assert.ThrowsAsync<QueryException>( async () => {
        await this._userRepository.Save((User)null!);
      });
    }
  }
}

