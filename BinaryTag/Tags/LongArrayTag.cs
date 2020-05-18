using BinaryIO;

namespace BinaryTag.Tags
{
    public class LongArrayTag : ArrayValueTag<long>
    {
        public override TagType Type => TagType.LongArray;

        public LongArrayTag()
        {
        }

        public LongArrayTag(long[] value) : base(value)
        {
        }

        public override void Read(BinaryStream stream)
        {
            int len = stream.ReadInt();
            Value = new long[len];
            for (int i = 0; i < len; i++)
                Value[i] = stream.ReadLong();
        }

        public override void Write(BinaryStream stream)
        {
            stream.WriteInt(Value.Length);
            for (int i = 0; i < Value.Length; i++)
                stream.WriteLong(Value[i]);
        }

        public override object Clone()
        {
            return new LongArrayTag((long[]) Value.Clone());
        }
    }
}