using System;
using System.Text.RegularExpressions;

namespace AssemblerLib.Tokenizer.Tokens
{
    public class AlphaNumeric : IToken, IEquatable<AlphaNumeric>
    {
        private static readonly Regex alphaNumericRegex = new Regex("[A-Z0-9a-z]+");

        private static bool IsValidContent(string content)
        {
            return alphaNumericRegex.IsMatch(content);
        }

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

        public override int GetHashCode()
        {
            return Content?.GetHashCode() ?? 0;
        }

        public override bool Equals(object obj)
        {
            return obj is AlphaNumeric a && Equals(a);
        }

        public bool Equals(AlphaNumeric other)
        {
            return other != null && other.Content == Content;
        }

        public override string ToString()
        {
            return Content;
        }

        public static implicit operator string(AlphaNumeric an)
        {
            return an.Content;
        }
    }
}
