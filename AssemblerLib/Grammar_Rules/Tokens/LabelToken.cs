using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

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
        public override string ToString() => Content;
        public override bool Equals(object obj) => obj is LabelToken lt && Equals(lt);

        public override int GetHashCode() => Content.GetHashCode();

        public bool Equals(LabelToken other) => other != null && other.Content == Content;
    }
}
