using System.Collections.Generic;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Compiler.CompilationTokens.Tokens
{
    public class WrappedExpressionFactor : Factor
    {
        public override string Content => $"({WrappedExpression.Content})";
        private Expression WrappedExpression { get; }

        public override NumericToken Value => WrappedExpression.Value;

        public WrappedExpressionFactor(Expression wrappedExpression)
        {
            WrappedExpression = wrappedExpression;
        }

        public override IEnumerable<IToken> Assemble()
        {
            return WrappedExpression.Assemble();
        }
    }
}
