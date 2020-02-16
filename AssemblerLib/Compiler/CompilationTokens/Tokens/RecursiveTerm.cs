using System;
using System.Collections.Generic;
using System.Text;
using AssemblerLib.Compiler.CompilationTokens.BoostedTokens;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Compiler.CompilationTokens.Tokens
{
    public class RecursiveTerm : Term
    {
        public override string Content => $"{NestedTerm.Content} * {Factor.Content}";
        private Term NestedTerm { get; }
        private Factor Factor { get; }

        public override NumericToken Value { get; }

        public RecursiveTerm(Term nested, Factor factor)
        {
            NestedTerm = nested;
            Factor = factor;
            Value = new RawNumericToken(NestedTerm.Value * Factor.Value);
        }
    }
}
