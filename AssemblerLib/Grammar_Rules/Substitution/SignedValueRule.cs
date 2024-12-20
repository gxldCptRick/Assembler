﻿using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System.Collections.Generic;

namespace AssemblerLib.Grammar_Rules.Substitution
{
    public class SignedValueRule : IGrammerRule
    {
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var s = currentStack.ToArray();
            var nextStack = new Stack<IToken>();
            var i = 0;
            for (; i + 1 < s.Length; i++)
            {
                if (s[i] is NumericToken nt)
                {
                    if (s[i + 1] is SpecialChars sc && sc == "-")
                    {
                        i++;
                        nextStack.Push(new SignedValueToken(nt, isNegative: true));
                    }
                    else
                    {
                        nextStack.Push(new SignedValueToken(nt));
                    }

                }
                else
                {
                    nextStack.Push(s[i]);
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
