using AssemblerLib.Grammar_Rules;
using AssemblerLib.Grammar_Rules.Substitution;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                new BranchRule(),
                new MoveTopBottomRule(),
                new DataProccessImmediateRule(),
                new DataProccessRegisterRule(),
                new SignedValueRule(),
                new LoadStoreImmediateRule(),
                new LoadStoreRegisterRule(),
                new InstructionRule()
            };
        }
        public ProgramToken Parse(string input)
        {
            var memoryStack = new Stack<IToken>();
            var tokenStream = _tokenizer.Tokenize(input).Where(t => !(t is CommentToken) && !(t is SpecialChars sc && sc == ","));
            foreach (var token in tokenStream)
            {
                memoryStack.Push(token);
                foreach (var rule in _rulesEngine)
                {
                    memoryStack = rule.ReduceStack(memoryStack);
                }
            }
            memoryStack = new ProgramRule().ReduceStack(memoryStack);

            if (memoryStack.Count != 1) throw new ArgumentException("Could not parse the inputs");
            if (!(memoryStack.Peek() is ProgramToken)) throw new ArgumentException("The input did not parse correctly");
            return memoryStack.Pop() as ProgramToken;
        }
    }
}
