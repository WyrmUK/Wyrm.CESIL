using System.Collections.Generic;
using System.IO;
using Wyrm.CESIL.Parsing;

namespace Wyrm.CESIL.Executing
{
    public interface IOperator
    {
        void Operate(Instruction instruction, OperationState state, TextWriter writer);
    }
}
