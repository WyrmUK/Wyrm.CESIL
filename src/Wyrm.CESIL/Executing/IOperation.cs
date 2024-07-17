using System.IO;

namespace Wyrm.CESIL.Executing
{
    internal interface IOperation
    {
        void Execute(object value, OperationState state, TextWriter writer);
    }
}
