using AssemblerLib.Tokenizer.Tokens;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerTests.Tokens
{
    [TestClass]
    public class CommentTokenShould
    {
        [TestMethod]
        public void ParseEmptyMultilineComment()
        {
            var input = "/**/";
            var sut = new CommentToken(input);
            sut
                .Content
                .Should()
                .BeEmpty();
        }

        [TestMethod]
        public void ParseFilledMultiLineComment()
        {
            var input = "/* Should Contain This */";
            var sut = new CommentToken(input);
            sut
                .Content
                .Should()
                .Be("Should Contain This");
        }

        [TestMethod]
        public void ParseFilledMultilineCommentsWithNewLines()
        {
            var input = "/* Should Contain \n Should Contain*/";
            var sut = new CommentToken(input);
            sut
                .Content
                .Should()
                .Be("Should Contain \n Should Contain");
        }

        [TestMethod]
        public void BreakWithBadFormat()
        {
            var input = "/$$/";
            Action makingCommentWithBadStarting = () => new CommentToken(input);
            makingCommentWithBadStarting
                .Should()
                .Throw<ArgumentException>("The starting part of this string is not valid");
        }

        [TestMethod]
        public void ParseMulitlineCommentWithLeadingWhiteSpace()
        {
            var input = "   /**/";
            var sut = new CommentToken(input);
            sut.Content
                .Should()
                .BeEmpty();
        }

        [TestMethod]
        public void ParseSingleComment()
        {
            var input = "// logo you might like ;)";
            var sut = new CommentToken(input);
            sut.Content
                .Should()
                .Be("logo you might like ;)");
        }
        [TestMethod]
        public void ParseSingleLineWithEndingSlash()
        {
            var input = "// /";
            var sut = new CommentToken(input);
            sut.Content
                .Should()
                .Be("/");
        }
    }
}
