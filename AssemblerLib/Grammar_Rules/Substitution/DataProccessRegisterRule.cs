using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using AssemblerLib.Commands.DataProcessing;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Grammar_Rules.Substitution
{
    public class DataProccessRegisterRule : IGrammerRule
    {
        private static readonly DataProccessRegisterBuilder builder = new DataProccessRegisterBuilder();
        private static readonly Regex _basicPattern = new Regex("([A-Za-z]{3})|([A-Za-z]{5})");
        private static readonly ISet<string> DontDoIt = new HashSet<string>() { "LDR", "STR" };
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var s = currentStack.ToArray();
            var nextStack = new Stack<IToken>();
            int i = 0;
            for(; i + 4 < s.Length; i++)
            {
                if (s[i] is NumericToken shift &&
                    s[i + 1] is RegisterToken secondParam &&
                    s[i + 2] is RegisterToken source && 
                    s[i + 3] is RegisterToken destination &&
                    s[i + 4] is AlphaNumeric an && _basicPattern.IsMatch(an) && !IsExcluded(an))
                {
                    i += 4;
                    var operation = builder.FromRawResources(an, destination, source, secondParam, shift);
                    nextStack.Push(operation);
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

        private bool IsExcluded(string an)
        {
            return an.StartsWith("STR") || an.StartsWith("LDR") || an.StartsWith("B");
        }
    }
}
