using AssemblerLib.Compiler.CompilationTokens.Tokens;
using AssemblerLib.Grammar_Rules;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Compiler.CompilationTokens.Rules
{
    public class FactorAsTermRule : IGrammerRule, IConditionalRule
    {
        public Stack<IToken> ConditionallyReduceStack(Stack<IToken> currentStack, IToken nextToken)
        {
            return (nextToken is SpecialChars leadingStar && leadingStar != null && leadingStar == "*") ||
                (currentStack.Peek() is SpecialChars star && star == "*" && nextToken is Factor && nextToken != null) ?
                currentStack:
                ReduceStack(currentStack);
        }

        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var nextStack = new Stack<IToken>();
            foreach (var token in currentStack)
            {
                if(token is Factor f)
                {
                    nextStack.Push(new FactorTerm(f));
                }else
                {
                    nextStack.Push(token);
                }
            }
            return new Stack<IToken>(nextStack);
        }
    }
}
