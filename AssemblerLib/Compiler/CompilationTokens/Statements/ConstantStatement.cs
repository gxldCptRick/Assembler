using System;
using System.Collections.Generic;
using System.Text;
using AssemblerLib.Commands.LoadStore;
using AssemblerLib.Commands.MoveTopBottom;
using AssemblerLib.Commands.PushPop;
using AssemblerLib.Compiler.CompilationTokens.BoostedTokens;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Compiler.CompilationTokens.Statements
{
    public class ConstantStatement : IStatement
    {
        public string Content => throw new NotImplementedException();
        private NumericToken _constantValue;

        public ConstantStatement(NumericToken constantValue)
        {
            _constantValue = constantValue;
        }

        const int lastSixteen = 0xFF_FF;
        static readonly RegisterToken _defaultValueRegister = new RegisterToken("R7");
        static readonly RegisterToken _stackPointerRegister = new RegisterToken("R13");
        public IEnumerable<IToken> AssemblyCommand()
        {
            var bottomValue = _constantValue & lastSixteen;
            var topValue = (_constantValue >> 16) & lastSixteen;
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
