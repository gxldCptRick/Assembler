using AssemblerLib.Commands.Branch;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblerLib.Grammar_Rules.Tokens
{
    public class ProgramToken : IToken
    {
        public string Content => Instructions
            .Select(i => i.Content)
            .Aggregate((agg, next) => $"{agg}{Environment.NewLine}{next}");

        private IDictionary<AlphaNumeric, int> _labelMapping;
        public IList<InstructionToken> Instructions
        {
            get => _instructions;
            set
            {
                value = value.Reverse().ToList();
                _labelMapping = new Dictionary<AlphaNumeric, int>();
                for (int i = 0; i < value.Count; i++)
                {
                    if (value[i].Operation is BranchToken b)
                    {
                        b.Position = i;
                        b.LabelMapping = _labelMapping;
                    }

                    if (value[i].Label != null)
                    {
                        _labelMapping[value[i].Label.Name] = i;
                    }
                }
                _instructions = value;
            }
        }

        private IList<InstructionToken> _instructions;

        public ProgramToken(IEnumerable<InstructionToken> groupedInstructions)
        {
            this.Instructions = groupedInstructions.ToList();
        }

        public IEnumerable<byte> Compile()
        {
            foreach (var instruction in Instructions)
            {
                foreach (var byteInstruction in instruction)
                {
                    yield return byteInstruction;
                }
            }
        }
        public override string ToString()
        {
            return Content;
        }

        public override bool Equals(object obj)
        {
            return obj is ProgramToken token &&
                    Instructions.SequenceEqual(token.Instructions);
        }

        public override int GetHashCode()
        {
            var hashCode = -1075755498;
            hashCode = hashCode * -1521134295 + EqualityComparer<IList<InstructionToken>>.Default.GetHashCode(Instructions);
            return hashCode;
        }
    }
}
