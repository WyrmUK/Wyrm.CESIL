using Wyrm.CESIL.Lexical;

namespace Wyrm.CESIL.Parsing
{
    public interface IInstructionBuilder
    {
        bool BuildInstruction(Token token, ref Instruction instruction, bool isData);
    }
}
