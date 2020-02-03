using AssemblerLib.Commands.DataProcessing;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Commands.LoadStore
{
    public class LoadStoreImmediateBuilder
    {
        // LDRALI R5, R3, 0
        public LoadStoreImmediateToken FromRawTokens(AlphaNumeric marker, RegisterToken destination, RegisterToken source, SignedValueToken offset)
        {
            var op = (LoadStoreSelection) Enum.Parse(typeof(LoadStoreSelection), marker.SubString(0, 3));
            var cond = Condition.AL;
            if(marker.Length() > 4)
            {
                cond = (Condition)Enum.Parse(typeof(Condition), marker.SubString(3, 2));
            }
            return new LoadStoreImmediateToken(op, cond, source, destination, offset);
        }
    }
}
