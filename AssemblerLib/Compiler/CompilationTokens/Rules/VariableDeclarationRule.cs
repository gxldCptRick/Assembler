using AssemblerLib.Compiler.CompilationTokens.Meta;
using AssemblerLib.Grammar_Rules;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Compiler.CompilationTokens.Rules
{
    public class VariableDeclarationRule : IGrammerRule
    {
        public SymbolTable Symbols { get; set; }
        public VariableDeclarationRule(SymbolTable symbols)
        {
            Symbols = symbols;
        }

        // VAR = EXPRESSION
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var s = currentStack.ToArray();
            var nextStack = new Stack<IToken>();
            var i = 0;
            for(; i + 1 < s.Length; i++)
            {
                if(s[i] is SpecialChars sc && sc == "=" &&
                    s[i + 1] is AlphaNumeric name)
                {
                    i += 1;
                    nextStack.Push(sc);
                    nextStack.Push(Symbols.DefineVariable(name));
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
