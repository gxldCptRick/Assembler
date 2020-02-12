using System;
using System.Collections.Generic;
using System.Text;
using AssemblerLib.Grammar_Rules.Tokens;

namespace AssemblerLib.Commands.LoadStore
{
    class LoadStoreWritebackRegisterToken : LoadStoreRegisterToken
    {
        public override string Content => $"{_operation}{(_condition == Condition.AL ? (object)"" : (object)_condition)}! {_destination}, {_source}, {_offset}, {_shift}";
        public LoadStoreWritebackRegisterToken(LoadStoreSelection operation, Condition condition, RegisterToken source, RegisterToken destination, RegisterToken offset, SignedValueToken shift) : base(operation, condition, source, destination, offset, shift)
        {
        }

        protected override int EncodeHook()
        {
            return base.EncodeHook() | 1 << LoadStoreConstants.WRITEBACK_OFFSET;
        }
    }
}
