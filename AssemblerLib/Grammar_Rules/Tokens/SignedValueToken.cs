using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Grammar_Rules.Tokens
{
    public class SignedValueToken : IToken
    {
        private readonly NumericToken _nestedToken;

        public string Content => $"{Value}";
        private bool IsNegative { get; }
        public int Value => IsNegative ? -_nestedToken : _nestedToken;

        public SignedValueToken(NumericToken nestedToken, bool isNegative = false)
        {
            _nestedToken = nestedToken;
            IsNegative = isNegative;
        }
        public static implicit operator int(SignedValueToken svt)
        {
            return svt.Value;
        }

        public override string ToString()
        {
            return Content;
        }
    }
}
