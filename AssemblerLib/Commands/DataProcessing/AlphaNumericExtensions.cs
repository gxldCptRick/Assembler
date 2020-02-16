using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Commands.DataProcessing
{
    internal static class AlphaNumericExtensions
    {
        public static int Length(this AlphaNumeric an)
        {
            return an.Content.Length;
        }

        public static string SubString(this AlphaNumeric an, int startIndex, int length = -1)
        {
            return an.Content.Substring(startIndex, length);
        }
    }
}
