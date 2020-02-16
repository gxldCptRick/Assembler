using AssemblerLib.Grammar_Rules.Tokens;

namespace AssemblerLib.Commands.LoadStore
{
    public class LoadStoreRegisterToken : LoadStoreToken
    {
        public override string Content => $"{_operation}{(_condition == Condition.AL ? "" : (object)_condition)} {_destination}, {_source}, {_offset}, {_shift}";
        protected SignedValueToken _shift;
        protected RegisterToken _offset;

        public LoadStoreRegisterToken(LoadStoreSelection operation, Condition condition, RegisterToken source, RegisterToken destination, RegisterToken offset, SignedValueToken shift) : base(operation, condition, source, destination)
        {
            _shift = shift;
            _offset = offset;
        }



        protected override int EncodeHook()
        {
            var i = 0;
            if (_shift < 0)
            {
                i |= (-_shift) << LoadStoreConstants.SHIFT_OFFSET;
            }
            else
            {
                i |= 1 << LoadStoreConstants.UPDOWN_OFFSET;
                i |= _shift << LoadStoreConstants.SHIFT_OFFSET;
            }
            i |= _offset << LoadStoreConstants.OFFSET_REGISTER_OFFSET;
            return i;
        }
    }
}
