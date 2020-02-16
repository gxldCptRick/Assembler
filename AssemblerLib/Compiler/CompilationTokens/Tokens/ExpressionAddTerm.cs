using System;
using System.Collections.Generic;
using System.Text;
using AssemblerLib.Compiler.CompilationTokens.BoostedTokens;
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
    }
}
