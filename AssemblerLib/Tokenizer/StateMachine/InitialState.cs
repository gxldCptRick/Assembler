using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Tokenizer.StateMachine
{
    public class InitialState : IState
    {
        public IState Transition(char condition, StringBuilder currentGroup, List<IToken> currentlyProccessedTokens)
        {
            IState state;
            currentGroup.Append(condition);
            if (char.IsWhiteSpace(condition))
            {
                state = this;
            }
            else if (char.IsLetter(condition))
            {
                state = new PossiblyAlphaNumeric();
            }
            else if (condition == '0')
            {
                state = new PossiblyHexNumeric();
            }
            else if (condition >= '1' && condition <= '9')
            {
                state = new PossiblyDecNumeric();
            }
            else if (SpecialChars.IsSpecialCharacter(condition))
            {
                currentlyProccessedTokens.Add(new SpecialChars(condition.ToString()));
                currentGroup.Clear();
                state = this;
            }
            else if (condition == '/')
            {
                state = new StartComment();
            }
            else
            {
                throw new ArgumentException($"{condition} is not reconginzed as a command");
            }
            return state;
        }
    }
}
