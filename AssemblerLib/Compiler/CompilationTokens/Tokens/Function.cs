using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AssemblerLib.Commands;
using AssemblerLib.Grammar_Rules.Substitution;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using static AssemblerLib.Compiler.CompilationTokens.BoostedTokens.CompliationExtensions;

namespace AssemblerLib.Compiler.CompilationTokens.Tokens
{
    public class Function: CompilerTracked, IStatement
    {
        public override string Content => $"FUNCTION {Name} {{ {Statements.Aggregate("", (agg, next) => $"{agg}\n{next}")}\n }}";

        public IList<IStatement> Statements { get; set; }
        public AlphaNumeric Name { get;  }

        public Function(AlphaNumeric name, IEnumerable<IStatement> statements = null)
        {
            statements = statements ?? Array.Empty<IStatement>();
            Name = name;
            Statements = statements.ToList();
        }

        public override IEnumerable<IToken> ResolveValue()
        {
            return new IToken[] { BranchTo(Name) };
        }
        
        public override IEnumerable<IToken> WriteOut()
        {
            if(Statements.Count > 0)
            {
                var assembly = Statements.Select(c => c.AssemblyCommand()).SelectMany(e => e).ToList();
                assembly[0] = new InstructionToken(assembly[0] as IOperation, label: new LabelToken(Name));
                assembly.AddRange(ClearStack());
                assembly.Add(Push(_defaultValueRegister));
                assembly.Add(BranchBack());
                return assembly;
            }
            else
            {
                return new IToken[] { new InstructionToken(BranchBack(), label: new LabelToken(Name)) };
            }
        }

        private IEnumerable<IToken> ClearStack()
        {
            return Enumerable.Range(0, Statements.Count).Select(c => Pop(_defaultSecondValueRegister));
        }

        public IEnumerable<IToken> AssemblyCommand()
        {
            return WriteOut();
        }
    }
}
