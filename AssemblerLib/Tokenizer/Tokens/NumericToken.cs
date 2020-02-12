using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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

        public override string ToString() => Content;
        public bool Equals(NumericToken other) => other != null && other.Value == Value;
        public override bool Equals(object obj) =>  obj is NumericToken num && Equals(num);
        public override int GetHashCode() => Value.GetHashCode();
        

        public static implicit operator int(NumericToken t) => t.Value;
    }
}
