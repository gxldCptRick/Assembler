using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System;

namespace AssemblerLib.Commands.DataProcessing
{
    public class DataProccessImmediateToken : DataProccessingToken
    {
        private NumericToken _immediateValue;

        public override string Content => $"{_opCode}{(_condition == Condition.AL ? (object)"" : (object)_condition)}I {_destinationRegister}, {_sourceRegister}, {ImmediateValue}";

        public NumericToken ImmediateValue
        {
            get => _immediateValue;
            private set
            {
                if (value == null) throw new ArgumentNullException(nameof(ImmediateValue));
                _immediateValue = value;
                
            }
        }

        public DataProccessImmediateToken(Condition condition, OperationCode opCode, RegisterToken sourceRegister, RegisterToken destinationRegister, NumericToken immediateValue) : base(condition, opCode, sourceRegister, destinationRegister)
        {
            ImmediateValue = immediateValue;
        }

        protected override int EncodeSections()
        {
            int value = 0b0000_0000_0000_0000_0000_0000_1111_1111 & ImmediateValue;
            int mods = 0;
            mods |= 1 << DataProccessConstants.IMMEDIATE_OPERAND_OFFSET;
            mods |= value << DataProccessConstants.IMMEDIATE_VALUE_OFFSET;

            return mods;
        }
    }
}
