using AssemblerLib.Compiler.CompilationTokens.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Compiler.CompilationTokens.Meta
{
    public class SymbolTable
    {
        private IDictionary<AlphaNumeric, Variable> DefinedVariables { get; set; }
        private IDictionary<AlphaNumeric, Function> DefinedFunctions { get; set; }
        public int NextAvailableAddress { get; private set; }
        public int BaseAddress { get;  }

       
        public Variable DefineVariable(AlphaNumeric name)
        {
            if (!DefinedVariables.ContainsKey(name))
            {
                DefinedVariables[name] = new Variable(name, NextAvailableAddress);
                NextAvailableAddress += 4;
            }
            
            return DefinedVariables[name];
        }

        public Function DefineFunction(AlphaNumeric name)
        {
            if (!DefinedFunctions.ContainsKey(name))
            {
                DefinedFunctions[name] = new Function(name);
            }

            return DefinedFunctions[name];
        }

        public CompilerTracked Resolve(AlphaNumeric name)
        {
            if (DefinedVariables.ContainsKey(name))
            {
                return DefinedVariables[name];
            }else if (DefinedFunctions.ContainsKey(name))
            {
                return DefinedFunctions[name];
            }else
            {
                throw new NullReferenceException($"Variable is not created for name: {name}");
            }
        }


        public IEnumerable<Function> GetAllFunctions()
        {
            return DefinedFunctions.Values;
        }

        public SymbolTable(int baseAddress)
        {
            DefinedVariables = new Dictionary<AlphaNumeric, Variable>();
            DefinedFunctions = new Dictionary<AlphaNumeric, Function>();
            BaseAddress = NextAvailableAddress = baseAddress;
            
        }

        public Variable ResolveVariable(string variableName)
        {
            var name = new AlphaNumeric(variableName);
            if (!DefinedVariables.ContainsKey(name))
            {
                DefinedVariables[name] = new Variable(name, NextAvailableAddress);
                NextAvailableAddress += 4;
            }
            return DefinedVariables[name];
        }
    }
}
