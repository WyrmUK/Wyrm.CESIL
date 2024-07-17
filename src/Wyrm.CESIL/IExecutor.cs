using System.Collections.Generic;
using System.IO;
using System.Threading;
using Wyrm.CESIL.Lexical;
using Wyrm.CESIL.Parsing;

namespace Wyrm.CESIL
{
    public interface IExecutor
    {
        void Prepare(IList<Instruction> instructionSet, IList<SyntaxError> errors, CancellationToken cancellationToken);
        void Run(IList<long> dataSet, TextWriter writer, CancellationToken cancellationToken);
    }
}
