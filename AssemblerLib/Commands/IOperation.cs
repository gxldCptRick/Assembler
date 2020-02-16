using AssemblerLib.Tokenizer.Tokens;

namespace AssemblerLib.Commands
{
    public interface IOperation : IToken
    {
        byte[] Encode();
    }
}
