using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AssemblerLib.Tokenizer.Tokens
{
    public class DecimalToken : NumericToken, IEquatable<DecimalToken>
    {

        private static readonly Regex numberRegex = new Regex("[0-9]+");
        protected override string FormatValue(int value) => value.ToString();
        protected override bool IsValidValue(string value) => numberRegex.IsMatch(value);
        protected override int ParseValue(string value) => int.Parse(value);
        public DecimalToken(string content) : base(content, "Content was not a valid decimal")
        { }
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        public override bool Equals(object obj) => obj is DecimalToken d && Equals(d);
        public bool Equals(DecimalToken other) => other != null && other.Value == Value;
    }
}
