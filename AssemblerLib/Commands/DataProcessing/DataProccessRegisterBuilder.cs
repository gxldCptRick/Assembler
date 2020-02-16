using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System;

namespace AssemblerLib.Commands.DataProcessing
{
    public class DataProccessRegisterBuilder
    {
        //ADDEQ R5, R4, R2, 0
        public DataProccessRegisterToken FromRawResources(
            AlphaNumeric operation,
            RegisterToken destination,
            RegisterToken source,
            RegisterToken secondParam,
            NumericToken shift)
        {
            var opCode = (OperationCode)Enum.Parse(typeof(OperationCode), operation.SubString(0, 3));
            var condition = Condition.AL;
            if (operation.Length() > 3)
            {
                condition = (Condition)Enum.Parse(typeof(Condition), operation.SubString(3, 2));
            }
            return new DataProccessRegisterToken(condition, opCode, source, destination, shift, secondParam);
        }
    }
}
