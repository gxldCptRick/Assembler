using AssemblerLib.Parser;
using System;
using System.IO;

namespace Assembler_Andres_Carrera
{
    public class AssemblyFileCompiler
    {
        public void AssemblyToPathFromPath(string fromPath, string toPath)
        {
            var fileContents = File.ReadAllText(fromPath);
            var assembler = new AssemblyParser();
            var program = assembler.ParseAssembly(fileContents);
            using (var writer = new FileStream(toPath, FileMode.Create))
            {
                foreach (var byteCode in program.Compile())
                {
                    writer.WriteByte(byteCode);
                }
            }
            Console.WriteLine($"Saved binary file to {toPath}");
        }
    }
}
