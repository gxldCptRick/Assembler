using AssemblerLib.Tokenizer.Tokens;
using System.Collections.Generic;
using System.Diagnostics;

namespace AssemblerLib.Compiler.CompilationTokens.Tokens
{
    public class FactorTerm : Term
    {
        public override string Content => Factor.Content;
        public Factor Factor { get; }

        public override NumericToken Value => Factor.Value;

        public FactorTerm(Factor factor)
        {
            Factor = factor;
        }

        public override IEnumerable<IToken> Assemble()
        {
            return Factor.Assemble();
        }

        public override string ToString()
        {
            return Content;
        }

    }
}
