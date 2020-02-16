using AssemblerLib.Tokenizer.Tokens;
using System;

namespace AssemblerLib.Grammar_Rules.Substitution
{
    public class LabelToken : IToken, IEquatable<LabelToken>
    {
        public string Content => $"{Name}:";

        public AlphaNumeric Name { get; }
        public LabelToken(AlphaNumeric name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return Content;
        }

        public override bool Equals(object obj)
        {
            return obj is LabelToken lt && Equals(lt);
        }

        public override int GetHashCode()
        {
            return Content.GetHashCode();
        }

        public bool Equals(LabelToken other)
        {
            return other != null && other.Content == Content;
        }
    }
}
