using AssemblerLib.Compiler.CompilationTokens.Tokens;
using AssemblerLib.Grammar_Rules;
using AssemblerLib.Tokenizer.Tokens;
using System.Collections.Generic;
using System.Diagnostics;

namespace AssemblerLib.Compiler.CompilationTokens.Rules
{
    [DebuggerDisplay("E := E + T")]
    public class ExpressionAddTermRule : IGrammerRule, IConditionalRule
    {
        public Stack<IToken> ConditionallyReduceStack(Stack<IToken> currentStack, IToken nextToken)
        {
            return (nextToken is SpecialChars sc && sc == "*" )||
                currentStack.Contains(new SpecialChars("*")) ?
                currentStack:
                ReduceStack(currentStack);
        }

        // E := E + T
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var s = currentStack.ToArray();
            var nextStack = new Stack<IToken>();
            var i = 0;
            for (; i + 2 < s.Length; i++)
            {
                if (s[i] is Term rightOperand &&
                    s[i + 1] is SpecialChars op && op == "+" &&
                    s[i + 2] is Expression leftOperand)
                {
                    i += 2;
                    nextStack.Push(new ExpressionAddTerm(leftOperand, rightOperand));
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
