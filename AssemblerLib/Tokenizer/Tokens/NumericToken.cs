using System;

namespace AssemblerLib.Tokenizer.Tokens
{
    public abstract class NumericToken : IToken, IEquatable<NumericToken>
    {
        private int _value;
        protected abstract bool IsValidValue(string value);
        protected abstract int ParseValue(string value);
        protected abstract string FormatValue(int value);

        public string Content
        {
            get => FormatValue(_value);
            set
            {
                if (IsValidValue(value))
                {
                    _value = ParseValue(value.Trim());
                }
                else
                {
                    throw new ArgumentException(_errorMessage, nameof(Content));
                }
            }
        }

        private readonly string _errorMessage;
        public int Value => _value;

        public NumericToken(string content, string errorMessage = "Something went wrong")
        {
            Content = content;
            _errorMessage = errorMessage;
        }

        public override string ToString()
        {
            return Content;
        }

        public bool Equals(NumericToken other)
        {
            return other != null && other.Value == Value;
        }

        public override bool Equals(object obj)
        {
            return obj is NumericToken num && Equals(num);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator int(NumericToken t)
        {
            return t.Value;
        }
    }
}
