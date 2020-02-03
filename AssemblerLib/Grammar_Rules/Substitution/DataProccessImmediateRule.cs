﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using AssemblerLib.Commands.DataProcessing;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Grammar_Rules.Substitution
{
    public class DataProccessImmediateRule : IGrammerRule
    {

        private static readonly Regex _immediatePattern = new Regex("(([A-Za-z]{3})||([A-Za-z]{5}))I");
        private static readonly ISet<string> DontDoIt = new HashSet<string>() { "LDRI", "STRI" };
        private static readonly DataProccessImmediateTokenBuilder builder = new DataProccessImmediateTokenBuilder();
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var s = currentStack.ToArray();
            var nextStack = new Stack<IToken>();
            int i = 0;
            for (; i + 3 < s.Length; i++)
            {
                if (s[i] is NumericToken secondParam &&
                    s[i + 1] is RegisterToken source && 
                    s[i + 2] is RegisterToken destination && 
                    s[i + 3] is AlphaNumeric an && _immediatePattern.IsMatch(an) && !DontDoIt.Contains(an))
                {
                    i += 3;
                    var token = builder.FromRawResources(an, destination, source, secondParam);
                    nextStack.Push(token);              
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
