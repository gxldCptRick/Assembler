using AssemblerLib.Grammar_Rules.Tokens;

namespace AssemblerLib.Commands.DataProcessing
{
    public abstract class DataProccessingToken : IOperation
    {
        public abstract string Content { get; }

        protected Condition _condition;
        protected OperationCode _opCode;
        protected RegisterToken _sourceRegister;
        protected RegisterToken _destinationRegister;
        public DataProccessingToken(Condition condition, OperationCode opCode, RegisterToken sourceRegister, RegisterToken destinationRegister)
        {
            _condition = condition;
            _sourceRegister = sourceRegister;
            _destinationRegister = destinationRegister;
            _opCode = opCode;
        }


        protected abstract int EncodeSections();
        public byte[] Encode()
        {
            // apply the bit stuff with whatever we inherit
            var i = 0 | EncodeSections();
            // this is where we play with the bytes
            i |= ((int)_condition) << (DataProccessConstants.CONDITION_OFFSET);
            i &= DataProccessConstants.VALUE_TO_HARD_CODE_ZEROS;
            i |= ((int)_opCode) << DataProccessConstants.OPCODE_OFFSET;
            if (_opCode != OperationCode.MOV)
            {
                i |= 1 << DataProccessConstants.SET_CONDITION_CODE_OFFSET;
            }
            i |= _sourceRegister << DataProccessConstants.SOURCE_REGISTER_OFFSET;
            i |= _destinationRegister << DataProccessConstants.DESTINATION_REGISTER_OFFSET;
            // fixes the endianess of our command
            var bytes = i.ToByteArray();
            bytes.Reverse();
            return bytes;
        }

        public override string ToString()
        {
            return Content;
        }
    }
}
