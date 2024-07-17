using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Wyrm.CESIL.Lexical;

namespace Wyrm.CESIL
{
    /// <summary>
    /// An interface to a Lexical Analyser.
    /// </summary>
    public interface ILexer
    {
        /// <summary>
        /// Analyses a program returning parseable tokens and listing all syntax errors.
        /// </summary>
        /// <param name="reader">The <see cref="TextReader"/> to read the program from.</param>
        /// <param name="errors">The <see cref="IList{T}"/> to write <see cref="SyntaxError"/>s to.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of parseable <see cref="Token"/>s.</returns>
        IEnumerable<Token> Analyse(TextReader reader, IList<SyntaxError> errors);
        /// <summary>
        /// Analyses a program returning parseable tokens and listing all syntax errors.
        /// </summary>
        /// <param name="reader">The <see cref="TextReader"/> to read the program from.</param>
        /// <param name="errors">The <see cref="IList{T}"/> to write <see cref="SyntaxError"/>s to.</param>
        /// <param name="cancellationToken">A token to cancel the analysis.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of parseable <see cref="Token"/>s.</returns>
        Task<IEnumerable<Token>> AnalyseAsync(TextReader reader, IList<SyntaxError> errors, CancellationToken cancellationToken);
    }
}
