using AssemblerLib.Commands.LoadStore;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System.Collections.Generic;
using System.Linq;

namespace AssemblerLib.Grammar_Rules.Substitution
{
    public class LoadStoreWritebackRule : IGrammerRule
    {
        private static LoadStoreWritebackBuilder _builder = new LoadStoreWritebackBuilder();
        // LDRI! R1, R2, 0
        // LDR! R1, R2, R3, 0
        // STR! R1, R2, R3, 0
        // STRI! R1, R2, 0
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var s = currentStack.ToArray();
            var nextStack = new Stack<IToken>();
            var i = 0;

            for (; i + 4 < s.Length; i++)
            {
                if (i + 5 < s.Length &&
                    s[i] is SignedValueToken rotate &&
                    s[i + 1] is RegisterToken shift &&
                    s[i + 2] is RegisterToken source &&
                    s[i + 3] is RegisterToken destination &&
                    s[i + 4] is SpecialChars sc && sc == "!" &&
                    s[i + 5] is AlphaNumeric an && an.StartsWith("LDR", "STR"))
                {
                    i += 5;
                    nextStack.Push(_builder.FromRawTokens(an, destination, source, shift, rotate));
                }
                else if (s[i] is SignedValueToken iShift &&
                          s[i + 1] is RegisterToken iSource &&
                          s[i + 2] is RegisterToken iDestination &&
                          s[i + 3] is SpecialChars iSc && iSc == "!" &&
                          s[i + 4] is AlphaNumeric iAn && iAn.StartsWith("LDR", "STR") && iAn.EndsWith("I"))
                {
                    i += 4;
                    nextStack.Push(_builder.FromRawTokens(iAn, iDestination, iSource, iShift));
                }
                else
                {
                    nextStack.Push(s[i]);
                }
            }

            for (; i < s.Length; i++)
            {
                nextStack.Push(s[i]);
            }
            return new Stack<IToken>(nextStack);
        }
    }

    internal static class AlphaExtensions
    {
        public static bool StartsWith(this AlphaNumeric an, params string[] startings)
        {
            var content = an.ToString();
            return startings.Any(a => content.StartsWith(a));
        }

        public static bool EndsWith(this AlphaNumeric an, string ending)
        {
            return an.ToString().EndsWith(ending);
        }
    }
}
