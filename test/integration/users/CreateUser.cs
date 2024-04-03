using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Testes.src.infra.models;
using Testes.test.integration.config;
using Testes.test.integration.mock;

namespace Testes.test.integration.users
{

  [ExcludeFromCodeCoverage]
  internal class CreateUserTest()
  {
    private ApplicationSetup _app = null!;
    private HttpClient _client = null!;

    [SetUp]
    public void Setup()
    {
      this._app = new ApplicationSetup();
      this._app.InitializeAsync().Wait();
      SeedHelper.ApplyMigrations(this._app).Wait();
      this._client = this._app.CreateClient();
    }

    [TearDown]
    public async Task TearDown()
    {
      await this._app.DisposeAsync();
    }

    [Test]
    [Category("integration")]
    public async Task DeveCriarUsuario()
    {
      // Arrange
      string email = "test@mail.com";
      string password = "D3th3nf2@";
      var requestBody = JsonConvert.SerializeObject(new { email, password });
      var httpContent = new StringContent(requestBody, Encoding.UTF8, "application/json");
      // Act
      var response = await this._client.PostAsync("/api/users", httpContent);
      // Assert
      var usersModel = await SeedHelper.ToListAsync<UserModel>(this._app);
      //Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
      Assert.Multiple(() => {
        Assert.That(usersModel, Has.Count.EqualTo(1));
        Assert.That(usersModel[0].Id, Is.EqualTo(1));
        Assert.That(usersModel[0].Email, Is.EqualTo(email));
        Assert.That(usersModel[0].Password, Is.Not.EqualTo(password));
      });
    }
  }
}