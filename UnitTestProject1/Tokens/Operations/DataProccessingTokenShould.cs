using AssemblerLib.Commands;
using AssemblerLib.Commands.DataProcessing;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AssemblerTests.Tokens.Operations
{
    [TestClass]
    public class DataProccessingTokenShould
    {
        [TestMethod]
        public void MakeSureImmediateValueEncodesCorrectly()
        {
            // 0xe2 94 20 08 # ADD R2, R4, 0x08
            var command = 0xe2_94_20_08;
            var expectedOutput = command.ToByteArray();
            expectedOutput.Reverse();
            var sut = new DataProccessImmediateToken(
                Condition.AL,
                OperationCode.ADD,
                new RegisterToken(new AlphaNumeric("R4")),
                new RegisterToken(new AlphaNumeric("R2")),
                new HexToken("0x08"));
            var actualOutput = sut.Encode();
            actualOutput
                .Should()
                .ContainInOrder(expectedOutput,
                        because: "That is the correct translation of this command");
        }

    }
}
