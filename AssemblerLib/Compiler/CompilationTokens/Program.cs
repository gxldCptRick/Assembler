using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Parser;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AssemblerLib.Compiler.CompilationTokens
{
    [DebuggerDisplay("Program: [{Content}]")]
    public class Program : ICompilationToken
    {
        public string Content => Statements.Select(s => s.Content).Aggregate("", (agg, next) => $"{agg}{Environment.NewLine}{next}").Trim();
        private IEnumerable<IStatement> Statements { get; set; }
        private ProgramToken InjectedProgram { get; set; }

        public Program(IEnumerable<IStatement> statements)
        {
            Statements = statements.ToList();
        }

        public Program(params IStatement[] statements) : this(statements as IEnumerable<IStatement>)
        {
        }

        public IEnumerable<IToken> AssembleInstructions()
        {
            bool hasPlaced = false;
            foreach (var item in Statements.Reverse())
            {
                foreach (var token in item.AssemblyCommand())
                {
                    if(token is InstructionToken it && it.Label != null && !hasPlaced && InjectedProgram != null)
                    {
                        hasPlaced = true;
                        foreach (var programInstruction in InjectedProgram.Instructions)
                        {
                            yield return programInstruction;
                        }
                    }
                    yield return token;
                }
            }
            if(!hasPlaced && InjectedProgram != null)
            {
                foreach (var instruction in InjectedProgram.Instructions)
                {
                    yield return instruction;
                }
            }
        }

        public ProgramToken Assemble()
        {
            return new AssemblyParser().Parse(AssembleInstructions());
        }


        public override string ToString()
        {
            return $"{{{Content}}}";
        }

        internal void InjectProgram(ProgramToken injectedProgram)
        {
            InjectedProgram = injectedProgram;
        }
    }
}
