using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Compiler.CompilationTokens.Tokens
{
    public abstract class Factor : ICompilationToken
    {
        public abstract string Content { get; }
        public abstract NumericToken Value { get; }
    }
}
