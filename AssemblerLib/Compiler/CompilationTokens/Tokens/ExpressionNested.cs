using System;
using System.Collections.Generic;
using System.Text;
using AssemblerLib.Compiler.CompilationTokens.BoostedTokens;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Compiler.CompilationTokens.Tokens
{
    public class ExpressionNested : Expression
    {
        public override string Content => $"({LeftOperand}) + ({RightOperand})";
        public override NumericToken Value { get; }

        private Expression LeftOperand { get; }
        private Expression RightOperand { get; }

        public ExpressionNested(Expression leftOperand, Expression rightOperand)
        {
            LeftOperand = leftOperand;
            RightOperand = rightOperand;
            Value = new RawNumericToken(leftOperand.Value + rightOperand.Value);
        }
    }
}
