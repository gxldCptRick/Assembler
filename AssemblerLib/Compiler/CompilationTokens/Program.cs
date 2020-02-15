using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblerLib.Compiler.CompilationTokens
{
    public class Program : ICompilationToken
    {
        public string Content => Statements.Select(s => s.Content).Aggregate((agg, next) => $"{agg}{Environment.NewLine}{next}");
        private IEnumerable<IStatement> Statements { get; set; }

        public Program(IEnumerable<IStatement> statements)
        {
            Statements = statements.ToList();
        }

        public Program(params IStatement[] statements): this(statements as IEnumerable<IStatement>)
        {
        }

        public IEnumerable<IToken> Assemble()
        {
            foreach(var statement in Statements.Reverse())
            {
                foreach (var token in statement.AssemblyCommand())
                {
                    yield return token;
                }
            }
        }
    }
}
