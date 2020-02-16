using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Tokenizer.StateMachine
{
    public class PossiblyDecNumeric : IState
    {
        public IState Transition(char condition, StringBuilder currentGroup, List<IToken> currentlyProccessedTokens)
        {
            IState state;
            if (condition >= '0' && condition <= '9')
            {
                currentGroup.Append(condition);
                state = this;
            }
            else if (char.IsWhiteSpace(condition))
            {
                currentlyProccessedTokens.Add(new DecimalToken(currentGroup.ToString()));
                currentGroup.Clear();
                state = new InitialState();
            }
            else if (SpecialChars.IsSpecialCharacter(condition))
            {
                currentlyProccessedTokens.Add(new DecimalToken(currentGroup.ToString()));
                currentGroup.Clear();
                state = new InitialState().Transition(condition, currentGroup, currentlyProccessedTokens);
            }
            else
            {
                throw new ArgumentException($"The character: {condition} is not numeric", nameof(condition));
            }
            return state;
        }
    }
}