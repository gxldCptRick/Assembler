using AssemblerLib.Compiler.CompilationTokens.Tokens;
using AssemblerLib.Grammar_Rules;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Compiler.CompilationTokens.Rules
{
    public class ExpressTermRule : IGrammerRule, IConditionalRule
    {
        public Stack<IToken> ConditionallyReduceStack(Stack<IToken> currentStack, IToken nextToken)
        {
            return (nextToken is SpecialChars leadingPlus  && leadingPlus != null && leadingPlus == "+") || 
                (currentStack.Peek() is SpecialChars plus && plus == "+" && nextToken is Term t && t != null)? 
                currentStack: 
                ReduceStack(currentStack); 
        }

        // EXP := {NumericValue}
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var nextStack = new Stack<IToken>();
            foreach (var token in currentStack)
            {
                if(token is Term n)
                {
                    nextStack.Push(new ExpressionTerm(n));
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
