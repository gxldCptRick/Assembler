using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using AssemblerLib.Compiler.CompilationTokens.BoostedTokens;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Compiler.CompilationTokens.Tokens
{
    [DebuggerDisplay("VarFactor: {Content}")]
    public class VariableFactor : Factor
    {
        public override string Content => $"Factor: {ActualValue.ToString()}";
        public override NumericToken Value { get;  }
        public Variable ActualValue { get; private set; }
        public VariableFactor(Variable actualValue)
        {
            ActualValue = actualValue;
            Value = new RawNumericToken(actualValue.Address);
        }
        public override IEnumerable<IToken> Assemble()
        {
            return ActualValue.ResolveValue();
        }
    }
}
