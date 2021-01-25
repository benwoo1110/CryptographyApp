using Cryptography.Core;
using Cryptography.Core.Enums;
using NUnit.Framework;

namespace Cryptography.UnitTests
{
    [TestFixture]
    public class TextResultTests
    {
        private TextResult textResult;
        
        [SetUp]
        public void Setup()
        {
            textResult = new TextResult();
        }

        [Test]
        public void IsValid_WhenValid_ReturnTrue()
        {
            textResult.State = ConvertResult.Valid;

            var result = textResult.IsValid();
            
            Assert.That(result, Is.True);
        }
        
        [Test]
        [TestCase(ConvertResult.InvalidLength)]
        [TestCase(ConvertResult.Unknown)]
        [TestCase(ConvertResult.ParseError)]
        public void IsValid_WhenInValid_ReturnFalse(ConvertResult state)
        {
            textResult.State = state;

            var result = textResult.IsValid();
            
            Assert.That(result, Is.False);
        }
        
        [Test]
        [TestCase(ConvertResult.Valid, false)]
        [TestCase(ConvertResult.InvalidLength, false)]
        [TestCase(ConvertResult.Unknown, false)]
        [TestCase(ConvertResult.ParseError, true)]
        public void HasParseError_WhenCalled_ReturnIfHasError(ConvertResult state, bool expectedResult)
        {
            textResult.State = state;

            var result = textResult.HasParseError();
            
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
