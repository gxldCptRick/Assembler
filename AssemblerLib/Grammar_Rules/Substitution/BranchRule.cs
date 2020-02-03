using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using AssemblerLib.Commands.Branch;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Grammar_Rules.Substitution
{
    public class BranchRule : IGrammerRule
    {
        private static readonly BranchBuilder builder = new BranchBuilder();
        private static readonly Regex _branchRegex = new Regex("(B[A-Za-z]{2})||(B)");
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var s = currentStack.ToArray();
            var nextStack = new Stack<IToken>();
            int i = 0; 

            for(; i + 1 < s.Length; i++)
            {
                if (s[i] is AlphaNumeric name &&
                    s[i + 1] is AlphaNumeric b && _branchRegex.IsMatch(b))
                {
                    i += 1;
                    nextStack.Push(builder.FromRawTokens(b, name));
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
