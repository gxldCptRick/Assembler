﻿using System.Collections.Generic;
using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Compiler.CompilationTokens.Tokens
{
    public class ExpressionTerm : Expression
    {
        public override string Content => Value.Content;
        public override NumericToken Value => Term.Value;
        public Term Term;


        public ExpressionTerm(Term term)
        {
            Term = term;
        }

        public override IEnumerable<IToken> Assemble()
        {
            return Term.Assemble();
        }
    }
}
