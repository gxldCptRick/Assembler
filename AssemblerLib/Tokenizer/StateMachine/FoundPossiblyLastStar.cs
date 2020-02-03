using System.Collections.Generic;
using System.Text;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Tokenizer.StateMachine
{
    internal class FoundPossiblyLastStar : IState
    {
        public IState Transition(char condition, StringBuilder currentGroup, List<IToken> currentlyProccessedTokens)
        {
            IState state;
            currentGroup.Append(condition);
            if(condition == '/')
            {
                currentlyProccessedTokens.Add(new CommentToken(currentGroup.ToString()));
                currentGroup.Clear();
                state = new InitialState();
            }
            else if(condition == '*')
            {
                state = this;
            }
            else
            {
                state = new MultiLineComment();
            }
            return state;
        }
    }
}