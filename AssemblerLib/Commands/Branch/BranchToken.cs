using AssemblerLib.Grammar_Rules.Substitution;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Commands.Branch
{
    public class BranchToken : IOperation
    {
        public string Content => $"B{_condition} {_destinationLabel}";

        private AlphaNumeric _destinationLabel;
        private Condition _condition;
        public IDictionary<AlphaNumeric, int> LabelMapping { get; set; }
        public int Position { get; set; }

        public BranchToken(Condition condition, AlphaNumeric destinationLabel)
        {
            _condition = condition;
            _destinationLabel = destinationLabel;
        }


        public byte[] Encode()
        {
            int i = 0;
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
            int labelIndex = LabelMapping[_destinationLabel];
            int offset;
            if (labelIndex <= Position)
            {
                offset = -(Position - labelIndex + 2);
            }
            else
            {
                offset = labelIndex - Position;
            }
            return offset & 0b0000_0000_1111_1111_1111_1111_1111_1111;
        }

        public override string ToString()
        {
            return Content;
        }
    }
}
