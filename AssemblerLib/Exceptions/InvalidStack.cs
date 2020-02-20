using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssemblerLib.Exceptions
{
    internal static class StackExtensions
    {
        public static string FormatValues<T>(this Stack<T> stack)
        {
           var s = stack.ToArray();
            return $"[{(s.Length > 0 ? s.Select(e => e.ToString()).Aggregate((agg, next) => $"{agg}, {next}"): "")}]";
        }
    }

    public class InvalidStack : ArgumentException
    {
        private const string DefaultMessage = "Stack produced was not reduced correctly";
        public Stack<IToken> Stack { get; }
        public string Reason { get; }

        public InvalidStack(Stack<IToken> invalidStack, string reason) : base($"Invalid Stack reason: {reason}:\n\t{invalidStack.FormatValues()}")
        {
            Stack = invalidStack;
            Reason = reason;
        }

        public InvalidStack() : base(DefaultMessage)
        {
        }

        public InvalidStack(string message) : base(message)
        {
        }

        public InvalidStack(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
