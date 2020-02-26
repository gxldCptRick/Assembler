using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Compiler.CompilationTokens.Tokens
{
    public class FunctionKeyWord : ICompilationToken
    {
        public string Content => "FUNCTION";
    }
}
