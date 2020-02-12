using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using AssemblerLib.Commands.Branch;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Grammar_Rules.Substitution
{
    public class BranchExchangeRule : IGrammerRule
    {
        Regex exchangeRegex = new Regex("^BX([A-Z]{2})?$");

        // BX R14
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var s = currentStack.ToArray();
            var nextStack = new Stack<IToken>();
            var i = 0; 

            for(; i + 1 < s.Length; i++)
            {
                if(s[i] is RegisterToken register && 
                    s[i + 1] is AlphaNumeric an && exchangeRegex.IsMatch(an))
                {
                    i += 1;
                    var code = an.Content.Length > 2 ? (Condition)Enum.Parse(typeof(Condition), an.Content.Substring(1, 2)) : Condition.AL;
                    nextStack.Push(new BranchExchange(code, register));
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
