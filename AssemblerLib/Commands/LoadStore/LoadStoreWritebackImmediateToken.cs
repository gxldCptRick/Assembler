using AssemblerLib.Grammar_Rules.Tokens;

namespace AssemblerLib.Commands.LoadStore
{
    public class LoadStoreWritebackImmediateToken : LoadStoreImmediateToken
    {

        public override string Content => $"{_operation}{(_condition == Condition.AL ? "" : (object)_condition)}I! {_destination}, {_source}, {_offset}";
        public LoadStoreWritebackImmediateToken(LoadStoreSelection operation, Condition condition, RegisterToken source, RegisterToken destination, SignedValueToken offset) : base(operation, condition, source, destination, offset)
        {
        }
        protected override int EncodeHook()
        {
            return base.EncodeHook() | 1 << LoadStoreConstants.WRITEBACK_OFFSET;
        }
    }
}
