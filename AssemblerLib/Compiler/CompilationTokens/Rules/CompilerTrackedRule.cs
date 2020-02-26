using AssemblerLib.Compiler.CompilationTokens.Meta;
using AssemblerLib.Compiler.CompilationTokens.Tokens;
using AssemblerLib.Grammar_Rules;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Compiler.CompilationTokens.Rules
{
    public class CompilerTrackedRule : IGrammerRule, IConditionalRule
    {
        private SymbolTable Symbols { get;  }
        public CompilerTrackedRule(SymbolTable symbols)
        {
            Symbols = symbols;
        }
        public Stack<IToken> ConditionallyReduceStack(Stack<IToken> currentStack, IToken nextToken)
        {
            return ReduceStack(currentStack);
        }

        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var nextStack = new Stack<IToken>();
            foreach (var token in currentStack)
            {
                if(token is AlphaNumeric an)
                {
                    nextStack.Push(Symbols.Resolve(an));
                }
                else
                {
                    nextStack.Push(token);
                }
            }
            return new Stack<IToken>(nextStack);
        }
    }
}
