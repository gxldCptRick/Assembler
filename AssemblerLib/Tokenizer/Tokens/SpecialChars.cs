using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Tokenizer.Tokens
{
    public class SpecialChars : IToken, IEquatable<SpecialChars>
    {
        static readonly ISet<char> SpecialCharacters = new HashSet<char>() { ',', '(', ')', ':', '-', '!', ';' };
        public static bool IsSpecialCharacter(string content) =>
            content != null &&
            content.Trim().Length == 1 &&
            IsSpecialCharacter(content.Trim()[0]);
        public static bool IsSpecialCharacter(char content) =>
            SpecialCharacters.Contains(content);
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

        public override int GetHashCode() => Content.GetHashCode();
        public override bool Equals(object obj) => obj is SpecialChars sc && Equals(sc);
        public bool Equals(SpecialChars other) => other != null && other.Content == Content;

        public override string ToString() => Content;

        public static implicit operator string(SpecialChars sc) => sc.Content;
    }
}
