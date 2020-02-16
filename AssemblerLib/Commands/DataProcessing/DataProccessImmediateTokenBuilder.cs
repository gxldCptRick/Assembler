using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System;

namespace AssemblerLib.Commands.DataProcessing
{
    public class DataProccessImmediateTokenBuilder
    {
        public DataProccessImmediateToken FromRawResources(
            AlphaNumeric marker,
            RegisterToken destination,
            RegisterToken source,
            NumericToken secondParam)
        {
            var opCode = (OperationCode)Enum.Parse(typeof(OperationCode), marker.SubString(0, 3));
            var condition = Condition.AL;
            if (marker.Length() > 4)
            {
                condition = (Condition)Enum.Parse(typeof(Condition), marker.SubString(3, 2));
            }
            return new DataProccessImmediateToken(condition, opCode, source, destination, secondParam);
        }
    }
}
