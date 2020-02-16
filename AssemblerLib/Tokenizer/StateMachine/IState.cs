using AssemblerLib.Tokenizer.Tokens;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Tokenizer.StateMachine
{
    public interface IState
    {
        IState Transition(char condition, StringBuilder currentGroup, List<IToken> currentlyProccessedTokens);
    }
}
