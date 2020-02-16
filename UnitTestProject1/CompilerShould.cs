using AssemblerLib.Compiler;
using AssemblerLib.Parser;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssemblerTests
{
    [TestClass]
    public class CompilerShould
    {
        private static AssemblyParser Assembler { get; set; }
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
        [TestMethod]
        public void CompileASimpleMultiplication()
        {
            var input = "2 * 1";
            var expected = Assembler.ParseAssembly("MOVW R7, 2 MOVT R7, 0 PUSH R7");
            var sut = new Compiler((actual) => actual.Should().Be(expected));
            sut.Compile(input);
        }

        [TestMethod]
        public void CompileWrappedAddition()
        {
            var input = "(1 + 1)";
            var expected = Assembler.ParseAssembly("MOVW R7, 2 MOVT R7, 0 PUSH R7");
            var sut = new Compiler((actual) => actual.Should().Be(expected));
            sut.Compile(input);
        }
    }
}
