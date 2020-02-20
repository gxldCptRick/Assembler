using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AssemblerLib.Tokenizer.Tokens
{
    [DebuggerDisplay("Special Character: {Content}")]
    public class SpecialChars : IToken, IEquatable<SpecialChars>
    {
        private static readonly ISet<char> SpecialCharacters = new HashSet<char>() { ',', '(', ')', ':', '-', '!', ';', '+', '*', '=' };
        public static bool IsSpecialCharacter(string content)
        {
            return content != null &&
                content.Trim().Length == 1 &&
                IsSpecialCharacter(content.Trim()[0]);
        }

        public static bool IsSpecialCharacter(char content)
        {
            return SpecialCharacters.Contains(content);
        }

        private string _content;
        public string Content
        {
            get => _content;
            set
            {
                if (IsSpecialCharacter(value))
                {
                    _content = value.Trim();
                }
                else
                {
                    throw new ArgumentException("Content was not a valid special character", nameof(value));
                }
            }
        }
        public SpecialChars(string content)
        {
            Content = content;
        }

        public override int GetHashCode()
        {
            return Content.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is SpecialChars sc && Equals(sc);
        }

        public bool Equals(SpecialChars other)
        {
            return other != null && other.Content == Content;
        }

        public override string ToString()
        {
            return Content;
        }

        public static implicit operator string(SpecialChars sc)
        {
            return sc.Content;
        }
    }
}
