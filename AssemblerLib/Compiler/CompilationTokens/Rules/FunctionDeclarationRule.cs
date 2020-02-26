using AssemblerLib.Compiler.CompilationTokens.Meta;
using AssemblerLib.Compiler.CompilationTokens.Tokens;
using AssemblerLib.Grammar_Rules;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Compiler.CompilationTokens.Rules
{
    public class FunctionDeclarationRule : IGrammerRule
    {
        public SymbolTable Symbols { get; set; }
        public FunctionDeclarationRule(SymbolTable symbols)
        {
            Symbols = symbols;
        }
        // FUNCTION Conner {
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var s = currentStack.ToArray();
            var nextStack = new Stack<IToken>();
            var i = 0;
            for(; i + 2 < s.Length; i++)
            {
                if(s[i] is SpecialChars sc && sc == "{" &&
                    s[i + 1] is AlphaNumeric name &&
                    s[i + 2] is FunctionKeyWord key)
                {
                    i += 2;
                    nextStack.Push(sc);
                    nextStack.Push(Symbols.DefineFunction(name));
                    nextStack.Push(key);
                }
                else
                {
                    nextStack.Push(s[i]);
                }
            }
            for(; i < s.Length; i++) { nextStack.Push(s[i]); }
            return new Stack<IToken>(nextStack);
        }
    }
}
