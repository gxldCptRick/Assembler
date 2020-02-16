using AssemblerLib.Commands.DataProcessing;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System;

namespace AssemblerLib.Commands.Branch
{
    public class BranchLinkBuilder
    {
        public BranchLinkToken FromRawTokens(AlphaNumeric marker, AlphaNumeric name)
        {
            var condition = Condition.AL;
            if (marker.Length() > 2)
            {
                condition = (Condition)Enum.Parse(typeof(Condition), marker.SubString(2, 2));
            }
            return new BranchLinkToken(condition, name);
        }
    }
}
