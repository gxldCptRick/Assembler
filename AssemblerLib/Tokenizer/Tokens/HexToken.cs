using System.Globalization;
using System.Text.RegularExpressions;

namespace AssemblerLib.Tokenizer.Tokens
{
    public class HexToken : NumericToken
    {
        private static readonly Regex hexRegex = new Regex("0[xX][A-Fa-f0-9]+");
        protected override string FormatValue(int value)
        {
            return $"0x{value.ToString("X")}";
        }

        protected override bool IsValidValue(string value)
        {
            return hexRegex.IsMatch(value);
        }

        protected override int ParseValue(string value)
        {
            return int.Parse(value.Substring(2), NumberStyles.HexNumber);
        }

        public HexToken(string content) : base(content, "Content was not a hex value in the format of 0x[HEXVALUE]")
        { }
    }
}
