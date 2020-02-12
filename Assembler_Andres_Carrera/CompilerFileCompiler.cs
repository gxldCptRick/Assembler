using AssemblerLib.Commands.MoveTopBottom;
using AssemblerLib.Compiler;
using AssemblerLib.Compiler.CompilationTokens.BoostedTokens;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Assembler_Andres_Carrera
{
    internal class CompilerFileCompiler
    {
        internal void CompileToPathFromPath(string fromPath, string templatePath, string toPath, ProgramToken stackInit, string assemblyOutFile)
        {
            var fileContents = File.ReadAllText(fromPath);
            var assemblyPath =  assemblyOutFile ?? $"{fromPath.Substring(0, fromPath.LastIndexOf("."))}.asm";
            var compiler = new Compiler((p) => File.WriteAllText(assemblyPath + ".compiled.asm", p.Content));
            var program = compiler.Compile(fileContents);
            using (var writer = new FileStream(toPath, FileMode.Create))
            {
                
                foreach (var byteCode in stackInit.Compile())
                {
                    writer.WriteByte(byteCode);
                }
                foreach (var byteCode in program.Compile())
                {
                    writer.WriteByte(byteCode);
                }
                var assembly = new AssemblyParser();
                var assembledTemplate = assembly.ParseAssembly(File.ReadAllText(templatePath));
                foreach (var byteCode in assembledTemplate.Compile())
                {
                    writer.WriteByte(byteCode);
                }
                File.WriteAllLines(assemblyPath, new string[] { stackInit.Content, program.Content,assembledTemplate.Content });
            }
            Console.WriteLine($"Saved binary file to {toPath}");
        }
    }
}
