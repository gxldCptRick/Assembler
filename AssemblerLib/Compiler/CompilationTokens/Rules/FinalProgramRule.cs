using AssemblerLib.Exceptions;
using AssemblerLib.Grammar_Rules;
using AssemblerLib.Tokenizer.Tokens;
using System.Collections.Generic;
using System.Diagnostics;

namespace AssemblerLib.Compiler.CompilationTokens.Rules
{
    [DebuggerDisplay("P := [ S ]")]
    public class FinalProgramRule : IGrammerRule
    {
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var nextStack = new Stack<IToken>();
            var list = new List<IStatement>();
            foreach (var token in currentStack)
            {
                if (token is IStatement statement)
                {
                    list.Add(statement);
                }
                else
                {
                    if (list.Count == 0) throw new InvalidStack(currentStack, "Program should be able to have exactly one item if stack contains tokens");
                    nextStack.Push(new Program(list));
                    list.Clear();
                    nextStack.Push(token);
                }
            }
            if (list.Count > 0)
            {
                nextStack.Push(new Program(list));
            }
            return new Stack<IToken>(nextStack);
        }
    }
}
