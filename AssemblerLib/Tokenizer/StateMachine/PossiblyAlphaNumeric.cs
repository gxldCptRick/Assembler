using System;
using System.Collections.Generic;
using System.Text;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Tokenizer.StateMachine
{
    public class PossiblyAlphaNumeric : IState
    {
        public IState Transition(char condition, StringBuilder currentGroup, List<IToken> currentlyProccessedTokens)
        {
            IState state;
            if(char.IsLetterOrDigit(condition))
            {
                state = this;
                currentGroup.Append(condition);
            }
            else if(char.IsWhiteSpace(condition))
            {
                currentlyProccessedTokens.Add(new AlphaNumeric(currentGroup.ToString()));
                currentGroup.Clear();
                state = new InitialState();
            }
            else if (SpecialChars.IsSpecialCharacter(condition))
            {
                currentlyProccessedTokens.Add(new AlphaNumeric(currentGroup.ToString()));
                currentGroup.Clear();
                state = new InitialState().Transition(condition, currentGroup, currentlyProccessedTokens);
            }
            else
            {
                throw new ArgumentException($"{condition} is not a recongized path", nameof(condition));
            }
            
            return state;
        }
    }
}
