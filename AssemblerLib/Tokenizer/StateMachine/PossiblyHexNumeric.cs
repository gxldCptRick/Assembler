using System;
using System.Collections.Generic;
using System.Text;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Tokenizer.StateMachine
{
    public class PossiblyHexNumeric : IState
    {
        public IState Transition(char condition, StringBuilder currentGroup, List<IToken> currentlyProccessedTokens)
        {
            currentGroup.Append(condition);
            if(condition == 'x' || condition == 'X')
            {
                return new ValidatingHexNumber();
            }else if(char.IsDigit(condition))
            {
                return new PossiblyDecNumeric();
            }else if (char.IsWhiteSpace(condition))
            {
                currentlyProccessedTokens.Add(new DecimalToken(currentGroup.ToString()));
                currentGroup.Clear();
                return new InitialState();
            }
            throw new ArgumentException($"There is no case for: {condition}", nameof(condition));
        }
    }
}
