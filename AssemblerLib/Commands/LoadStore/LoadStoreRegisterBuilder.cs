using AssemblerLib.Commands.DataProcessing;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Commands.LoadStore
{
    public class LoadStoreRegisterBuilder
    {
        //LDRAL R5, R2, R3, 0
        public LoadStoreRegisterToken FromRawTokens(AlphaNumeric marker, RegisterToken destination, RegisterToken source, RegisterToken offset, SignedValueToken shift)
        {
            var op = (LoadStoreSelection)Enum.Parse(typeof(LoadStoreSelection), marker.SubString(0, 3));
            var cond = Condition.AL;
            if (marker.Length() > 3)
            {
                cond = (Condition)Enum.Parse(typeof(Condition), marker.SubString(3, 2));
            }

            return new LoadStoreRegisterToken(op, cond, source, destination, offset, shift);
        }
    }
}
