using System;
using System.Collections.Generic;
using System.Text;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Compiler.CompilationTokens.Tokens
{
    public class ExpressionConstantValue : Expression
    {
        public override string Content => Value.Content;
        public override NumericToken Value { get; }

        public ExpressionConstantValue(NumericToken value)
        {
            Value = value;
        }
    }
}
