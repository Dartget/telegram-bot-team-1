using FluentAssertions;
using Moq;
using Xunit;
using TelegramBot.Services;
using TelegramBot.WebHookSetup;
using Microsoft.Extensions.Logging;
using TelegramBot.Types;

namespace TelegramBotTests
{
	public class HandleUpdateServiceTest
	{
		[Fact]
		public void CreateContext_switch_correct()
		{
			//Arrange 
			var botClientMock = new Mock<IWebHookClient>();
			var loggerMock = new Mock<ILogger<HandleUpdateService>>();
			Update update = new Update { Message = new Message { Text = "/example" } };
			var handlerUpdate = new HandleUpdateService(botClientMock.Object, loggerMock.Object);

			//Action 
			var context = handlerUpdate.CreateContext(update);

			//Assert 
			context.strategy.Should().BeOfType<GetExampleService>();
		}

		[Fact]
		public void CreateContext_switch_incorrect()
		{
			//Arrange
			var botClientMock = new Mock<IWebHookClient>();
			var loggerMock = new Mock<ILogger<HandleUpdateService>>();
			Update update = new Update { Message = new Message { Text = "jadfj" } };
			var handlerUpdate = new HandleUpdateService(botClientMock.Object, loggerMock.Object);

			//Action
			var context = handlerUpdate.CreateContext(update);

			//Assert 
			context.strategy.Should().NotBeOfType<GetExampleService>();
		}
	}
}
