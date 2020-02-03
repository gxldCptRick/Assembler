using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Commands.DataProcessing
{
    internal static class AlphaNumericExtensions
    {
        public static int Length(this AlphaNumeric an) => an.Content.Length;
        public static string SubString(this AlphaNumeric an, int startIndex, int length = -1) => an.Content.Substring(startIndex, length);
    }
}
