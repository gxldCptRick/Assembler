using AssemblerLib.Commands;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System.Collections.Generic;

namespace AssemblerLib.Grammar_Rules.Substitution
{
    public class InstructionRule : IGrammerRule
    {
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var s = currentStack.ToArray();
            var nextStack = new Stack<IToken>();
            var i = 0;
            for (; i + 1 < s.Length; i++)
            {
                if (s[i] is IOperation op)
                {
                    if (s[i + 1] is LabelToken label)
                    {
                        nextStack.Push(new InstructionToken(op, label));
                        i++;
                    }
                    else
                    {
                        nextStack.Push(new InstructionToken(op));
                    }
                }
                else
                {
                    nextStack.Push(s[i]);
                }
            }

            for (; i < s.Length; i++)
            {
                if (s[i] is IOperation op)
                {
                    nextStack.Push(new InstructionToken(op));
                }
                else
                {
                    nextStack.Push(s[i]);
                }
            }

            return new Stack<IToken>(nextStack);
        }
    }
}
