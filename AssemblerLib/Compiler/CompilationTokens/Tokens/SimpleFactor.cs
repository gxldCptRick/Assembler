using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Compiler.CompilationTokens.Tokens
{
    public class SimpleFactor : Factor
    {
        public override string Content => Value.Content;

        public override NumericToken Value { get; }

        public SimpleFactor(NumericToken value)
        {
            Value = value;
        }
    }
}
