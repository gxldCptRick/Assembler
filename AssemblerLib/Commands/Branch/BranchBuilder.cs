using AssemblerLib.Commands.DataProcessing;
using AssemblerLib.Grammar_Rules.Substitution;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Commands.Branch
{
    public class BranchBuilder
    {
        // BAL CONNER
        public BranchToken FromRawTokens(AlphaNumeric marker, AlphaNumeric name)
        {
            var condition = Condition.AL;
            if (marker.Length() > 1)
            {
                condition = (Condition)Enum.Parse(typeof(Condition), marker.SubString(1, 2));
            }
            return new BranchToken(condition, name);
        }
    }
}
