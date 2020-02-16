using AssemblerLib.Grammar_Rules.Tokens;

namespace AssemblerLib.Commands.LoadStore
{
    public abstract class LoadStoreToken : IOperation
    {
        public abstract string Content { get; }

        protected LoadStoreSelection _operation;
        protected Condition _condition;
        protected RegisterToken _source;
        protected RegisterToken _destination;

        public LoadStoreToken(LoadStoreSelection operation, Condition condition, RegisterToken source, RegisterToken destination)
        {
            _condition = condition;
            _operation = operation;
            _source = source;
            _destination = destination;
        }
        //LDRI! R1, R2, 0
        protected abstract int EncodeHook();

        public byte[] Encode()
        {
            var i = 0 | EncodeHook();
            i |= ((int)_condition) << LoadStoreConstants.CONDITION_OFFSET;
            i |= 1 << LoadStoreConstants.HARDCODED_ONE_OFFSET;
            if (_operation == LoadStoreSelection.LDR)
            {
                i |= 1 << LoadStoreConstants.PREPOST_OFFSET;
            }
            i |= ((int)_operation) << LoadStoreConstants.LOADSTORE_OFFSET;
            i |= _source << LoadStoreConstants.SOURCE_REGISTER_OFFSET;
            i |= _destination << LoadStoreConstants.DESTINATION_REGISTER_OFFSET;
            var command = i.ToByteArray();
            command.Reverse();
            return command;
        }

        public override string ToString()
        {
            return Content;
        }
    }
}
