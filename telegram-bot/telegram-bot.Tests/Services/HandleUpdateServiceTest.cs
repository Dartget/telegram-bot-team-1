using FluentAssertions;
using Moq;
using Xunit;
using TelegramBot.Services;
using TelegramBot.Services.GetMovie;
using TelegramBot.Services.Weather;
using TelegramBot.WebHookSetup;
using Microsoft.Extensions.Logging;
using TelegramBot.Types;

namespace TelegramBotTests
{
	public class HandleUpdateServiceTest
	{
		[Fact]
		public void CreateContext_switch_examle()
		{
			//Arrange
			var botClientMock = new Mock<IWebHookClient>();
			var loggerMock = new Mock<ILogger<HandleUpdateService>>();
			Update update = new Update { Message = new Message { Text = "/start" } };
			var handlerUpdate = new HandleUpdateService(botClientMock.Object, loggerMock.Object);

			//Action
			var context = handlerUpdate.CreateContext(update);

			//Assert
			context.strategy.Should().BeOfType<GetCommandsService>();
		}

		[Fact]
		public void CreateContext_switch_incorrectMessage()
		{
			//Arrange
			var botClientMock = new Mock<IWebHookClient>();
			var loggerMock = new Mock<ILogger<HandleUpdateService>>();
			Update update = new Update { Message = new Message { Text = "jadfj" } };
			var handlerUpdate = new HandleUpdateService(botClientMock.Object, loggerMock.Object);

			//Action
			var context = handlerUpdate.CreateContext(update);

			//Assert
			context.strategy.Should().BeOfType<GetIncorrectMessage>();
		}
		[Fact]
		public void CreateContext_switch_getmovie()
		{
			//Arrange
			var botClientMock = new Mock<IWebHookClient>();
			var loggerMock = new Mock<ILogger<HandleUpdateService>>();
			Update update = new Update { Message = new Message { Text = "/getmovie" } };
			var handlerUpdate = new HandleUpdateService(botClientMock.Object, loggerMock.Object);

			//Action
			var context = handlerUpdate.CreateContext(update);

			//Assert
			context.strategy.Should().BeOfType<GetMovieService>();
		}
	}
}
