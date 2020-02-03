using AssemblerLib.Commands.DataProcessing;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Commands.MoveTopBottom
{
    public class MoveTopBottomBuilder
    {
        //MOVWAL R5, 8
        public IOperation FromRawTokens(AlphaNumeric marker, RegisterToken destination, NumericToken value)
        {
            var condition = Condition.AL;
            var type = marker.SubString(0,4);
            if(marker.Length() > 4)
            {
                condition = (Condition)Enum.Parse(typeof(Condition), marker.SubString(4, 2));
            }
            if(type == "MOVW")
            {
                return new MoveWordToken(condition, destination, value); ;
            }
            else if(type == "MOVT")
            {
                return new MoveTopToken(condition, destination, value);
            }
            else
            {
                //crash
                throw new ArgumentException("CRASHING");
            }
        }
    }
}
