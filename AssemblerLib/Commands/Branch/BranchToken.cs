using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System.Collections.Generic;

namespace AssemblerLib.Commands.Branch
{
    public class BranchToken : IOperation
    {
        public virtual string Content => $"B{(_condition == Condition.AL ? "" : (object)_condition)} {_destinationLabel}";

        protected AlphaNumeric _destinationLabel;
        protected Condition _condition;
        public IDictionary<AlphaNumeric, int> LabelMapping { get; set; }
        public int Position { get; set; }

        public BranchToken(Condition condition, AlphaNumeric destinationLabel)
        {
            _condition = condition;
            _destinationLabel = destinationLabel;
        }

        public virtual int PrepareEncode()
        {
            return 0;
        }

        public byte[] Encode()
        {
            var i = PrepareEncode();
            i |= ((int)_condition) << 28;
            // hardcoded values
            i |= 0b101 << 25;
            i |= CalculateOffset() << 0;
            var bytes = i.ToByteArray();
            bytes.Reverse();
            return bytes;
        }

        private int CalculateOffset()
        {
            var labelIndex = LabelMapping[_destinationLabel];
            int offset;
            if (labelIndex <= Position)
            {
                offset = -(Position - labelIndex + 2);
            }
            else
            {
                offset = labelIndex - Position - 2;
            }
            return offset & 0b0000_0000_1111_1111_1111_1111_1111_1111;
        }

        public override string ToString()
        {
            return Content;
        }
    }
}
