using AssemblerLib.Compiler.CompilationTokens.Tokens;
using AssemblerLib.Grammar_Rules;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Compiler.CompilationTokens.Rules
{
    public class ReplacementExpressionRule : IGrammerRule
    {
        // EXP := {NumericValue}
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var nextStack = new Stack<IToken>();
            foreach (var token in currentStack)
            {
                if(token is NumericToken n)
                {
                    nextStack.Push(new ExpressionConstantValue(n));
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
