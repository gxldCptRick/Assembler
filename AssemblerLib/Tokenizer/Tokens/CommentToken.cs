using System;

namespace AssemblerLib.Tokenizer.Tokens
{
    public class CommentToken : IToken, IEquatable<CommentToken>
    {
        private string _content;
        public string Content
        {
            get => _content;
            set
            {

                if (value == null)
                {
                    throw new ArgumentException($"Content cannot be null", nameof(value));
                }

                value = value.Trim();
                if (value.StartsWith("//"))
                {
                    _content = value.Substring(2).Trim();
                }
                else if (value.StartsWith("/*"))
                {
                    _content = value.Substring(2, value.Length - 4).Trim();
                }
                else
                {
                    throw new ArgumentException($"{value} is not a proper comment. Comments should start with either '//' or '/*'", nameof(value));
                }
            }
        }
        public CommentToken(string content)
        {
            Content = content;
        }
        public override int GetHashCode()
        {
            return Content.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is CommentToken c && Equals(c);
        }

        public bool Equals(CommentToken other)
        {
            return other != null && other.Content == Content;
        }
    }
}
