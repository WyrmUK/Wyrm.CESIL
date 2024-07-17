using System.Collections.Generic;
using Wyrm.CESIL.Lexical;
using Wyrm.CESIL.Parsing;

namespace Wyrm.CESIL
{
    /// <summary>
    /// An interface to a Parser to parse language tokens.
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Parses language <see cref="Token"/>s and <see cref="long"/> data elements.
        /// </summary>
        /// <param name="tokens">An <see cref="IEnumerable{T}"/> of <see cref="Token"/>s to parse.</param>
        /// <param name="data">An <see cref="IList{T}"/> of <see cref="long"/> data items to use.</param>
        /// <param name="errors">An <see cref="IList{T}"/> to be populated with <see cref="SyntaxError"/>s.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Instruction"/>s.</returns>
        IEnumerable<Instruction> Parse(IEnumerable<Token> tokens, IList<long> data, IList<SyntaxError> errors);
    }
}
