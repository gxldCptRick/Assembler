using System.Collections.Generic;
using System.Linq;
using AssemblerLib.Compiler.CompilationTokens.BoostedTokens;
using static AssemblerLib.Compiler.CompilationTokens.BoostedTokens.CompliationExtensions;
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

        public override IEnumerable<IToken> Assemble()
        {
            return NestedTerm.Assemble()
                .Concat(Factor.Assemble())
                .Concat(new IToken[] {
                    Pop(_defaultValueRegister),
                    Pop(_defaultSecondValueRegister),
                    Mulitply(),
                    Push()
                });
        }
    }
}
