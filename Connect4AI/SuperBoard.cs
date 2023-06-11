using connect_4_core;
using System.Collections;

namespace Connect4AI
{
    public record SuperBoard(IBoard Board, PlaySequenceSet PlaySequenceSet);
    public class PlaySequence : IEnumerable<uint>
    {
        List<uint> sequence = new List<uint>();

        public PlaySequence()
        {
        }

        public PlaySequence(PlaySequence other )
        {
            this.sequence = new List<uint>(other.sequence);
        }


        private void Invariant()
        {
            if (sequence.Count > 42)
            {
                throw new Exception("play sequence can't be longer than 42");
            }

            foreach (var col in sequence)
            {
                if (col > 6) {
                    throw new Exception("column can't be larger than 6");

                }
            }
        }

        public void Add(uint col)
        {
            sequence.Add(col);
            Invariant();

        }
        public override string ToString()
        {
            var a = sequence.ToArray();
            var s = "[" + string.Join(", ", a) + "]";
            return s;
        }

        public IEnumerator<uint> GetEnumerator()
        {
            return sequence.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return sequence.GetEnumerator();
        }
    }
    public class PlaySequenceSet : IEnumerable<PlaySequence>
    {
        HashSet<PlaySequence> sequences = new HashSet<PlaySequence>();
        public PlaySequenceSet() { 
        }

        public PlaySequenceSet(PlaySequenceSet ps)
        {   
            foreach (var seq  in ps.sequences)
            {
                sequences.Add(new PlaySequence(seq));
            }
        }

        public int Count { get { return sequences.Count; } }

        public override string ToString()
        {
            IEnumerable<PlaySequence> a = sequences.ToArray();
            var s = "[" + string.Join(", ", a) + "]";
            return s;
        }

        public void Add(PlaySequence ps)
        {
            sequences.Add(ps);
        }
        public void Merge(PlaySequenceSet other)
        {
            sequences.Union(other.sequences);
        }

        public IEnumerator<PlaySequence> GetEnumerator()
        {
            return sequences.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return sequences.GetEnumerator();
        }
    }
}
