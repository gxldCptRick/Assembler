using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using AssemblerLib.Commands.LoadStore;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Grammar_Rules.Substitution
{
    public class LoadStoreRegisterRule : IGrammerRule
    {
        //LDR R5, R6, R7, -0
        private static readonly Regex _ldrStrRegex = new Regex("((((LD)|(ST))R)||(((LD)|(ST))R)[A-Za-z]{2})");
        private static readonly LoadStoreRegisterBuilder builder = new LoadStoreRegisterBuilder();
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var s = currentStack.ToArray();
            var nextStack = new Stack<IToken>();
            int i = 0;

            for (; i + 4 < s.Length; i++)
            {
                if (s[i] is SignedValueToken shift &&
                    s[i + 1] is RegisterToken offset &&
                    s[i + 2] is RegisterToken source && 
                    s[i + 3] is RegisterToken destination &&
                    s[i + 4] is AlphaNumeric an && _ldrStrRegex.IsMatch(an))
                {
                    i += 4;
                    nextStack.Push(builder.FromRawTokens(an, destination, source, offset, shift));
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
