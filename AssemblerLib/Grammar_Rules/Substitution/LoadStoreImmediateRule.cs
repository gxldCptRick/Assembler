using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using AssemblerLib.Commands.LoadStore;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Grammar_Rules.Substitution
{
    public class LoadStoreImmediateRule : IGrammerRule
    {

        private static readonly Regex _ldrStrRegex = new Regex("(LDR[A-Z]{0,2}I)|(STR[A-Z]{0,2}I)");
        private static readonly LoadStoreImmediateBuilder builder = new LoadStoreImmediateBuilder();
        // LDRAL R5, R6, 0
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var s = currentStack.ToArray();
            var nextStack = new Stack<IToken>();
            int i = 0;

            for (; i + 3 < s.Length; i++)
            {
                if(s[i] is SignedValueToken offset &&
                    s[i + 1] is RegisterToken source &&
                    s[i + 2] is RegisterToken destination && 
                    s[i + 3] is AlphaNumeric an && _ldrStrRegex.IsMatch(an))
                {
                    i += 3;
                    nextStack.Push(builder.FromRawTokens(an, destination, source, offset));
                }
                else
                {
                    nextStack.Push(s[i]);
                }
            }

            for(; i < s.Length; i++)
            {
                nextStack.Push(s[i]);
            }

            return new Stack<IToken>(nextStack);
        }
    }
}
