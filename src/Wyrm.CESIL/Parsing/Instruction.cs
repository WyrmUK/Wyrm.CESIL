namespace Wyrm.CESIL.Parsing
{
    public class Instruction
    {
        public Instruction(int lineNo)
        {
            LineNo = lineNo;
        }
        public int LineNo { get; }
        public string Label { get; set; }
        public InstructionType? InstructionType { get; set; }
        public object Value { get; set; }
    }
}
