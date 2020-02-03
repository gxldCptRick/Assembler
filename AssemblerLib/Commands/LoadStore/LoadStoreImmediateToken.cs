using System;
using System.Collections.Generic;
using System.Text;
using AssemblerLib.Grammar_Rules.Tokens;

namespace AssemblerLib.Commands.LoadStore
{
    public class LoadStoreImmediateToken : LoadStoreToken
    {
        private SignedValueToken _offset;

        public override string Content => $"{_operation}{_condition} {_destination}, {_source}, {_offset}";
        public LoadStoreImmediateToken(LoadStoreSelection operation, Condition condition, RegisterToken source, RegisterToken destination, SignedValueToken offset) : base(operation, condition, source, destination)
        {
            _offset = offset;
        }
        
        protected override int EncodeHook()
        {
            int i = 0;
            i |= 1 << LoadStoreConstants.IMMEDIATE_OFFSET;
            if(_offset < 0)
            {
                i |= -_offset << LoadStoreConstants.IMMEDIATE_VALUE_OFFSET;
            }
            else
            {
                i |= 1 << LoadStoreConstants.UPDOWN_OFFSET;
                i |= _offset << LoadStoreConstants.IMMEDIATE_VALUE_OFFSET;
            }

            return i;
        }
    }
}
