using System.Text.RegularExpressions;

namespace AssemblerLib.Tokenizer.Tokens
{
    public class DecimalToken : NumericToken
    {

        private static readonly Regex numberRegex = new Regex("[0-9]+");
        protected override string FormatValue(int value)
        {
            return value.ToString();
        }

        protected override bool IsValidValue(string value)
        {
            return numberRegex.IsMatch(value);
        }

        protected override int ParseValue(string value)
        {
            return int.Parse(value);
        }

        public DecimalToken(string content) : base(content, "Content was not a valid decimal")
        { }
    }
}
