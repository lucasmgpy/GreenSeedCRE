using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using GreenSeed.Models;
using System.Text.Json;
using System.Text;
using System.Linq;

namespace GreenSeed.Tests.Models
{
    public class SessionExtensionsTests
    {
        [Fact]
        public void Set_SerializesAndStoresObjectInSession()
        {
            // Arrange
            var sessionMock = new Mock<ISession>();
            var key = "TestKey";
            var testObject = new TestClass { Id = 1, Name = "Test" };
            var serializedData = JsonSerializer.Serialize(testObject);
            var serializedBytes = Encoding.UTF8.GetBytes(serializedData);

            // Act
            sessionMock.Object.Set(key, testObject);

            // Assert
            sessionMock.Verify(s => s.Set(key, It.Is<byte[]>(bytes => bytes.SequenceEqual(serializedBytes))), Times.Once);
        }

        [Fact]
        public void Get_DeserializesAndRetrievesObjectFromSession()
        {
            // Arrange
            var sessionMock = new Mock<ISession>();
            var key = "TestKey";
            var testObject = new TestClass { Id = 1, Name = "Test" };
            var serializedData = JsonSerializer.Serialize(testObject);
            var serializedBytes = Encoding.UTF8.GetBytes(serializedData);

            sessionMock.Setup(s => s.TryGetValue(key, out serializedBytes)).Returns(true);

            // Act
            var result = sessionMock.Object.Get<TestClass>(key);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testObject.Id, result.Id);
            Assert.Equal(testObject.Name, result.Name);
        }

        [Fact]
        public void Get_ReturnsDefault_WhenKeyDoesNotExist()
        {
            // Arrange
            var sessionMock = new Mock<ISession>();
            var key = "NonExistingKey";
            byte[] value = null;

            sessionMock.Setup(s => s.TryGetValue(key, out value)).Returns(false);

            // Act
            var result = sessionMock.Object.Get<TestClass>(key);

            // Assert
            Assert.Null(result);
        }

        // Classe auxiliar para os testes
        private class TestClass
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
