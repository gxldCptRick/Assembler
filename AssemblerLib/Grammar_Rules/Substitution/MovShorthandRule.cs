using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using AssemblerLib.Commands.DataProcessing;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Grammar_Rules.Substitution
{
    public class MovShorthandRule : IGrammerRule
    {
        Regex movMatch = new Regex("^MOV([A-Z]{2})?$");
        DataProccessRegisterBuilder builder = new DataProccessRegisterBuilder();
        // MOV R0, R14, 0

        RegisterToken discard = new RegisterToken("R0");
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var s = currentStack.ToArray();
            var nextStack = new Stack<IToken>();
            var i = 0;

            for(; i + 3 < s.Length; i++)
            {
                if (s[i] is NumericToken rotation &&
                    s[i + 1] is RegisterToken source && 
                    s[i + 2] is RegisterToken destination &&
                    s[i + 3] is AlphaNumeric an && movMatch.IsMatch(an))
                {
                    i += 3;
                    nextStack.Push(builder.FromRawResources(an, destination, discard, source, rotation));
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
