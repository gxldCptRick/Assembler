using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using AssemblerLib.Commands;
using AssemblerLib.Commands.LoadStore;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using FluentAssertions;

namespace AssemblerTests.Tokens.Operations
{
    [TestClass]
    public class LoadStoreTokenShould
    {
        [TestMethod]
        public void EncodeImmediateTokenCorrectly()
        {
            uint expectedBits = 0b1110_0111_1001_0100_0101_0000_0000_0000;
            var expected = expectedBits.ToByteArray();
            expected.Reverse();
            var sut = new LoadStoreImmediateToken(
                LoadStoreSelection.LDR,
                Condition.AL,
                new RegisterToken(new AlphaNumeric("R4")),
                new RegisterToken(new AlphaNumeric("R5")),
                new SignedValueToken(new DecimalToken("0"))
                );
            var actual = sut.Encode();
            actual.Should().ContainInOrder(expected);
        }
    }
}
