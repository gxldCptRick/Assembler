using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using static AssemblerLib.Compiler.CompilationTokens.BoostedTokens.CompliationExtensions;

namespace AssemblerLib.Compiler.CompilationTokens.Tokens
{
    [DebuggerDisplay("Variable: {Content}")]
    public class Variable : CompilerTracked
    {
        private static readonly int LastSixteen = 0xFFFF;
        public override string Content => $"{Name}:(0x{Address.ToString("X8")})";
        public AlphaNumeric Name { get; set; }
        public int Address { get; set; }
        public bool IsStored { get; set; }
        public Variable(AlphaNumeric name, int address)
        {
            Name = name;
            Address = address;
        }
        private IEnumerable<IToken> LoadAddressIntoRegister()
        {
            var bottomValue = Address & LastSixteen;
            var topValue = (Address >> 16) & LastSixteen;
            return new IToken[] {
                MoveWord(_defaultVariableRegister, bottomValue),
                MoveTop(_defaultVariableRegister, topValue)
            };
        }
        public override IEnumerable<IToken> ResolveValue()
        {
            if (!IsStored) throw new InvalidOperationException("You must first store a value into the variable before reading.");
            return LoadAddressIntoRegister()
                .Concat(new IToken[] {
                    LoadRegister(_defaultVariableRegister, _defaultVariableRegister),
                    Push(_defaultVariableRegister)
                });
        }
        public override IEnumerable<IToken> WriteOut()
        {
            IsStored = true;
            return LoadAddressIntoRegister().Concat(new IToken[] {
                Pop(_defaultValueRegister),
                StoreRegister(_defaultVariableRegister, _defaultValueRegister),
            }).Concat(ResolveValue());
        }
        public override string ToString()
        {
            return Content;
        }
    }
}
