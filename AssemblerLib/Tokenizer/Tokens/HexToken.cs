using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace AssemblerLib.Tokenizer.Tokens
{
    public class HexToken: NumericToken, IEquatable<HexToken>
    {
        static readonly Regex hexRegex = new Regex("0[xX][A-Fa-f0-9]+");
        protected override string FormatValue(int value) => $"0x{value.ToString("X")}";
        protected override bool IsValidValue(string value) => hexRegex.IsMatch(value);
        protected override int ParseValue(string value) => int.Parse(value.Substring(2), NumberStyles.HexNumber);

        public HexToken(string content) : base(content, "Content was not a hex value in the format of 0x[HEXVALUE]")
        { }
        public override int GetHashCode() => Value.GetHashCode();
        public override bool Equals(object obj) => obj is HexToken h && Equals(h);
        public bool Equals(HexToken other) => other != null && other.Value == Value;
    }
}
