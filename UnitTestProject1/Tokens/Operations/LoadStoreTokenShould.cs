using AssemblerLib.Commands;
using AssemblerLib.Commands.LoadStore;
using AssemblerLib.Compiler.CompilationTokens.BoostedTokens;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AssemblerTests.Tokens.Operations
{
    [TestClass]
    public class LoadStoreTokenShould
    {
        [TestMethod]
        public void EncodeImmediateTokenCorrectly()
        {
            var expectedBits = 0b1110_0111_1001_0100_0101_0000_0000_0000;
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
        [TestMethod]
        public void EncodeWriteBackImmediateTokenCorrectly()
        {
            var expectedBits = 0xE6_AD_70_04;
            var expected = expectedBits.ToByteArray();
            expected.Reverse();
            var sut = new LoadStoreWritebackImmediateToken(
                LoadStoreSelection.STR,
                Condition.AL,
                new RegisterToken("R13"),
                new RegisterToken("R7"),
                new SignedValueToken(new RawNumericToken(4)));
            var encoding = sut.Encode();
            encoding.Should().ContainInOrder(expected);

        }

        [TestMethod]
        public void EncodeWriteBackImmediateTokenLoadOpCorrrectly()
        {
            var expectedBits = 0xE7_3D_90_04;
            var expected = expectedBits.ToByteArray();
            expected.Reverse();
            var sut = new LoadStoreWritebackImmediateToken(
                LoadStoreSelection.LDR,
                Condition.AL,
                new RegisterToken("R13"),
                new RegisterToken("R9"),
                new SignedValueToken(new RawNumericToken(4), isNegative: true)
            );
            var encoding = sut.Encode();
            encoding.Should().ContainInOrder(expected);
        }
    }
}
