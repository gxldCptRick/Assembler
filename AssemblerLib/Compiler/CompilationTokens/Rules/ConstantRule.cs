using AssemblerLib.Compiler.CompilationTokens.Statements;
using AssemblerLib.Grammar_Rules;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Compiler.CompilationTokens.Rules
{
    public class ConstantRule : IGrammerRule
    {
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var s = currentStack.ToArray();
            var nextStack = new Stack<IToken>();
            int i = 0;
            for (; i < s.Length; i++)
            {
                if (i + 1 < s.Length)
                {
                    if (s[i] is SpecialChars sc && sc == ";" &&
                        s[i + 1] is NumericToken nt)
                    {
                        i += 1;
                        nextStack.Push(new ConstantStatement(nt));
                    }
                    else
                    {
                        nextStack.Push(s[i]);
                    }
                }
                else
                {
                    if (s[i] is NumericToken nt)
                    {
                        nextStack.Push(new ConstantStatement(nt));
                    }
                    else
                    {
                        nextStack.Push(s[i]);
                    }
                }

            }
            for (; i < s.Length; i++)
            {
                nextStack.Push(s[i]);
            }

            return new Stack<IToken>(nextStack);
        }
    }
}
