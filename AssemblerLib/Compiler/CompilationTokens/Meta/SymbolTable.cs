using AssemblerLib.Compiler.CompilationTokens.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Compiler.CompilationTokens.Meta
{
    public class SymbolTable
    {
        private IDictionary<AlphaNumeric, Variable> Symbols { get; set; }
        public int NextAvailableAddress { get; private set; }
        public int BaseAddress { get;  }

        public Variable Resolve(AlphaNumeric an)
        {
            if (!Symbols.ContainsKey(an))
            {
                Symbols[an] = new Variable(an, NextAvailableAddress);
                NextAvailableAddress += 4;
            }
            return Symbols[an];
        }

        public SymbolTable(int baseAddress)
        {
            Symbols = new Dictionary<AlphaNumeric, Variable>();
            BaseAddress = NextAvailableAddress = baseAddress;
            
        }

        public Variable Resolve(string variableName)
        {
            return Resolve(new AlphaNumeric(variableName));
        }
    }
}
