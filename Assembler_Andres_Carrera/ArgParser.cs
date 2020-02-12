using AssemblerLib.Commands.MoveTopBottom;
using AssemblerLib.Compiler.CompilationTokens.BoostedTokens;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assembler_Andres_Carrera
{
    public class ArgParser
    {
        private static ProgramToken defaultProgam = StackInitialAddress(0x0);
        private static string ResolvePath(string path) => Path.IsPathFullyQualified(path) ? path : Path.Join(Environment.CurrentDirectory, path);
        public IDictionary<string, string> ParseArguments(string[] args)
        {
            if (args.Length < 1) throw new ArgumentException("Must pass in at least the file path to the assembly!!");
            var parsedArgs = DefaultDictionary();
            if (args.Contains("-h"))
            {
                parsedArgs["mode"] = "help";
            }
            else
            {
                args[0] = ResolvePath(args[0]);
                parsedArgs["input"] = args[0];
                var groups = new Dictionary<string, string>();
                for (int i = 1; i + 1 < args.Length; i++)
                {
                    groups.Add(args[i], args[i + 1]);
                }
                var defaultOutFile = args[0].Substring(0, args[0].LastIndexOf(".")) + ".bin";
                parsedArgs["output"] = ResolvePath(groups.GetValueOrDefault("-o", defaultOutFile));
                if (groups.TryGetValue("-c", out string templateFile))
                {
                    parsedArgs["mode"] = "txt";
                    parsedArgs["template"] = ResolvePath(templateFile);
                }
                if (groups.TryGetValue("-s", out string stackValue))
                {
                    var tokenizer = new Tokenizer();
                    var proccesed = tokenizer.Tokenize(stackValue).First();
                    try
                    {
                        parsedArgs["stack"] = StackInitialAddress((proccesed as NumericToken).Value).Content;
                    }
                    catch (NullReferenceException e)
                    {
                        throw new ArgumentException("Must put in a valid number for the stack", e);
                    }
                }

                if(groups.TryGetValue("-f", out string outAssemblyFile))
                {
                    parsedArgs["assemblyOutFile"] = ResolvePath(outAssemblyFile);
                }
            }
            return parsedArgs;
        }


        private static Dictionary<string, string> DefaultDictionary()
        {
            return new Dictionary<string, string>() { { "stack", defaultProgam.Content }, { "mode", "asm" } };
        }

        private static ProgramToken StackInitialAddress(int addr)
        {
            int first = 0xFF_FF;
            int word = addr & first;
            int top = (addr >> 16) & first;
            var stackPointerRegsiter = new RegisterToken("R13");
            return new ProgramToken(new InstructionToken[]
                {
                    new InstructionToken(new MoveTopToken(Condition.AL, stackPointerRegsiter, new RawNumericToken(top))),
                    new InstructionToken(new MoveWordToken(Condition.AL, stackPointerRegsiter, new RawNumericToken(word))),
                });
        }
    }
}
