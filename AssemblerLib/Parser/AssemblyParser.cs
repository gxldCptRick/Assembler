using AssemblerLib.Exceptions;
using AssemblerLib.Grammar_Rules;
using AssemblerLib.Grammar_Rules.Substitution;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssemblerLib.Parser
{
    public class AssemblyParser
    {
        private List<IGrammerRule> _rulesEngine;
        private Tokenizer.Tokenizer _tokenizer = new Tokenizer.Tokenizer();
        public AssemblyParser()
        {
            _rulesEngine = new List<IGrammerRule>()
            {
                new RegisterRule(),
                new LabelRule(),
                new BranchExchangeRule(),
                new BranchRule(),
                new BranchLinkRule(),
                new PushPopRule(),
                new MulitplyRule(),
                new MoveTopBottomRule(),
                new MovShorthandRule(),
                new DataProccessImmediateRule(),
                new DataProccessRegisterRule(),
                new SignedValueRule(),
                new LoadStoreWritebackRule(),
                new LoadStoreImmediateRule(),
                new LoadStoreRegisterRule(),
                new InstructionRule()
            };
        }
        public ProgramToken ParseAssembly(string input)
        {

            return Parse(_tokenizer.Tokenize(input));
        }

        public ProgramToken Parse(IEnumerable<IToken> tokens)
        {
            if (tokens.Count() == 0) return new ProgramToken(Array.Empty<InstructionToken>());
            var stack = new Stack<IToken>();
            var tokenStream = tokens.Where(t => !(t is CommentToken) && !(t is SpecialChars sc && sc == ","));
            foreach (var token in tokenStream)
            {
                stack.Push(token);
                foreach (var rule in _rulesEngine)
                {
                    stack = rule.ReduceStack(stack);
                }
            }
            stack = new ProgramRule().ReduceStack(stack);

            if (stack.Count != 1)
            {
                throw new InvalidStack(stack, $"Stack should have reduced to 1 not {stack.Count}");
            }

            if (!(stack.Peek() is ProgramToken))
            {
                throw new InvalidStack(stack, $"Stack contain {nameof(ProgramToken)} on the stack.");
            }

            return stack.Pop() as ProgramToken;
        }
    }
}
