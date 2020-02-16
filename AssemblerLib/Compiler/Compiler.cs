using AssemblerLib.Compiler.CompilationTokens;
using AssemblerLib.Compiler.CompilationTokens.Rules;
using AssemblerLib.Exceptions;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Parser;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssemblerLib.Compiler
{
    public class Compiler
    {
        private IList<IConditionalRule> _compilerProccessingEngine;
        public Compiler(Action<ProgramToken> callbackHook)
        {
            _callbackHook = callbackHook;
            _compilerProccessingEngine = new List<IConditionalRule>
            {
                new FactorWrappingExpression(),
                new TermFactorMultiplicationRule(),
                new TermFactorRule(),
                new ExpressionAddTermRule(),
                new ExpressionTermRule(),
            };
        }

        private Action<ProgramToken> _callbackHook;
        public ProgramToken Compile(string input)
        {
            
            var initialTokenizedInput = new Tokenizer.Tokenizer().Tokenize(input);
            var proccessedStack = new FactorSubstituteRule().ReduceStack(new Stack<IToken>(initialTokenizedInput));
            var tokenizedInput = proccessedStack.Reverse().ToList();
            var stack = new Stack<IToken>();
            for (var i = 0; i < tokenizedInput.Count; i++)
            {
                var currentElement = tokenizedInput[i];
                var nextElement = i + 1 < tokenizedInput.Count ? tokenizedInput[i + 1] : null;
                stack.Push(currentElement);
                foreach (var rule in _compilerProccessingEngine)
                {
                    stack = rule.ConditionallyReduceStack(stack, nextElement);
                }
            }
            stack = new ConstantRule().ReduceStack(stack);
            stack = new FinalProgramRule().ReduceStack(stack);
            if (stack.Count != 1)
            {
                throw new InvalidStack(stack, $"Stack should have reduced to one not {stack.Count}");
            }

            if (!(stack.Peek() is Program))
            {
                throw new InvalidStack(stack, $"Stack should only have {nameof(Program)}");
            }

            var rawProgram = stack.Pop() as Program;
            var program = new AssemblyParser().Parse(rawProgram.Assemble());
            _callbackHook?.Invoke(program);
            return program;
        }
    }
}
