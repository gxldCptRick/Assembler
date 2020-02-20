using System.Collections.Generic;
using System.Linq;
using AssemblerLib.Compiler.CompilationTokens.BoostedTokens;
using static AssemblerLib.Compiler.CompilationTokens.BoostedTokens.CompliationExtensions;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Compiler.CompilationTokens.Tokens
{
    public class ExpressionAddTerm : Expression
    {
        public override string Content => $"{LeftOperand} + {RightOperand}";
        public override NumericToken Value { get; }

        private Expression LeftOperand { get; }
        private Term RightOperand { get; }

        public ExpressionAddTerm(Expression leftOperand, Term rightOperand)
        {
            LeftOperand = leftOperand;
            RightOperand = rightOperand;
            Value = new RawNumericToken(leftOperand.Value + rightOperand.Value);
        }

        public override IEnumerable<IToken> Assemble()
        {
            return LeftOperand.Assemble()
                .Concat(RightOperand.Assemble())
                .Concat(new IToken[] {
                    Pop(_defaultValueRegister),
                    Pop(_defaultSecondValueRegister),
                    Add(),
                    Push(_defaultValueRegister)
                });
        }
    }
}
