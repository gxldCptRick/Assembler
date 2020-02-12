using AssemblerLib.Commands;
using AssemblerLib.Grammar_Rules.Substitution;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Grammar_Rules.Tokens
{
    public class InstructionToken : IToken, IEnumerable<byte>
    {
        public LabelToken Label { get; }
        public string Content =>Label != null ? $"{Label} {Operation.Content}": Operation.Content;

        public IOperation Operation { get; set; }

        public InstructionToken(IOperation operation, LabelToken label = null)
        {
            Operation = operation;
            Label = label;
        }
        public IEnumerator<byte> GetEnumerator()
        {
            var bytes = (IEnumerable<byte>)Operation.Encode();
            return bytes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public override string ToString()
        {
            return Content;
        }

    }
}
