using AssemblerLib.Compiler;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Parser;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerTests
{
    [TestClass]
    public class CompilerShould
    {
        static AssemblyParser Assembler { get; set; }
        [ClassInitialize]
        public static void InitialSetup(TestContext context) 
        {
            context.WriteLine("Setting up the assembler");
            Assembler = new AssemblyParser();
        }

        [TestMethod]
        public void CompileANumberCorrectly()
        {
            var input = "2";
            var expected = Assembler.ParseAssembly(@"MOVW R7, 2 MOVT R7, 0 PUSH R7");
            var sut = new Compiler((actual) => actual.Should().Be(expected));
            sut.Compile(input);
        }

        [TestMethod]
        public void CompileASingleExpressionCorrectly()
        {
            var input = "2 + 2";
            var expected = Assembler
                .ParseAssembly("MOVW R7, 4 MOVT R7, 0 PUSH R7");
            var sut = new Compiler((actual) => actual.Should().Be(expected));
            sut.Compile(input);
        }
        [TestMethod]
        public void CompileMultipleExpressionsCorrectly()
        {
            var input = "2 + 2\n 3 + 5";
            var expected = Assembler.ParseAssembly("MOVW R7, 4 MOVT R7, 0 PUSH R7 MOVW R7, 8 MOVT R7, 0 PUSH R7");
            var sut = new Compiler((actual) => actual.Should().Be(expected));
            sut.Compile(input);
        }
    }
}
