using AssemblerLib.Compiler.CompilationTokens.Statements;
using AssemblerLib.Compiler.CompilationTokens.Tokens;
using AssemblerLib.Grammar_Rules;
using AssemblerLib.Tokenizer.Tokens;
using System.Collections.Generic;

namespace AssemblerLib.Compiler.CompilationTokens.Rules
{
    public class ConstantRule : IGrammerRule
    {
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var s = currentStack.ToArray();
            var nextStack = new Stack<IToken>();
            var i = 0;
            for (; i < s.Length; i++)
            {
                if (i + 1 < s.Length)
                {
                    if (s[i] is SpecialChars sc && sc == ";" &&
                        s[i + 1] is Expression properValue)
                    {
                        i += 1;
                        nextStack.Push(new ConstantStatement(properValue));
                    }
                    else if (s[i] is Expression value)
                    {
                        nextStack.Push(new ConstantStatement(value));
                    }
                    else
                    {
                        nextStack.Push(s[i]);
                    }
                }
                else
                {
                    if (s[i] is Expression nt)
                    {
                        nextStack.Push(new ConstantStatement(nt));
                    }
                    else
                    {
                        nextStack.Push(s[i]);
                    }
                }
            }

            return new Stack<IToken>(nextStack);
        }
    }
}
