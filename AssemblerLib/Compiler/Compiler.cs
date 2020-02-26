using AssemblerLib.Compiler.CompilationTokens;
using AssemblerLib.Compiler.CompilationTokens.Meta;
using AssemblerLib.Compiler.CompilationTokens.Rules;
using AssemblerLib.Compiler.CompilationTokens.Tokens;
using AssemblerLib.Exceptions;
using AssemblerLib.Grammar_Rules;
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
        private IEnumerable<IConditionalRule> _compilerProccessingEngine;
        private IEnumerable<IGrammerRule> _preProccessingEngine;
        private IEnumerable<IGrammerRule> _postProccessingEngine;
        private SymbolTable _globalTable;
        public Compiler(Action<Program> callbackHook, SymbolTable table=null)
        {
            _callbackHook = callbackHook;
            _globalTable = table ?? new SymbolTable(0xFB0);
            _preProccessingEngine = new IGrammerRule[]
            {
                new FactorSubstituteRule(),
                new FunctionKeywordRule(),
                new FunctionDeclarationRule(_globalTable),
                new VariableDeclarationRule(_globalTable)
            };
            _compilerProccessingEngine = new IConditionalRule[]
            {
                new FactorWrappingExpression(),
                new CompilerTrackedRule(_globalTable),
                new FunctionFactorRule(),
                new VariableFactorRule(),
                new TermFactorMultiplicationRule(),
                new TermFactorRule(),
                new ExpressionAddTermRule(),
                new ExpressionTermRule(),
            };
            _postProccessingEngine = new IGrammerRule[]
            {
                new VariableAssignmentRule(),
                new ConstantRule(),
                new FunctionRule(),
                new FinalProgramRule()
            };
        }

        private readonly Action<Program> _callbackHook;
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

        private Stack<IToken> PreproccessStack(IEnumerable<IToken> tokens)
        {
            var fullStack = new Stack<IToken>(tokens);
            foreach (var rule in _preProccessingEngine)
            {
                fullStack = rule.ReduceStack(fullStack);
            }
            return new Stack<IToken>(fullStack);
        }

        public ProgramToken Compile(string input, ProgramToken injectedProgram=null)
        {
            
            var initialTokenizedInput = new Tokenizer.Tokenizer().Tokenize(input);
            var tokenizedInput = PreproccessStack(initialTokenizedInput).ToList();
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
            foreach (var rule in _postProccessingEngine)
            {
                stack = rule.ReduceStack(stack);
            }
            

            if (stack.Count != 1)
            {
                throw new InvalidStack(stack, $"Stack should have reduced to one not {stack.Count}");
            }

            if (!(stack.Peek() is Program))
            {
                throw new InvalidStack(stack, $"Stack should only have {nameof(Program)}");
            }

            var rawProgram = stack.Pop() as Program;
            _callbackHook?.Invoke(rawProgram);
            rawProgram.InjectProgram(injectedProgram);
            var program = rawProgram.Assemble();
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
