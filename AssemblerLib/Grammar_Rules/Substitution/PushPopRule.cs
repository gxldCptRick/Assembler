using AssemblerLib.Commands.PushPop;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AssemblerLib.Grammar_Rules.Substitution
{
    public class PushPopRule : IGrammerRule
    {
        private Regex pushPattern = new Regex("^PUSH([A-Z]{2})?$");
        private Regex popPattern = new Regex("^POP([A-Z]{2})?$");
        //PUSH R4
        //POP R3
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var s = currentStack.ToArray();
            var nextStack = new Stack<IToken>();
            var i = 0;

            for (; i < s.Length; i++)
            {
                if (s[i] is RegisterToken sauce)
                {
                    if (s[i + 1] is AlphaNumeric push &&
                        pushPattern.IsMatch(push))
                    {
                        i += 1;
                        var cond = FromAlphaNumeric(push);
                        nextStack.Push(new PushToken(cond, sauce));
                    }
                    else if (s[i + 1] is AlphaNumeric pop &&
                        popPattern.IsMatch(pop))
                    {
                        i += 1;
                        var cond = FromAlphaNumeric(pop);
                        nextStack.Push(new PopToken(cond, sauce));
                    }
                    else
                    {
                        nextStack.Push(s[i]);
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

        public Condition FromAlphaNumeric(AlphaNumeric an)
        {
            var content = an.Content;
            var condition = Condition.AL;

            if (pushPattern.IsMatch(content) && content.Length > 4)
            {
                condition = (Condition)Enum.Parse(typeof(Condition), content.Substring(4, 2));
            }
            else if (popPattern.IsMatch(an) && content.Length > 3)
            {
                condition = (Condition)Enum.Parse(typeof(Condition), content.Substring(3, 2));
            }

            return condition;
        }
    }
}
