using BinaryIO;

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

        public override void Read(BinaryStream stream)
        {
            Value = stream.ReadLong();
        }

        public override void Write(BinaryStream stream)
        {
            stream.WriteLong(Value);
        }

        public override object Clone()
        {
            return new LongTag(Value);
        }
    }
}