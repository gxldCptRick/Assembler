using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Commands.Branch
{
    public class BranchLinkToken : BranchToken
    {
        public override string Content => $"BL{(_condition == Condition.AL ? "" : (object)_condition)} {_destinationLabel}";
        public BranchLinkToken(Condition condition, AlphaNumeric destinationLabel) : base(condition, destinationLabel)
        {
        }

        public override int PrepareEncode()
        {
            return (1 << 24);
        }
    }
}
