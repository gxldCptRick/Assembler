using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using AssemblerLib.Commands;
using AssemblerLib.Commands.PushPop;
using AssemblerLib.Grammar_Rules.Tokens;
using FluentAssertions;

namespace AssemblerTests.Tokens.Operations
{
    [TestClass]
    public class PushPopTokenShould
    {
        [TestMethod]
        public void EncodePushCorrectly()
        {
            var i = 0b1110_010_1_0_0_1_0_1101_0111_0000_0000_0100;
            var expectedBytes = i.ToByteArray();
            expectedBytes.Reverse();
            var sut = new PushToken(Condition.AL, new RegisterToken("R7"));
            sut.Encode().Should().ContainInOrder(expectedBytes);
        }

        [TestMethod]
        public void EncodePopCorrectly()
        {
            var i = 0b1110_010_0_1_0_0_1_1101_1001_0000_0000_0100;
            var bytes = i.ToByteArray();
            bytes.Reverse();
            var sut = new PopToken(Condition.AL, new RegisterToken("R9"));
            sut.Encode().Should().ContainInOrder(bytes);
        }
    }
}
