using System;
using System.Collections.Generic;
using System.Text;
using AssemblerLib.Commands.MoveTopBottom;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Grammar_Rules.Substitution
{
    public class MoveTopBottomRule : IGrammerRule
    {
        private MoveTopBottomBuilder builder = new MoveTopBottomBuilder();
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var s = currentStack.ToArray();
            var nextStack = new Stack<IToken>();
            int i = 0;
            for (; i + 2 < s.Length; i++)
            {
                if (s[i] is NumericToken value &&
                    s[i + 1] is RegisterToken destination &&
                    s[i + 2] is AlphaNumeric an && (an.Content.StartsWith("MOVT") || an.Content.StartsWith("MOVW")))
                {
                    i += 2;
                    nextStack.Push(builder.FromRawTokens(an, destination, value));
                }
                else
                {
                    nextStack.Push(s[i]);
                }
            }
            for(;i < s.Length; i++)
            {
                nextStack.Push(s[i]);
            }

            return new Stack<IToken>(nextStack);
        }
    }
}
