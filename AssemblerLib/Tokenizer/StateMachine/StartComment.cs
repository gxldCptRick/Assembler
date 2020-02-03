using System;
using System.Collections.Generic;
using System.Text;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Tokenizer.StateMachine
{
    internal class StartComment : IState
    {
        public IState Transition(char condition, StringBuilder currentGroup, List<IToken> currentlyProccessedTokens)
        {
            IState state;
            currentGroup.Append(condition);
            if (condition == '/')
            {
                state = new SingleLineComment();
            }
            else if (condition == '*')
            {
                state = new MultiLineComment();
            }
            else
            {
                throw new ArgumentException($"Character not mapped to a state: {condition}", nameof(condition));
            }
            return state;
        }
    }
}