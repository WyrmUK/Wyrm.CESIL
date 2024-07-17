using System.IO;
using Wyrm.CESIL.Exceptions;

namespace Wyrm.CESIL.Executing
{
    internal class PrintOperation : IOperation
    {
        public void Execute(object value, OperationState state, TextWriter writer)
        {
            if (value is string && ((string)value).StartsWith("\"") && ((string)value).EndsWith("\"")) writer.Write(((string)value).Substring(1, ((string)value).Length - 2).Replace("\"\"", "\""));
            else throw new IllegalOperationException("Unknown data type for PRINT.");
            ++state.Instruction;
        }
    }
}
