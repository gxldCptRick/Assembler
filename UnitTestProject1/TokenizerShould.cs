using AssemblerLib.Tokenizer;
using AssemblerLib.Tokenizer.Tokens;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssemblerTests
{
    [TestClass]
    public class TokenizerShould
    {
        [TestMethod]
        public void ParseAlphaTokensCorrectly()
        {
            var input = "Alpah string";
            var sut = new Tokenizer();
            var output = sut.Tokenize(input);
            output
                .Should()
                .HaveCount(2)
                .And
                .ContainInOrder(new AlphaNumeric("Alpah"), new AlphaNumeric("string"));
        }

        [TestMethod]
        public void ParseDecimalTokens()
        {
            var input = "10 1001";
            var sut = new Tokenizer();
            var output = sut.Tokenize(input);
            output
                .Should()
                .HaveCount(2)
                .And
                .ContainInOrder(new DecimalToken("10"), new DecimalToken("1001"));
        }

        [TestMethod]
        public void ParseHexTokens()
        {
            var input = "0xFF11 0xABCDEF";
            var sut = new Tokenizer();
            var output = sut.Tokenize(input);
            output
                .Should()
                .HaveCount(2)
                .And
                .ContainInOrder(new HexToken("0xFF11"), new HexToken("0xABCDEF"));
        }

        [TestMethod]
        public void ParseSingleLineComment()
        {
            var input = "// this is a single line comment\n //this is not a another single line comment";
            var sut = new Tokenizer();
            var content = sut.Tokenize(input);
            content
                .Should()
                .HaveCount(2)
                .And
                .ContainInOrder(
                new CommentToken("//this is a single line comment"),
                new CommentToken("//this is not a another single line comment")
                );
        }

        [TestMethod]
        public void ParseMultiLineComments()
        {
            var input = "/* you are the one being racist */ /**/";
            var sut = new Tokenizer();
            var content = sut.Tokenize(input);
            content
                .Should()
                .HaveCount(2)
                .And
                .ContainInOrder(
                new CommentToken("/* you are the one being racist */"),
                new CommentToken("/**/")
                );
        }

        [TestMethod]
        public void ParseDifferentCommentsAtTheSameTime()
        {
            var input = "/* Shaggy man */ //I am squanching over here";
            var sut = new Tokenizer();
            var output = sut.Tokenize(input);
            output
                .Should()
                .HaveCount(2)
                .And
                .ContainInOrder(
                new CommentToken("/* Shaggy man */"),
                new CommentToken("//I am squanching over here")
                );
        }

        [TestMethod]
        public void ParseOutMultipleKindsOfNumerics()
        {
            var input = "900 0x312";
            var sut = new Tokenizer();
            var output = sut.Tokenize(input);
            output.Should()
                .HaveCount(2)
                .And
                .ContainInOrder(
                new DecimalToken("900"),
                new HexToken("0x312")
                );
        }

        [TestMethod]
        public void ParseAllTokensInStringCorrectly()
        {
            var input = "Alpha 900, 0x33 //rest off this thing\n/* Hello World*/";
            var sut = new Tokenizer();
            var output = sut.Tokenize(input);
            output.Should()
                .HaveCount(6)
                .And
                .ContainInOrder(
                new AlphaNumeric("Alpha"),
                new DecimalToken("900"),
                new SpecialChars(","),
                new HexToken("0x33"),
                new CommentToken("//rest off this thing"),
                new CommentToken("/* Hello World*/")
                );
        }

        [TestMethod]
        public void ParseSpecialCharacters()
        {
            var input = ", (:)";
            var sut = new Tokenizer();
            var output = sut.Tokenize(input);
            output.Should()
                .HaveCount(4)
                .And
                .ContainInOrder(
                new SpecialChars(","),
                new SpecialChars("("),
                new SpecialChars(":"),
                new SpecialChars(")")
                ); ;
        }

        [TestMethod]
        public void TokenizeASimpleCommandWithLabel()
        {
            var input = "omega: ADD R1, R2, R3";
            var sut = new Tokenizer();
            var output = sut.Tokenize(input);
            output.Should()
                .HaveCount(8)
                .And
                .ContainInOrder(
                new AlphaNumeric("omega"),
                new SpecialChars(":"),
                new AlphaNumeric("ADD"),
                new AlphaNumeric("R1"),
                new SpecialChars(","),
                new AlphaNumeric("R2"),
                new SpecialChars(","),
                new AlphaNumeric("R3")
                );
        }
    }
}
