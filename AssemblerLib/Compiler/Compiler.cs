using AssemblerLib.Compiler.CompilationTokens;
using AssemblerLib.Compiler.CompilationTokens.Rules;
using AssemblerLib.Grammar_Rules;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Parser;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Compiler
{
    public class Compiler
    {
        IList<IGrammerRule> _compilerEngine;
        public Compiler(Action<ProgramToken> callbackHook)
        {
            _callbackHook = callbackHook;
            _compilerEngine = new List<IGrammerRule> { new ConstantRule() };
        }

        private Action<ProgramToken> _callbackHook;
        public ProgramToken Compile(string input)
        {
            var stack = new Stack<IToken>();
            var inputTokens = new Tokenizer.Tokenizer().Tokenize(input);
            foreach (var item in inputTokens)
            {
                stack.Push(item);
                foreach (var rule in _compilerEngine)
                {
                    stack = rule.ReduceStack(stack);
                }
            }
            stack = new FinalProgramRule().ReduceStack(stack);
            if (stack.Count != 1) throw new ArgumentException("Not able to reduce the stack into program");
            if (!(stack.Peek() is Program)) throw new ArgumentException("Last token on stack is not program");
            var rawProgram = stack.Pop() as Program;
            var program = new AssemblyParser().Parse(rawProgram.Assemble());
            _callbackHook?.Invoke(program);
            return program;
        }
    }
}
