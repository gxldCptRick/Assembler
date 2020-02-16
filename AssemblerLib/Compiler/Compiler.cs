using AssemblerLib.Compiler.CompilationTokens;
using AssemblerLib.Compiler.CompilationTokens.Rules;
using AssemblerLib.Grammar_Rules;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Parser;
using AssemblerLib.Exceptions;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AssemblerLib.Compiler
{
    public class Compiler
    {
        IList<IGrammerRule> _compilerProccessingEngine;
        public Compiler(Action<ProgramToken> callbackHook)
        {
            _callbackHook = callbackHook;
            _compilerProccessingEngine = new List<IGrammerRule>
            {
                new ExpressTermRule(),
                new ExpressionTermRule(),
            };
        }

        private Action<ProgramToken> _callbackHook;
        public ProgramToken Compile(string input)
        {
            var stack = new Stack<IToken>();
            var inputTokens = new Tokenizer.Tokenizer().Tokenize(input);
            
            foreach (var item in inputTokens)
            {
                stack.Push(item);
                foreach (var rule in _compilerProccessingEngine)
                {
                    stack = rule.ReduceStack(stack);
                }
            }
            stack = new ConstantRule().ReduceStack(stack);
            stack = new FinalProgramRule().ReduceStack(stack);
            if (stack.Count != 1) throw new InvalidStack(stack, $"Stack should have reduced to one not {stack.Count}");
            if (!(stack.Peek() is Program)) throw new InvalidStack(stack, $"Stack should only have {nameof(Program)}");
            var rawProgram = stack.Pop() as Program;
            var program = new AssemblyParser().Parse(rawProgram.Assemble());
            _callbackHook?.Invoke(program);
            return program;
        }
    }
}
