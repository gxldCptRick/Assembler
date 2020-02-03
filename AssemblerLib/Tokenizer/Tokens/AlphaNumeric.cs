using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AssemblerLib.Tokenizer.Tokens
{
    public class AlphaNumeric : IToken, IEquatable<AlphaNumeric>
    {
        static readonly Regex alphaNumericRegex = new Regex("[A-Z0-9a-z]+");
        static bool IsValidContent(string content) => alphaNumericRegex.IsMatch(content);
        private string _content;
        public string Content
        {
            get => _content;
            set
            {
                if (IsValidContent(value))
                {
                    _content = value.Trim();
                }
                else

                {
                    throw new ArgumentException("Content must be alphanumeric", nameof(Content));
                }
            }
        }

        public AlphaNumeric(string content)
        {
            Content = content;
        }

        public override int GetHashCode() => Content?.GetHashCode() ?? 0;
        public override bool Equals(object obj) => obj is AlphaNumeric a && Equals(a);
        public bool Equals(AlphaNumeric other) => other != null && other.Content == Content;
        public override string ToString() => Content;

        public static implicit operator string(AlphaNumeric an) => an.Content;
    }
}
