using AssemblerLib.Compiler.CompilationTokens.Tokens;
using AssemblerLib.Grammar_Rules;
using AssemblerLib.Tokenizer.Tokens;
using System.Collections.Generic;
using System.Diagnostics;

namespace AssemblerLib.Compiler.CompilationTokens.Rules
{
    [DebuggerDisplay("T := T * F")]
    public class TermFactorMultiplicationRule : IGrammerRule, IConditionalRule
    {
        /// <summary>
        /// T := T * F
        /// </summary>
        public TermFactorMultiplicationRule()
        {
        }
        public Stack<IToken> ConditionallyReduceStack(Stack<IToken> currentStack, IToken nextToken)
        {
            return ReduceStack(currentStack);
        }

        // T * F 
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var s = currentStack.ToArray();
            var nextStack = new Stack<IToken>();
            var i = 0;
            for (; i + 2 < s.Length; i++)
            {
                if (s[i] is Factor f &&
                    s[i + 1] is SpecialChars op && op == "*" &&
                    s[i + 2] is Term t)
                {
                    i += 2;
                    nextStack.Push(new RecursiveTerm(t, f));
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
