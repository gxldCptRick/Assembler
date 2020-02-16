using AssemblerLib.Tokenizer.Tokens;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Tokenizer.StateMachine
{
    internal class MultiLineComment : IState
    {
        public IState Transition(char condition, StringBuilder currentGroup, List<IToken> currentlyProccessedTokens)
        {
            IState state;
            currentGroup.Append(condition);
            if (condition == '*')
            {
                state = new FoundPossiblyLastStar();
            }
            else
            {
                state = this;
            }
            return state;
        }
    }
}