using System;
using System.Collections.Generic;
using System.Text;
using AssemblerLib.Compiler.CompilationTokens.BoostedTokens;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Compiler.CompilationTokens.Tokens
{
    class FunctionFactor : Factor
    {
        public override string Content => $"{FunctionCalled.Name}";
        public Function FunctionCalled { get; set; }
        public override NumericToken Value => new RawNumericToken(0);

        public FunctionFactor(Function functionCalled)
        {
            FunctionCalled = functionCalled;
        }
        public override IEnumerable<IToken> Assemble()
        {
            return FunctionCalled.ResolveValue();
        }
    }
}
