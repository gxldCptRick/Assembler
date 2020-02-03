using System;
using System.Collections.Generic;
using System.Text;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Tokenizer.StateMachine
{
    internal class SingleLineComment : IState
    {
        public IState Transition(char condition, StringBuilder currentGroup, List<IToken> currentlyProccessedTokens)
        {
            IState state;
            if(condition == '\n')
            {
                currentlyProccessedTokens.Add(new CommentToken(currentGroup.ToString()));
                currentGroup.Clear();
                state = new InitialState();
            }
            else
            {
                currentGroup.Append(condition);
                state = this;
            }
            return state;
        }
    }
}