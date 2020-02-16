using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Compiler.CompilationTokens.Tokens
{
    public class FactorTerm : Term
    {
        public override string Content => Factor.Content;
        private Factor Factor { get; }

        public override NumericToken Value => Factor.Value;

        public FactorTerm(Factor factor)
        {
            Factor = factor;
        }


    }
}
