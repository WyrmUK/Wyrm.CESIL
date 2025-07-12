using Wyrm.CESIL.Parsing;

namespace Wyrm.CESIL.Executing
{
    internal interface IOperationFactory
    {
        IOperation CreateOperation(Instruction instruction);
    }
}
