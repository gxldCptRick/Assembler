using AssemblerLib.Compiler.CompilationTokens;
using AssemblerLib.Compiler.CompilationTokens.Rules;
using AssemblerLib.Compiler.CompilationTokens.Tokens;
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

        private readonly Action<ProgramToken> _callbackHook;
        private Expression ReduceExpression(IList<IToken> tokens)
        {
            var stack = new Stack<IToken>();
            for (var i = 0; i < tokens.Count; i++)
            {
                var currentElement = tokens[i];
                var nextElement = i + 1 < tokens.Count ? tokens[i + 1] : null;
                stack.Push(currentElement);
                foreach (var rule in _compilerProccessingEngine)
                {
                    stack = rule.ConditionallyReduceStack(stack, nextElement);
                }
            }
            if (stack.Count != 1) throw new InvalidStack(stack, "Stack should have reduced to one.");
            if (!(stack.Peek() is Expression)) throw new InvalidStack(stack, "Stack did not reduce to expression");
            return stack.Pop() as Expression;
        }
        public ProgramToken Compile(string input)
        {
            
            var initialTokenizedInput = new Tokenizer.Tokenizer().Tokenize(input);
            var proccessedStack = new FactorSubstituteRule().ReduceStack(new Stack<IToken>(initialTokenizedInput));
            var tokenizedInput = proccessedStack.Reverse().ToList();
            ReducePairs(tokenizedInput);
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

        private IToken StartingParen { get; } = new SpecialChars("(");
        private IToken ClosingParen { get; } = new SpecialChars(")");
        private void ReducePairs(List<IToken> tokenizedInput)
        {
            
            while(tokenizedInput.Contains(StartingParen) || tokenizedInput.Contains(ClosingParen))
            {
                var indexOfStart = tokenizedInput.LastIndexOf(StartingParen);
                var indexOfEnd = tokenizedInput.IndexOf(ClosingParen);
                if ((indexOfStart != -1 && indexOfEnd == -1) ||
                    (indexOfEnd == -1 && indexOfEnd != -1)) throw new InvalidStack("There are no matching parens.");
                while(indexOfEnd < indexOfStart)
                {
                    indexOfEnd = tokenizedInput.IndexOf(ClosingParen, indexOfEnd + 1);
                    if (indexOfEnd == -1) throw new InvalidStack("The Parens are missed matched");
                }
                var tokens = new List<IToken>(indexOfEnd - indexOfStart);
                for(int i = indexOfStart + 1; i < indexOfEnd; i++)
                {
                    tokens.Add(tokenizedInput[i]);
                }
                tokenizedInput.RemoveRange(indexOfStart + 1, tokens.Count);
                var expresssion = ReduceExpression(tokens);
                tokenizedInput.Insert(indexOfStart + 1, expresssion);
                tokenizedInput.RemoveRange(indexOfStart, 3);
                tokenizedInput.Insert(indexOfStart, new WrappedExpressionFactor(expresssion));


            }
        }
    }
}
