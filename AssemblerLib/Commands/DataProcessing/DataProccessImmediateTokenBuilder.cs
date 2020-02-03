using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Commands.DataProcessing
{
    public class DataProccessImmediateTokenBuilder
    {
        public DataProccessImmediateToken  FromRawResources(
            AlphaNumeric operation,
            RegisterToken destination,
            RegisterToken source,
            NumericToken secondParam)
        {
            var opCode = (OperationCode)Enum.Parse(typeof(OperationCode), operation.SubString(0, 3));
            var condition = Condition.AL;
            if (operation.Length() > 4)
            {
                condition = (Condition)Enum.Parse(typeof(Condition), operation.SubString(3, 2));
            }
            return new DataProccessImmediateToken(condition, opCode, source, destination, secondParam);
        }
    }
}
