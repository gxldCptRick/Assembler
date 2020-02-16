using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AssemblerLib.Grammar_Rules.Substitution
{
    public class RegisterRule : IGrammerRule
    {
        private static readonly Regex _registerPattern = new Regex("R((1[0-5])|[0-9])");

        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var nextStack = new Stack<IToken>();
            foreach (var better in currentStack)
            {
                if (better is AlphaNumeric ac && _registerPattern.IsMatch(ac))
                {
                    nextStack.Push(new RegisterToken(ac));
                }
                else
                {
                    nextStack.Push(better);
                }
            }
            return new Stack<IToken>(nextStack);
        }
    }
}
