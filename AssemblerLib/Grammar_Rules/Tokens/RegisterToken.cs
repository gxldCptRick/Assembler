﻿using AssemblerLib.Tokenizer.Tokens;
using System;

namespace AssemblerLib.Grammar_Rules.Tokens
{
    public class RegisterToken : IToken
    {
        public string Content => $"R{Rank}";
        private byte _rank;
        public byte Rank
        {
            get => _rank;
            private set
            {
                if (value < 16 && value >= 0)
                {
                    _rank = value;
                }
                else
                {
                    throw new ArgumentException($"Rank must be from 0-15. but it was {value}", nameof(Rank));
                }
            }
        }

        public RegisterToken(AlphaNumeric rawRegister) : this(rawRegister.Content)
        {
        }

        public RegisterToken(string innerText)
        {
            Rank = byte.Parse(innerText.Substring(1));
        }

        public override string ToString()
        {
            return Content;
        }

        public override bool Equals(object obj)
        {
            return obj is RegisterToken token &&
                   Rank == token.Rank;
        }

        public override int GetHashCode()
        {
            return 1852875615 + Rank.GetHashCode();
        }

        public static implicit operator int(RegisterToken t)
        {
            return t.Rank;
        }
    }
}
