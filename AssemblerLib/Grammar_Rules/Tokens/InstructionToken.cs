using AssemblerLib.Commands;
using AssemblerLib.Grammar_Rules.Substitution;
using AssemblerLib.Tokenizer.Tokens;
using System.Collections;
using System.Collections.Generic;

namespace AssemblerLib.Grammar_Rules.Tokens
{
    public class InstructionToken : IToken, IEnumerable<byte>
    {
        public LabelToken Label { get; }
        public string Content => Label != null ? $"{Label} {Operation.Content}" : Operation.Content;

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
            return $"{Content}";
        }

        public override bool Equals(object obj)
        {
            return obj is InstructionToken token &&
                    (Label != null ? Label.Equals(token.Label) : Label == token.Label) &&
                   Operation.Equals(token.Operation);
        }

        public override int GetHashCode()
        {
            var hashCode = 1544552139;
            hashCode = hashCode * -1521134295 + EqualityComparer<LabelToken>.Default.GetHashCode(Label);
            hashCode = hashCode * -1521134295 + EqualityComparer<IOperation>.Default.GetHashCode(Operation);
            return hashCode;
        }
    }
}
