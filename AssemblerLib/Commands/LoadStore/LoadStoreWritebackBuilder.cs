using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System;

namespace AssemblerLib.Commands.LoadStore
{
    public class LoadStoreWritebackBuilder
    {
        // LDRI! R1, R2, 0
        // LDR! R1, R2, R3, 0
        // STR! R1, R2, R3, 0
        // STRI! R1, R2, 0
        public LoadStoreToken FromRawTokens(params IToken[] tokens)
        {
            var marker = (tokens[0] as AlphaNumeric).ToString();
            var destination = tokens[1] as RegisterToken;
            var source = tokens[2] as RegisterToken;
            var operation = (LoadStoreSelection)Enum.Parse(typeof(LoadStoreSelection), marker.Substring(0, 3));
            var condition = Condition.AL;

            if (tokens.Length == 5)
            {
                if (marker.Length > 3)
                {
                    condition = (Condition)Enum.Parse(typeof(Condition), marker.Substring(3, 2));
                }
                var offset = tokens[3] as RegisterToken;
                var shift = tokens[4] as SignedValueToken;
                return new LoadStoreWritebackRegisterToken(operation, condition, source, destination, offset, shift);
            }
            else if (tokens.Length == 4)
            {
                if (marker.Length > 4)
                {
                    condition = (Condition)Enum.Parse(typeof(Condition), marker.Substring(3, 2));
                }
                var offset = tokens[3] as SignedValueToken;
                return new LoadStoreWritebackImmediateToken(operation, condition, source, destination, offset);
            }
            else
            {
                throw new ArgumentException($"Cannot make writeback with given amount of tokens: {tokens.Length}");
            }
        }
    }
}
