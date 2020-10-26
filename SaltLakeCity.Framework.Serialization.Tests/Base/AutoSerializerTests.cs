using Xunit;

namespace SaltLakeCity.Framework.Serialization.Tests
{
    public class AutoSerializerTests
    {
        [Fact]
        public void Serialize_Success()
        {
            // => Arrange
            var param = new TestParam()
            {
                Name = "Test",
                Age = 25,
                IsOld = true
            };
            var serializer = new TestParamSerializer();

            // => Act
            var serialized = serializer.Serialize(param);
            
            var deserialized = serializer.Deserialize(serialized);

            // => Assert
            Assert.NotNull(deserialized);
            Assert.Equal("Test", deserialized.Name);
            Assert.Equal(25, deserialized.Age);
            Assert.True(deserialized.IsOld);
        }

        private class TestParam
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public bool IsOld { get; set; }
        }

        private class TestParamSerializer : AutoSerializer<TestParam>
        {
            
        }
    }
}