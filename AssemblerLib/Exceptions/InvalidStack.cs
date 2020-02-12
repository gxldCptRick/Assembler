using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Exceptions
{
    public class InvalidStack: ArgumentException
    {
        const string DefaultMessage = "Stack produced was not reduced correctly";
        public Stack<IToken> Stack { get; }
        public string Reason { get; }

        public InvalidStack(Stack<IToken> invalidStack, string reason): base($"Invalid Stack reason{reason}:\n\t{invalidStack}")
        {
            Stack = invalidStack;
            Reason = reason;
        }

        public InvalidStack(): base(DefaultMessage)
        {
        }

        public InvalidStack(string message): base(message)
        {
        }

        public InvalidStack(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
