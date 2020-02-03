using System;
using System.Collections.Generic;
using System.Text;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Tokenizer.StateMachine
{
    internal class ValidatingHexNumber : IState
    {
        public bool IsHexAlphaCharacter(char test) => (test >= 'A' && test <= 'F') || (test >= 'a' && test <= 'f');
        public IState Transition(char condition, StringBuilder currentGroup, List<IToken> currentlyProccessedTokens)
        {
            currentGroup.Append(condition);
            if (char.IsDigit(condition) || IsHexAlphaCharacter(condition))
            {
                return this;
            }else if (char.IsWhiteSpace(condition))
            {
                currentlyProccessedTokens.Add(new HexToken(currentGroup.ToString()));
                currentGroup.Clear();
                return new InitialState();
            }
            throw new ArgumentException($"Unexpected character: {condition}", nameof(condition));
        }
    }
}