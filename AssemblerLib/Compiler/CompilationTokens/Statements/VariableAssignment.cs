using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AssemblerLib.Compiler.CompilationTokens.Tokens;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Compiler.CompilationTokens.Statements
{
    public class VariableAssignment : IStatement
    {
        public string Content => $"{Bucket} = {Value}";

        public Expression Value { get; set; }
        public Variable Bucket { get; set; }

        public VariableAssignment(Variable bucket, Expression value)
        {
            Bucket = bucket;
            Value = value;
        }
        public IEnumerable<IToken> AssemblyCommand()
        {
            return Value.Assemble().Concat(Bucket.WriteOut());
        }
    }
}
