using NUnit.Framework;
using PasswordHasher.Core.Hashing;

namespace PasswordHasher.Core.Tests.Hashing
{
    public class HasherTests
    {
        private Hasher _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _classUnderTest = new Hasher();
        }

        [Test]
        public void ToBase64EncodedSha512_ReturnsHashedEncodedResult()
        {
            var input = "angryMonkey";
            var expectedHash = "ZEHhWB65gUlzdVwtDQArEyx+KVLzp/aTaRaPlBzYRIFj6vjFdqEb0Q5B8zVKCZ0vKbZPZklJz0Fd7su2A+gf7Q==";

            var actualHash = _classUnderTest.ToBase64EncodedSha512(input);

            Assert.That(actualHash, Is.EqualTo(expectedHash));
        }
    }
}