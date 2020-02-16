using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Compiler.CompilationTokens.Tokens
{
    public abstract class Term : ICompilationToken
    {
        public abstract string Content { get; }
        public abstract NumericToken Value { get; }
    }
}
