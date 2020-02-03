using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Commands
{
    public interface IOperation: IToken
    {
        byte[] Encode();
    }
}
