using System;
using System.Collections.Generic;
using System.Text;
using AssemblerLib.Commands.Multiply;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Grammar_Rules.Substitution
{
    public class MulitplyRule : IGrammerRule
    {
        // MUL Rd, Rm, Rs
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var s = currentStack.ToArray();
            var nextStack = new Stack<IToken>();
            var i = 0;

            for (; i + 3 < s.Length; i++)
            {
                if (s[i] is RegisterToken second &&
                    s[i + 1] is RegisterToken first &&
                    s[i + 2] is RegisterToken dest &&
                    s[i + 3] is AlphaNumeric an && an.Content.StartsWith("MUL"))
                {
                    i += 3;
                    var commandString = an.Content;
                    var condition = commandString.Length > 3 ? (Condition)Enum.Parse(typeof(Condition), commandString.Substring(3, 2)) : Condition.AL;
                    nextStack.Push(new MultiplicationToken(condition, dest, second, first));
                }
                else
                {
                    nextStack.Push(s[i]);
                }
            }

            for(; i < s.Length; i++) { nextStack.Push(s[i]); }
            return new Stack<IToken>(nextStack);
        }
    }
}
