using AssemblerLib.Compiler.CompilationTokens.Tokens;
using AssemblerLib.Grammar_Rules;
using AssemblerLib.Tokenizer.Tokens;
using System.Collections.Generic;
using System.Diagnostics;

namespace AssemblerLib.Compiler.CompilationTokens.Rules
{
    /// <summary>
    /// Expresion := Term
    /// </summary>
    [DebuggerDisplay("E := T")]
    public class ExpressionTermRule : IGrammerRule, IConditionalRule
    {
        public Stack<IToken> ConditionallyReduceStack(Stack<IToken> currentStack, IToken nextToken)
        {
            return (nextToken is SpecialChars leadingPlus && leadingPlus != null && (leadingPlus == "*")) ||
                currentStack.Contains(new SpecialChars("*")) ?
                currentStack:
                ReduceStack(currentStack);
        }
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var nextStack = new Stack<IToken>();
            foreach (var token in currentStack)
            {
                if (token is Term n)
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
        /// <summary>
        /// E := T
        /// </summary>
        public ExpressionTermRule()
        {

        }
    }
}
