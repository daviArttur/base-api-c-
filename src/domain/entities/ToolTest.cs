using NUnit.Framework;
using Testes.src.domain.dto;
using Testes.test.stub;

namespace Testes.src.domain.entities
{
  internal class ToolTest()
  {
    [Test]
    [Category("unit")]
    public void ItShouldBeDefined()
    {
      // Stub
      List<string> emptyTagList = [];

      // Arrange
      CreateToolDto createToolDto = CreateToolDtoStub.GetData();

      // Act
      var Tool = new Tool(createToolDto);

      // Assert
      Assert.Multiple(() =>
      {
        Assert.That(Tool.Id, Is.EqualTo(createToolDto.Id));
        Assert.That(Tool.Title, Is.EqualTo(createToolDto.Title));
        Assert.That(Tool.Link, Is.EqualTo(createToolDto.Link));
        Assert.That(Tool.Description, Is.EqualTo(createToolDto.Description));
        Assert.That(Tool.Tags, Is.EqualTo(emptyTagList));
      });
    }
  }
}