using AssemblerLib.Commands.MoveTopBottom;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using AssemblerLib.Commands;
using FluentAssertions;

namespace AssemblerTests.Tokens.Operations
{
    [TestClass]
    public class MoveTopWordTokenShould
    {
        [TestMethod]
        public void EncodeItCorrectly()
        {
            var expectedBits = 0b1110_0011_0000_1111_0111_0000_0000_0000;
            var expectedBytes = expectedBits.ToByteArray();
            expectedBytes.Reverse();
            var sut = new MoveWordToken(Condition.AL, new RegisterToken(new AlphaNumeric("R7")), new HexToken("0xf000"));
            sut.Encode().Should().ContainInOrder(expectedBytes);
            
        }
    }
}
