using FluentAssertions;
using Moq;
using SomeProject.Services;
using Xunit;

namespace SomeProject.Tests.Services
{
    /// <summary>
    /// На каждый проект создается один тестовый проект с добавлением постфикса Tests, в нашем случае SomeProject.Tests.
    /// На каждый подлежащий тестированию класс создаётся соответствующий тестовый класс в проекте с тестами.
    /// Структура тестового проекта обычно повторяет стурктуру тестируемого,
    /// чтобы легкче было сопостовлять классы и относящиеся к ним тесты.
    /// Если класс Customer находится в SomeProject.Services, CustomerTest помещается в SomeProject.Tests.Services.
    /// Тестируемый класс называется SUT (system under test).
    /// SUT не мокается, создаётся экземпляр реального класса.
    /// Все же зависимости класса мокаются*, если только не являются простой иммутабельной структурой.
    /// В идеале все зависимости должны быть интерфейсами и внедряться через конструктор или
    /// передаваться как параметры тестируемого метода (сдедуем заветам из SOLI[D]).
    /// Это позволит протестировать SUT полностью изолированно.
    /// Для моков используем Moq фреймворк, который позволяет не только подменить зависимости (.Setup),
    /// но и проверить ожидания, относительно вызова отлельных методов (.Verify).
    /// 
    /// Тестируется обычно однин метод для одного состояния за раз, не стоит писать один [Fact], проверяющий сразу
    /// наличие товара, его отсутствие и, например, выброс исключения.
    /// Несколько однотипных состоний можно при желании протестировать, использую [Theory]**.
    /// **https://andrewlock.net/creating-parameterised-tests-in-xunit-with-inlinedata-classdata-and-memberdata/
    ///
    /// *Различают, как минимум, Fake, Stub и Mock, но это отдельная тема, выходящая за рамки текущего обсуждения.
    ///
    /// P.s. не забываем про AAA и FIRST
    /// Дополнительные примеры можно посмотреть тут: https://github.com/xunit/samples.xunit
    /// </summary>
    
    public class CustomerTest
    {
        [Fact]
        public void Purchase_succeeds_when_enough_inventory()
        {
            // Arrange
            var storeMock = new Mock<IStore>();
            storeMock
                .Setup(store => store.HasEnoughInventory(Product.Hamburger, 5))
                .Returns(true);
            var customer = new Customer(storeMock.Object);
            // Act
            var success = customer.Purchase(Product.Hamburger, 5);
            // Assert
            success.Should().Be(true);
            storeMock.Verify(store => store.RemoveInventory(Product.Hamburger, 5), Times.Once);
        }

        /// <summary>
        /// Fact - простое однозначное утверждение, которое мы тестируем, отражается в названии метода.
        /// В данном случае проверяем негативный сценарий, если 5 гамбургеров у нас нет:
        ///     - Результат должен вернуться отрицательный;
        ///     - метод RemoveInventory НЕ должен быть вызван.
        /// </summary>
        [Fact]
        public void Purchase_fails_when_not_enough_inventory()
        {
            // Arrange
            var storeMock = new Mock<IStore>();
            storeMock
                .Setup(store => store.HasEnoughInventory(Product.Hamburger, 5))
                .Returns(false);
            var customer = new Customer(storeMock.Object);
            // Act
            var success = customer.Purchase(Product.Hamburger, 5);
            // Assert
            success.Should().Be(false);
            storeMock.Verify(store => store.RemoveInventory(Product.Hamburger, 5), Times.Never);
        }
    }
}