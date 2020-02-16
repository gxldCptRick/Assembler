using AssemblerLib.Compiler.CompilationTokens.Tokens;
using AssemblerLib.Grammar_Rules;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Compiler.CompilationTokens.Rules
{
    public class ExpressionWrappingRule : IGrammerRule, IConditionalRule
    {
        public Stack<IToken> ConditionallyReduceStack(Stack<IToken> currentStack, IToken nextToken)
        {
            return ReduceStack(currentStack);
        }

        // ( EXPRESSION )
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var s = currentStack.ToArray();
            var nextStack = new Stack<IToken>();
            var i = 0;
            for(; i + 2 < s.Length; i++)
            {
                if(s[i] is SpecialChars closing && closing == ")" &&
                    s[i + 1] is Expression wrapped && 
                    s[i + 2] is SpecialChars opening && opening == "(")
                {
                    i += 2;
                    nextStack.Push(new WrappedExpressionFactor(wrapped));
                }
                else
                {
                    nextStack.Push(s[i]);
                }
            }

            for (; i < s.Length; i++) { nextStack.Push(s[i]); }
            return new Stack<IToken>(nextStack);
        }
    }
}
