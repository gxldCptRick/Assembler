﻿using AssemblerLib.Tokenizer.StateMachine;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Tokenizer
{
    public class Tokenizer
    {
        public List<IToken> Tokenize(string input)
        {
            IState startingState = new InitialState();
            var output = new List<IToken>();
            var builder = new StringBuilder();
            
            foreach (var character in $"{input}\n")
            {
                startingState = startingState.Transition(character, builder, output);
            }

            if(startingState is MultiLineComment || startingState is FoundPossiblyLastStar)
            {
                throw new ArgumentException("The Multiline Comment was never terminated", nameof(input));
            }
            else if(!(startingState is InitialState))
            {
                throw new ArgumentException("The Stuff is not what I expected", nameof(input));
            }
            return output;
        }

    }
}