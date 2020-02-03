using AssemblerLib.Grammar_Rules.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

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

        protected abstract int EncodeHook();

        public byte[] Encode()
        {
            int i = 0 | EncodeHook();
            i |= ((int)_condition) << LoadStoreConstants.CONDITION_OFFSET;
            i |= 1 << LoadStoreConstants.HARDCODED_ONE_OFFSET;
            i |= 1 << LoadStoreConstants.PREPOST_OFFSET;
            i |= ((int)_operation) << LoadStoreConstants.LOADSTORE_OFFSET;
            i |= _source << LoadStoreConstants.SOURCE_REGISTER_OFFSET;
            i |= _destination << LoadStoreConstants.DESTINATION_REGISTER_OFFSET;
            byte[] command = i.ToByteArray();
            command.Reverse();
            return command;
        }

        public override string ToString()
        {
            return Content;
        }
    }
}
