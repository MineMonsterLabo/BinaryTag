using System.IO;

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

        public override void Read(BinaryReader reader)
        {
            int len = reader.Read7BitEncodedInt();
            Value = new long[len];
            for (int i = 0; i < len; i++)
                Value[i] = reader.ReadInt64();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write7BitEncodedInt(Value.Length);
            for (int i = 0; i < Value.Length; i++)
                writer.Write(Value[i]);
        }

        public override object Clone()
        {
            return new LongArrayTag((long[])Value.Clone());
        }
    }
}