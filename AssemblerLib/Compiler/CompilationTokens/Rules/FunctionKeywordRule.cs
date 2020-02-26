using AssemblerLib.Compiler.CompilationTokens.Tokens;
using AssemblerLib.Grammar_Rules;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Compiler.CompilationTokens.Rules
{
    public class FunctionKeywordRule : IGrammerRule
    {
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var nextStack = new Stack<IToken>();
            foreach (var token in currentStack)
            {
                nextStack.Push(token is AlphaNumeric an && an == "FUNCTION" ? new FunctionKeyWord(): token);
            }
            return new Stack<IToken>(nextStack);
        }
    }
}
