using AssemblerLib.Parser;
using System;

namespace Assembler_Andres_Carrera
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var argParser = new ArgParser();
            var parsedMap = argParser.ParseArguments(args);
            if (parsedMap["mode"] == "asm")
            {
                new AssemblyFileCompiler().AssemblyToPathFromPath(parsedMap["input"], parsedMap["output"]);
            }
            else if (parsedMap["mode"] == "help")
            {
                Console.WriteLine("Usage: 'asm path_to_assembly <-o path_to_output>?' to assemble assembly file");
                Console.WriteLine("Usage: 'asm path_to_text -c path_to_template <-o path_to_output>? <-s ## or 0x##>?' to compile txt file into both assembly and binary");
            }
            else
            {
                parsedMap.TryGetValue("assemblyOutFile", out var assemblyOutFile);
                new CompilerFileCompiler().CompileToPathFromPath(parsedMap["input"], parsedMap["template"], parsedMap["output"], new AssemblyParser().ParseAssembly(parsedMap["stack"]), assemblyOutFile);
            }
        }
    }
}
