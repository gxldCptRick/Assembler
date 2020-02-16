using AssemblerLib.Compiler.CompilationTokens.Tokens;
using AssemblerLib.Grammar_Rules;
using AssemblerLib.Tokenizer.Tokens;
using System.Collections.Generic;
using System.Diagnostics;

namespace AssemblerLib.Compiler.CompilationTokens.Rules
{
    [DebuggerDisplay("T := F")]
    public class TermFactorRule : IGrammerRule, IConditionalRule
    {
        public Stack<IToken> ConditionallyReduceStack(Stack<IToken> currentStack, IToken nextToken)
        {
            return ReduceStack(currentStack);
        }

        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var nextStack = new Stack<IToken>();
            foreach (var token in currentStack)
            {
                if (token is Factor f)
                {
                    nextStack.Push(new FactorTerm(f));
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
