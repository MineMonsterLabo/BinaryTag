using System.IO;

namespace BinaryTag.Tags
{
    public class LongTag : ValueTag<long>
    {
        public override TagType Type => TagType.Long;

        public LongTag()
        {
        }

        public LongTag(long value) : base(value)
        {
        }

        public override void Read(BinaryReader reader)
        {
            Value = reader.ReadInt64();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Value);
        }

        public override object Clone()
        {
            return new LongTag(Value);
        }
    }
}