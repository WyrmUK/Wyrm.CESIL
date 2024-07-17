namespace Wyrm.CESIL.Parsing
{
    /// <summary>
    /// An enumeration of CESIL instruction types.
    /// </summary>
    public enum InstructionType
    {
        /// <summary>
        /// LOAD
        /// </summary>
        LOAD,
        /// <summary>
        /// STORE
        /// </summary>
        STORE,
        /// <summary>
        /// IN
        /// </summary>
        IN,
        /// <summary>
        /// ADD
        /// </summary>
        ADD,
        /// <summary>
        /// SUBTRACT
        /// </summary>
        SUBTRACT,
        /// <summary>
        /// MULTIPLY
        /// </summary>
        MULTIPLY,
        /// <summary>
        /// DIVIDE
        /// </summary>
        DIVIDE,
        /// <summary>
        /// JUMP
        /// </summary>
        JUMP,
        /// <summary>
        /// JIZERO
        /// </summary>
        JIZERO,
        /// <summary>
        /// JINEG
        /// </summary>
        JINEG,
        /// <summary>
        /// PRINT
        /// </summary>
        PRINT,
        /// <summary>
        /// OUT
        /// </summary>
        OUT,
        /// <summary>
        /// LINE
        /// </summary>
        LINE,
        /// <summary>
        /// HALT
        /// </summary>
        HALT
    }
}
