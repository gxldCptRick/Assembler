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

        private IOperation _operation;
        private LabelToken _label;


        public LabelToken Label { get => _label; }
        public string Content =>_label != null ? $"{_label} {Operation}": Operation.ToString();

        public IOperation Operation { get => _operation; set => _operation = value; }

        public InstructionToken(IOperation operation, LabelToken label = null)
        {
            Operation = operation;
            _label = label;
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
