﻿using AssemblerLib.Commands.Branch;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AssemblerLib.Grammar_Rules.Tokens
{
    public class ProgramToken : IToken
    {
        public string Content => Instructions
            .Select(i => i.ToString())
            .Aggregate("", (agg, next) => $"{agg}{Environment.NewLine}{next}").Trim();

        private IDictionary<AlphaNumeric, int> _labelMapping;
        public IList<InstructionToken> Instructions
        {
            get => _instructions;
            set
            {
                value = value.Reverse().ToList();
                _labelMapping = new Dictionary<AlphaNumeric, int>();
                for (var i = 0; i < value.Count; i++)
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
            Instructions = groupedInstructions.ToList();
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
            return obj is ProgramToken token && token.Compile().SequenceEqual(Compile());
        }

        public override int GetHashCode()
        {
            var hashCode = -1075755498;
            hashCode = hashCode * -1521134295 + EqualityComparer<IList<InstructionToken>>.Default.GetHashCode(Instructions);
            return hashCode;
        }

    }
}
