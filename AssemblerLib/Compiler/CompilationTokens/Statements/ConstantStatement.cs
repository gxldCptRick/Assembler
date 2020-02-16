using AssemblerLib.Commands.MoveTopBottom;
using AssemblerLib.Commands.PushPop;
using AssemblerLib.Compiler.CompilationTokens.BoostedTokens;
using AssemblerLib.Compiler.CompilationTokens.Tokens;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System.Collections.Generic;

namespace AssemblerLib.Compiler.CompilationTokens.Statements
{
    public class ConstantStatement : IStatement
    {
        public string Content => _expression.Content;
        private NumericToken ConstantValue => _expression.Value;
        private readonly Expression _expression;


        public ConstantStatement(Expression expression)
        {
            _expression = expression;
        }

        private const int lastSixteen = 0xFF_FF;
        private static readonly RegisterToken _defaultValueRegister = new RegisterToken("R7");
        public IEnumerable<IToken> AssemblyCommand()
        {
            var bottomValue = ConstantValue & lastSixteen;
            var topValue = (ConstantValue >> 16) & lastSixteen;
            return new List<IToken>
            {
                new MoveWordToken(
                    Condition.AL,
                    _defaultValueRegister,
                    new RawNumericToken(bottomValue)),
                new MoveTopToken(
                    Condition.AL,
                    _defaultValueRegister,
                    new RawNumericToken(topValue)),
                new PushToken(Condition.AL,
                _defaultValueRegister)
            };
        }
    }
}
