using System;
using System.Collections.Generic;
using System.Text;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Grammar_Rules.Substitution
{
    public class ProgramRule : IGrammerRule
    {
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var nextStack = new Stack<IToken>();
            var groupedInstructions = new List<InstructionToken>();
            foreach (var token in currentStack)
            {
                if(token is InstructionToken i)
                {
                    groupedInstructions.Add(i);
                }
                else
                {
                    if (groupedInstructions.Count > 0)
                    {
                        nextStack.Push(new ProgramToken(groupedInstructions));
                        groupedInstructions.Clear();
                    }
                    nextStack.Push(token);
                }
            }

            if (groupedInstructions.Count > 0)
            {
                nextStack.Push(new ProgramToken(groupedInstructions));
                groupedInstructions.Clear();
            }

            return new Stack<IToken>(nextStack);
        }
    }
}
