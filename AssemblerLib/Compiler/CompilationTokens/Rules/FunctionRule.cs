using AssemblerLib.Compiler.CompilationTokens.Tokens;
using AssemblerLib.Exceptions;
using AssemblerLib.Grammar_Rules;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblerLib.Compiler.CompilationTokens.Rules
{
    public class FunctionRule : IGrammerRule
    {
        private static readonly SpecialChars LeftBracket = new SpecialChars("{");
        private static readonly SpecialChars RightBracket = new SpecialChars("}");
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var stack = currentStack.ToList();
            while (stack.Contains(LeftBracket) || stack.Contains(RightBracket))
            {
                ReduceFunctionChunk(stack);
            }
            stack.Reverse();
            return new Stack<IToken>(stack);

        }


        public void ReduceFunctionChunk(List<IToken> inputStream)
        {
            var indexOfStart = inputStream.IndexOf(LeftBracket);
            var indexOfEnd = inputStream.IndexOf(RightBracket);
            if ((indexOfStart == -1 && indexOfEnd != -1) || (indexOfStart != -1 && indexOfEnd == -1))
            {
                throw new InvalidStack(new Stack<IToken>(inputStream), "curly brace not closed");
            }
            if (indexOfStart + 2 < inputStream.Count)
            {
                if (inputStream[indexOfStart + 2] is FunctionKeyWord &&
                    inputStream[indexOfStart + 1] is Function fn)
                {
                    for (int i = indexOfEnd + 1; i < indexOfStart; i++)
                    {
                        if (inputStream[i] is IStatement s)
                        {
                            fn.Statements.Add(s);

                        }
                        else
                        {
                            throw new InvalidStack(new Stack<IToken>(inputStream), "There can only be statments within a function");
                        }
                    }
                    fn.Statements = fn.Statements.Reverse().ToList();
                    inputStream.RemoveRange(indexOfEnd, fn.Statements.Count + 4);
                    inputStream.Insert(0, fn);
                }
            }
        }
    }
}
