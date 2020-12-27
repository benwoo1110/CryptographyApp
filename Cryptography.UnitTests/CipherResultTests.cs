﻿using Cryptography.Core;
using Cryptography.Core.Enums;
using NUnit.Framework;

namespace Cryptography.UnitTests
{
    [TestFixture]
    public class CipherResultTests
    {
        private CipherResult cipherResult;
        
        [SetUp]
        public void Setup()
        {
            cipherResult = new CipherResult();
        }

        [Test]
        [TestCase(ConvertResult.Valid, ConvertResult.Valid, false)]
        [TestCase(ConvertResult.Invalid, ConvertResult.Invalid, false)]
        [TestCase(ConvertResult.ParseError, ConvertResult.Valid, true)]
        [TestCase(ConvertResult.Valid, ConvertResult.ParseError, true)]
        [TestCase(ConvertResult.ParseError, ConvertResult.ParseError, true)]
        public void HasParsingErrors_WhenCalled_ReturnIfHasErrors(ConvertResult input, ConvertResult key, bool expectedResult)
        {
            cipherResult.ValidInput = input;
            cipherResult.ValidKey = key;

            Assert.That(cipherResult.HasParsingErrors, Is.EqualTo(expectedResult));
        }
    }
}
