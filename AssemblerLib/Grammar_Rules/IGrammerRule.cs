using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Grammar_Rules
{
    public interface IGrammerRule
    {
        Stack<IToken> ReduceStack(Stack<IToken> currentStack);
    }
}
