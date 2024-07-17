using System.Collections.Generic;
using System.Threading;
using Wyrm.CESIL.Lexical;
using Wyrm.CESIL.Parsing;

namespace Wyrm.CESIL
{
    public interface IParser
    {
        IEnumerable<Instruction> Parse(IEnumerable<Token> tokens, IList<long> data, IList<SyntaxError> errors, CancellationToken cancellationToken);
    }
}
