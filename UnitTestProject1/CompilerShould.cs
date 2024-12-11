using AssemblerLib.Compiler;
using AssemblerLib.Compiler.CompilationTokens.BoostedTokens;
using AssemblerLib.Compiler.CompilationTokens.Meta;
using AssemblerLib.Compiler.CompilationTokens.Tokens;
using AssemblerLib.Exceptions;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Parser;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace AssemblerTests
{
    [TestClass]
    public class CompilerShould
    {
        private static AssemblyParser Assembler { get; set; }

        private readonly SymbolTable table = new SymbolTable(0xFB0);

        private string PushConstant(int value)
        {
            return $"MOVW R7, {value} MOVT R7, 0 PUSH R7";
        }

        private string LoadValues()
        {
            return "POP R7 POP R8";
        }

        private string AddValues()
        {
            return $"{LoadValues()} ADD R7, R7, R8, 0 PUSH R7";
        }

        private string MultiplyValues()
        {
            return $"{LoadValues()} MUL R7, R7, R8 PUSH R7";
        }

        private string LoadVariable(string variableName)
        {
            int addr = (table.ResolveVariable(variableName) as Variable).Address;
            return $"MOVW R6, {addr & 0xFFFF} MOVT R6, {(addr >> 16) & 0xFFFF} LDRI R6, R6, 0 PUSH R6";
        }

        private string StoreVariable(string variableName)
        {
            int addr = (table.ResolveVariable(variableName) as Variable).Address;
            return $"MOVW R6, {addr & 0xFFFF} MOVT R6, {(addr >> 16) & 0xFFFF} POP R7 STRI R7, R6, 0 {LoadVariable(variableName)}";
        }

        private string DefineFunction(string label, int statements, params string[] instructions)
        {
            var instructionsCommand = ConcatCommand(instructions);
            var clearString = ConcatCommand(Enumerable.Range(1, statements).Select(c => Pop("R8")).ToArray());
            return ConcatCommand($"f: {instructionsCommand} {clearString} PUSH R7", "BX R14");
        }

        private string Pop(string register)
        {
            return $"POP {register}";
        }
        private string CallFunction(string name)
        {
            return $"BL {name}";
        }

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
            var expected = Assembler.ParseAssembly(PushConstant(2));
            var sut = new Compiler((actual) => actual.Assemble().Should().Be(expected));
            sut.Compile(input);
        }

        [TestMethod]
        public void CompileASingleExpressionCorrectly()
        {
            var input = "2 + 2";
            var expected = Assembler
                .ParseAssembly($"{PushConstant(2)} {PushConstant(2)} {AddValues()}");
            var sut = new Compiler((actual) => actual.Assemble().Should().Be(expected));
            sut.Compile(input);
        }

        [TestMethod]
        public void CompileMultipleExpressionsCorrectly()
        {
            var input = "2 + 2\n 3 + 5";
            var expected = Assembler.ParseAssembly(
                ConcatCommand(
                    PushConstant(2),
                    PushConstant(2),
                    AddValues(),
                    PushConstant(3),
                    PushConstant(5),
                    AddValues()
                    )
                );
            var sut = new Compiler((actual) => actual.Assemble().Should().Be(expected));
            sut.Compile(input);
        }

        private string ConcatCommand(params string[] commands)
        {
            return commands.Aggregate(string.Empty, (agg, next) => agg + " " + next).Trim();
        }

        [TestMethod]
        public void CompileMultipleAdditionChain()
        {
            var input = "2 + 2 + 3 + 5";
            var expected = Assembler.ParseAssembly(
                ConcatCommand(
                    PushConstant(2),
                    PushConstant(2),
                    AddValues(),
                    PushConstant(3),
                    AddValues(),
                    PushConstant(5),
                    AddValues())
                );
            var sut = new Compiler((actual) => actual.Assemble().Should().Be(expected));
            sut.Compile(input);
        }

        [TestMethod]
        public void CompileASimpleMultiplication()
        {
            var input = "2 * 1";
            var expected = Assembler.ParseAssembly(
                ConcatCommand(
                    PushConstant(2),
                    PushConstant(1),
                    MultiplyValues())
                );
            var sut = new Compiler((actual) => actual.Assemble().Should().Be(expected));
            sut.Compile(input);
        }

        [TestMethod]
        public void CompileAdditionAndMulitplication()
        {
            var input = "3 + 1 * 2";
            var expected = Assembler.ParseAssembly(
                ConcatCommand(
                    PushConstant(3),
                    PushConstant(1),
                    PushConstant(2),
                    MultiplyValues(),
                    AddValues())
                );
            var sut = new Compiler((actual) => actual.Assemble().Should().Be(expected));
            sut.Compile(input);
        }

        [TestMethod]
        public void CompileMulitplicationFollowedByAddition()
        {
            var input = "2 * 1 + 1";
            var expected = Assembler.ParseAssembly(
                ConcatCommand(
                    PushConstant(2),
                    PushConstant(1),
                    MultiplyValues(),
                    PushConstant(1),
                    AddValues())
                );
            var sut = new Compiler((actual) => actual.Assemble().Should().Be(expected));
            sut.Compile(input);
        }


        [TestMethod]
        public void CompileWithALotOfMultipleMultiplyingAndAdding()
        {
            var input = "2 * 2 * 3 + 1 + 4";
            var expected = Assembler.ParseAssembly(
                ConcatCommand(
                    PushConstant(2),
                    PushConstant(2),
                    MultiplyValues(),
                    PushConstant(3),
                    MultiplyValues(),
                    PushConstant(1),
                    AddValues(),
                    PushConstant(4),
                    AddValues())
                );
            var sut = new Compiler((actual) => actual.Assemble().Should().Be(expected));
            sut.Compile(input);
        }

        [TestMethod]
        public void CompileWithALotOfMultipleMultiplyingAndAddingMixed()
        {
            var input = "2 * 3 + 5 * 4";
            var expected = Assembler.ParseAssembly(
                ConcatCommand(
                PushConstant(2),
                PushConstant(3),
                MultiplyValues(),
                PushConstant(5),
                PushConstant(4),
                MultiplyValues(),
                AddValues())
            );
            var sut = new Compiler((actual) => actual.Assemble().Should().Be(expected));
            sut.Compile(input);
        }

        [TestMethod]
        public void CompileWrappedAddition()
        {
            var input = "(1 + 1)";
            var expected = Assembler.ParseAssembly(
                ConcatCommand(
                    PushConstant(1),
                    PushConstant(1),
                    AddValues()));
            var sut = new Compiler((actual) => actual.Assemble().Should().Be(expected));
            sut.Compile(input);
        }

        [TestMethod]
        public void CompileWrappedMultiplication()
        {
            var input = "(2 * 1)";
            var expected = Assembler.ParseAssembly(
                ConcatCommand(
                    PushConstant(2),
                    PushConstant(1),
                    MultiplyValues())
                );
            var sut = new Compiler((actual) => actual.Assemble().Should().Be(expected));
            sut.Compile(input);
        }

        [TestMethod]
        public void CompileWrappedExpressionTimesFactor()
        {
            var input = "(1 + 1) * 1";
            var expected = Assembler.ParseAssembly(
                ConcatCommand(
                    PushConstant(1),
                    PushConstant(1),
                    AddValues(),
                    PushConstant(1),
                    MultiplyValues())
                );
            var sut = new Compiler((actual) => actual.Assemble().Should().Be(expected));
            sut.Compile(input);
        }

        [TestMethod]
        public void CompileDoublyWrappedConstant()
        {
            var input = "((2))";
            var expected = Assembler.ParseAssembly(PushConstant(2));
            var sut = new Compiler((actual) => actual.Assemble().Should().Be(expected));
            sut.Compile(input);
        }

        [TestMethod]
        public void CompileWrappedConstant()
        {
            var input = "(2)";
            var expected = Assembler.ParseAssembly(PushConstant(2));
            var sut = new Compiler((actual) => actual.Assemble().Should().Be(expected));
            sut.Compile(input);
        }

        [TestMethod]
        public void CompileComplexExpression()
        {
            var input = "((2 + 1) * 2) + 3 * 4";
            var expected = Assembler.ParseAssembly(
                ConcatCommand(
                    PushConstant(2),
                    PushConstant(1),
                    AddValues(),
                    PushConstant(2),
                    MultiplyValues(),
                    PushConstant(3),
                    PushConstant(4),
                    MultiplyValues(),
                    AddValues())
                );
            var sut = new Compiler((actual) => actual.Assemble().Should().Be(expected));
            sut.Compile(input);
        }

        [TestMethod]
        public void CompileNastyExpression()
        {
            var input = "((2 + 1) * 3 + 4) + 9 * (3 + 4)";
            var expected = Assembler.ParseAssembly(
                ConcatCommand(
                    PushConstant(2),
                    PushConstant(1),
                    AddValues(),
                    PushConstant(3),
                    MultiplyValues(),
                    PushConstant(4),
                    AddValues(),
                    PushConstant(9),
                    PushConstant(3),
                    PushConstant(4),
                    AddValues(),
                    MultiplyValues(),
                    AddValues()
                    )
                );
            var sut = new Compiler((actual) => actual.Assemble().Should().Be(expected));
            sut.Compile(input);
        }

        [TestMethod]
        public void ThrowInvalidStackWhenMissingClosingParen()
        {
            var input = "(2 + 1";
            var sut = new Compiler((actual) => Console.WriteLine(actual));
            Func<ProgramToken> BadInput = () => sut.Compile(input);
            BadInput.Should().Throw<InvalidStack>();
        }

        [TestMethod]
        public void ThrowInvalidStackWhenMissingOpeningParen()
        {
            var input = ")(2 + 1)";
            var sut = new Compiler((actual) => Console.WriteLine(actual));
            Func<ProgramToken> BadInput = () => sut.Compile(input);
            BadInput.Should().Throw<InvalidStack>();
        }


        [TestMethod]
        public void ThrowInvalidStackWhenParensArentClosingEachother()
        {
            var input = ")(";
            var sut = new Compiler((actual) => Console.WriteLine(actual));
            Func<ProgramToken> BadInput = () => sut.Compile(input);
            BadInput.Should().Throw<InvalidStack>();
        }

        [TestMethod]
        public void CompileBasicAssignment()
        {
            var input = "VAR = 1";
            var expected = Assembler.ParseAssembly(
            ConcatCommand(
                PushConstant(1),
                StoreVariable("VAR"))
            );
            var sut = new Compiler((actual) => actual.Assemble().Should().Be(expected), table);
            sut.Compile(input);
        }

        [TestMethod]
        public void CompileAssignmentAndUse()
        {
            var input = "VAR = 2\nVAR + 2";
            var expected = Assembler.ParseAssembly(
                ConcatCommand(
                    PushConstant(2),
                    StoreVariable("VAR"),
                    LoadVariable("VAR"),
                    PushConstant(2),
                    AddValues())
                );
            var sut = new Compiler((actual) => actual.Assemble().Should().Be(expected), table);
            sut.Compile(input);
        }
        [TestMethod]
        public void CompileAssignmentUseReAssignmentCorrectly()
        {
            var input = "VAR = 2 \n VAR + 3\n VAR = 3";
            var expected = Assembler.ParseAssembly(
                ConcatCommand(
                    PushConstant(2),
                    StoreVariable("VAR"),
                    LoadVariable("VAR"),
                    PushConstant(3),
                    AddValues(),
                    PushConstant(3),
                    StoreVariable("VAR"))
                );
            var sut = new Compiler((actual) => actual.Assemble().Should().Be(expected), table);
            sut.Compile(input);
        }

        [TestMethod]
        public void CompileTwoVariables()
        {
            var input = "VAR1 = 2\n VAR2 = 3";
            var expected = Assembler.ParseAssembly(
                ConcatCommand(
                    PushConstant(2),
                    StoreVariable("VAR1"),
                    PushConstant(3),
                    StoreVariable("VAR2"))
                );
            var sut = new Compiler((actual) => actual.Assemble().Should().Be(expected), table);
            sut.Compile(input);
        }

        [TestMethod]
        public void CompileTwoVariablesButOneReferencesTheOther()
        {
            var input = "VAR1 = 3\n VAR2 = VAR1 * 2";
            var expected = Assembler.ParseAssembly(
                ConcatCommand(
                    PushConstant(3),
                    StoreVariable("VAR1"),
                    LoadVariable("VAR1"),
                    PushConstant(2),
                    MultiplyValues(),
                    StoreVariable("VAR2"))
                );
            var sut = new Compiler((actual) => actual.Assemble().Should().Be(expected), table);
            sut.Compile(input);
        }

        [TestMethod]
        public void CompileEmptyFunction()
        {
            var input = "FUNCTION f { }";
            var sut = new Compiler(null);
            sut.Compile(input).Content.Should().Be("f: BX R14");
        }


        [TestMethod]
        public void CompileSingleStatementFunction()
        {
            var input = "FUNCTION f { 2 + 3 }";
            var expectedAssembly = 
                DefineFunction("f",
                1,
                PushConstant(2), 
                PushConstant(3), 
                AddValues());

            var expected = Assembler.ParseAssembly(expectedAssembly);
            var sut = new Compiler(a => a.Assemble().Should().Be(expected));
            sut.Compile(input);
        }

        [TestMethod]
        public void CompileFunctionDefinitionAndCall()
        {
            var input = "FUNCTION f { 2 + 3 } f";
            var expectedAssembly = ConcatCommand(
                CallFunction("f"),
                DefineFunction("f",
                1,
                PushConstant(2),
                PushConstant(3),
                AddValues()));
            var expected = Assembler.ParseAssembly(expectedAssembly);
            var sut = new Compiler(a => a.Assemble().Should().Be(expected));
            sut.Compile(input);
        }

        [TestMethod]
        public void CompileFunctionAndInjectProgram()
        {
            var input = "FUNCTION f { 2 + 3 } f";
            var expectedAssembly = ConcatCommand(
                CallFunction("f"),
                "MOVT R0, 0xFFFF",
                DefineFunction("f", 
                1,
                PushConstant(2),
                PushConstant(3),
                AddValues()));
            var expected = Assembler.ParseAssembly(expectedAssembly);
            var injectedProgramAsm = "MOVT R0, 0xFFFF";
            var injectedProgram = Assembler.ParseAssembly(injectedProgramAsm);
            var sut = new Compiler(null);
            sut.Compile(input, injectedProgram).Should().Be(expected);
        }

        [TestMethod]
        public void CompileFunctionWithMultipileLines()
        {
            var input = "FUNCTION f { 2 + 3  4 + 5}";
            var expectedOutput = DefineFunction("f", 
                2,
                PushConstant(2),
                PushConstant(3),
                AddValues(),
                PushConstant(4),
                PushConstant(5),
                AddValues());
            var sut = new Compiler(null);
            var expected = Assembler.ParseAssembly(expectedOutput);
            sut.Compile(input).Should().Be(expected);
        }

        [TestMethod]
        public void CompileMultipleFunctionsCorrectly()
        {
            var input = ConcatCommand("FUNCTION f { 2 + 3 }", "FUNCTION g { 3 + 3 }");
            var expectedOutput = ConcatCommand(
                DefineFunction("g", 1, PushConstant(3),
                                        PushConstant(3),
                                        AddValues()),
                DefineFunction("f", 1, PushConstant(2),
                                        PushConstant(3),
                                        AddValues()));
            var sut = new Compiler(null);
            var expected = Assembler.ParseAssembly(expectedOutput);
            sut.Compile(input).Should().Be(expected);
        }
    }
}
