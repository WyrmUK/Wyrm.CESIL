using System.Collections.Generic;
using System.IO;
using System.Threading;
using Wyrm.CESIL.Lexical;

namespace Wyrm.CESIL
{
    public interface ILexer
    {
        IEnumerable<Token> Analyse(TextReader reader, IList<SyntaxError> errors, CancellationToken cancellationToken);
    }
}
