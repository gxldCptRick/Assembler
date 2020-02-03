using System;
using System.IO;

namespace Assembler_Andres_Carrera
{
    class Program
    {
        static void Main(string[] args)
        {
            string path;
            if(args.Length > 0)
            {
                path = args[0];
                if (!path.ToLower().StartsWith("c:"))
                {
                    path = Path.Join(Environment.CurrentDirectory, path);
                }
            }
            else
            {
                // user input to get path.
                path = "USER INPUT NOT SET";
            }

            new FileCompiler().CompileFromPath(path);
        }
    }
}
