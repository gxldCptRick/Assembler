using AssemblerLib.Tokenizer.Tokens;
using System.Collections.Generic;

namespace AssemblerLib.Grammar_Rules
{
    public interface IGrammerRule
    {
        Stack<IToken> ReduceStack(Stack<IToken> currentStack);
    }
}
