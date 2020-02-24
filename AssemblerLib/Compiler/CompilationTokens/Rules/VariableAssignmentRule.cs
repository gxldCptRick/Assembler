using AssemblerLib.Compiler.CompilationTokens.Statements;
using AssemblerLib.Compiler.CompilationTokens.Tokens;
using AssemblerLib.Grammar_Rules;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AssemblerLib.Compiler.CompilationTokens.Rules
{
    [DebuggerDisplay("I := V = E")]
    public class VariableAssignmentRule : IGrammerRule
    {
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var s = currentStack.ToArray();
            var nextStack = new Stack<IToken>();
            var i = 0 ;
            for (; i + 2 < s.Length; i++)
            {
                if(s[i] is Expression value &&
                    s[i + 1] is SpecialChars sc && sc == "=" &&
                    s[i + 2] is Variable bucket)
                {
                    i += 2;
                    nextStack.Push(new VariableAssignment(bucket, value));
                }
                else
                {
                    nextStack.Push(s[i]);
                }
            }
            for (; i < s.Length; i++) { nextStack.Push(s[i]); }
            return new Stack<IToken>(nextStack);
        }
    }
}
