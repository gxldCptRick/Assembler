using AssemblerLib.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Assembler_Andres_Carrera
{
    public class FileCompiler
    {
        public void CompileFromPath(string path)
        {
            var fileContents = File.ReadAllText(path);
            var compiler = new AssemblyParser();
            try
            {
                var program = compiler.Parse(fileContents);
                var filePath = $"{path.Substring(0, path.LastIndexOf("."))}.bin";
                using (var writer = new FileStream(filePath, FileMode.Create))
                {
                    foreach (var byteCode in program.Compile())
                    {
                        writer.WriteByte(byteCode);
                    }
                }
                Console.WriteLine($"Saved binary file to {filePath}");
            }
            catch (ArgumentException e)
            {
                Console.Error.WriteLine($"error: {e.Message}");
            }


        }
    }
}
