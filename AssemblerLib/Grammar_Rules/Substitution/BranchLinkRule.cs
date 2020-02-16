using AssemblerLib.Commands.Branch;
using AssemblerLib.Tokenizer.Tokens;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AssemblerLib.Grammar_Rules.Substitution
{
    public class BranchLinkRule : IGrammerRule
    {

        private static Regex branchLinkPattern = new Regex("^(BL[A-Z]{2})|(BL)$");
        private static BranchLinkBuilder builder = new BranchLinkBuilder();


        // BLLT LABEL_NAME
        public Stack<IToken> ReduceStack(Stack<IToken> currentStack)
        {
            var s = currentStack.ToArray();
            var nextStack = new Stack<IToken>();
            var i = 0;

            for (; i + 1 < s.Length; i++)
            {
                if (s[i] is AlphaNumeric name &&
                    s[i + 1] is AlphaNumeric an && branchLinkPattern.IsMatch(an))
                {
                    i += 1;
                    nextStack.Push(builder.FromRawTokens(an, name));
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
}
