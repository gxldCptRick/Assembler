using AssemblerLib.Compiler.CompilationTokens.Tokens;
using AssemblerLib.Exceptions;
using AssemblerLib.Grammar_Rules;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Compiler.CompilationTokens.Rules
{
    public class NestedExpressionRule : IGrammerRule
    {
        // EXP := EXP + EXP
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            if(currentStack.Count >=  3)
            {
                var copy = new Stack<IToken>(new Stack<IToken>(currentStack));
                var left = copy.Pop() as Expression;
                var operation = copy.Pop() as SpecialChars;
                var right = copy.Pop() as Expression;
                if (operation is null) throw new InvalidStack(currentStack, $"The stack must contain {nameof(Expression)} + {nameof(Expression)} in that order if it is greater than 3");
                return new Stack<IToken>(new IToken[] { new ExpressionNested(left, right) });
            }
            return currentStack;
        }
    }
}
