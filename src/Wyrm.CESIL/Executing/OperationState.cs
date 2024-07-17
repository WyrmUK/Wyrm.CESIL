using System.Collections.Generic;
using Wyrm.CESIL.Exceptions;

namespace Wyrm.CESIL.Executing
{
    internal class OperationState : IOperationState
    {
        private readonly IList<long> dataSet;
        private readonly IDictionary<string, int> labels;
        private readonly Dictionary<string, long> stores = new Dictionary<string, long>();

        public OperationState(IList<long> dataSet, IDictionary<string, int> labels)
        {
            this.dataSet = dataSet;
            this.labels = labels;
        }
        public long PopData()
        {
            if (dataSet.Count == 0) throw new NoDataException();
            var value = dataSet[0];
            dataSet.RemoveAt(0);
            return value;
        }
        public int InstructionFor(string label)
        {
            if (!labels.ContainsKey(label)) throw new IllegalLabelException();
            return labels[label];
        }
        public long Accumulator { get; set; }
        public int Instruction { get; set; }
        public bool Halted { get; set; }
        public long this[string variable]
        {
            get
            {
                if (!stores.ContainsKey(variable)) throw new NotInitialisedException();
                return stores[variable];
            }
            set
            {
                if (!stores.ContainsKey(variable)) stores.Add(variable, value);
                else stores[variable] = value;
            }
        }
    }
}
