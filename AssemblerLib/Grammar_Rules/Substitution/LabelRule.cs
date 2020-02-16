using AssemblerLib.Tokenizer.Tokens;
using System.Collections.Generic;

namespace AssemblerLib.Grammar_Rules.Substitution
{
    public class LabelRule : IGrammerRule
    {
        public Stack<IToken> ReduceStack(Stack<IToken> outputs)
        {
            var s = outputs.ToArray();
            var nextStack = new Stack<IToken>();

            var i = 0;
            for (; i + 1 < s.Length; i++)
            {
                if (s[i] is SpecialChars sc &&
                    sc == ":" &&
                    s[i + 1] is AlphaNumeric an)
                {
                    nextStack.Push(new LabelToken(an));
                    i++;
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
