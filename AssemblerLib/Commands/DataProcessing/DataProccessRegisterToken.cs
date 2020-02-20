using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System.Linq;
using System;

namespace AssemblerLib.Commands.DataProcessing
{
    public class DataProccessRegisterToken : DataProccessingToken
    {
        

        private NumericToken _shift;
        private RegisterToken _secondParameter;

        public NumericToken Shift
        {
            get => _shift;
            set
            {
                if (value <= 255 && value >= 0)
                {
                    _shift = value;
                }
                else
                {
                    throw new ArgumentException("Shift value must fit within 8 bits", nameof(Shift));
                }
            }
        }

        public RegisterToken SecondParameter
        {
            get => _secondParameter;
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(SecondParameter));
                }

                _secondParameter = value;
            }
        }
        public override string Content => $"{_opCode}{(_condition == Condition.AL ? "" : (object)_condition)} {_destinationRegister}, {_sourceRegister}, {_secondParameter}, {_shift}";

        public DataProccessRegisterToken(
            Condition condition,
            OperationCode opCode,
            RegisterToken sourceRegister,
            RegisterToken destinationRegister,
            NumericToken shift,
            RegisterToken secondParamater) : base(condition, opCode, sourceRegister, destinationRegister)
        {
            Shift = shift;
            SecondParameter = secondParamater;
        }

        protected override int EncodeSections()
        {
            var mods = 0;
            mods |= Shift << DataProccessConstants.REGISTER_SHIFT_OFFSET;
            mods |= SecondParameter << DataProccessConstants.REGISTER_SECOND_OPERAND;

            return mods;
        }
    }
}
