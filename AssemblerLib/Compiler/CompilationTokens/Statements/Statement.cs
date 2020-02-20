using AssemblerLib.Commands.MoveTopBottom;
using AssemblerLib.Commands.PushPop;
using AssemblerLib.Compiler.CompilationTokens.BoostedTokens;
using AssemblerLib.Compiler.CompilationTokens.Tokens;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System.Collections.Generic;
using System.Diagnostics;

namespace AssemblerLib.Compiler.CompilationTokens.Statements
{
    [DebuggerDisplay("Statement: {Content}")]
    public class ConstantStatement : IStatement
    {
        public string Content => _expression.Content;
        private readonly Expression _expression;


        public ConstantStatement(Expression expression)
        {
            _expression = expression;
        }

        public IEnumerable<IToken> AssemblyCommand()
        {
            return _expression.Assemble();
        }
        public override string ToString()
        {
            return Content;
        }
    }
}
