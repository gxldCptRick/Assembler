using AssemblerLib.Compiler.CompilationTokens.Tokens;
using AssemblerLib.Grammar_Rules;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Compiler.CompilationTokens.Rules
{
    class FunctionFactorRule : IConditionalRule, IGrammerRule

    {
        public Stack<IToken> ConditionallyReduceStack(Stack<IToken> currentStack, IToken nextToken)
        {
            return nextToken is SpecialChars sc && sc == "{" ? currentStack : ReduceStack(currentStack); 
        }

        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var s = currentStack.ToArray();
            var nextStack = new Stack<IToken>();
            var i = 0;
            for(; i + 1 < s.Length; i++)
            {
                if(s[i] is Function fn && 
                    !(s[i + 1] is FunctionKeyWord key))
                {
                    i += 1;
                    nextStack.Push(new FunctionFactor(fn));
                    nextStack.Push(s[i]);
                }
                else
                {
                    nextStack.Push(s[i]);
                }
            }
            for(; i < s.Length; i++) { nextStack.Push(s[i] is Function fn ? new FunctionFactor(fn): s[i]); }
            return new Stack<IToken>(nextStack);
        }
    }
}
