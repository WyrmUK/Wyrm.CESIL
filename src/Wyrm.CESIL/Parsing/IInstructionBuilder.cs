using Wyrm.CESIL.Lexical;

namespace Wyrm.CESIL.Parsing
{
    /// <summary>
    /// Interface to an Instruction Builder.
    /// </summary>
    public interface IInstructionBuilder
    {
        /// <summary>
        /// Builds an instruction from a token.
        /// </summary>
        /// <param name="token">The <see cref="Token"/> to build from.</param>
        /// <param name="instruction">Receives the <see cref="Instruction"/> built.</param>
        /// <param name="isData">Indicates if this is data or not.</param>
        /// <returns>True if an <see cref="Instruction"/> could be built.</returns>
        /// <exception cref="Exceptions.SyntaxException">Thrown if there is an error in syntax.</exception>
        /// <exception cref="System.NotSupportedException">Thrown if the token type isn't found.</exception>
        /// <exception cref="Exceptions.IncompleteInstructionException">Thrown if the instruction can't be completed.</exception>
        /// <exception cref="Exceptions.IllegalIntegerException">Thrown if the data value is not a proper integer.</exception>
        /// <exception cref="Exceptions.IllegalDataException">Thrown if the data is not valid.</exception>
        /// <exception cref="Exceptions.IllegalInstructionException">Thrown if the instruction can't be found or is in the wrong place.</exception>
        /// <exception cref="Exceptions.IllegalLabelException">Thrown if a label is not valid.</exception>
        /// <exception cref="Exceptions.IllegalLocationException">Thrown if a token is not at the right location.</exception>
        bool BuildInstruction(Token token, ref Instruction instruction, bool isData);
    }
}
