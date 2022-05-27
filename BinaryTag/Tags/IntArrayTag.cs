using System.IO;

namespace BinaryTag.Tags
{
    public class IntArrayTag : ArrayValueTag<int>
    {
        public override TagType Type => TagType.IntArray;

        public IntArrayTag()
        {
        }

        public IntArrayTag(int[] value) : base(value)
        {
        }

        public override void Read(BinaryReader reader)
        {
#if NETSTANDARD
            var len = reader.Read7BitEncodedIntPolyfill();
#else
            int len = reader.Read7BitEncodedInt();
#endif
            Value = new int[len];
            for (int i = 0; i < len; i++)
                Value[i] = reader.ReadInt32();
        }

        public override void Write(BinaryWriter writer)
        {
#if NETSTANDARD
            writer.Write7BitEncodedIntPolyfill(Value.Length);
#else
            writer.Write7BitEncodedInt(Value.Length);
#endif
            for (int i = 0; i < Value.Length; i++)
                writer.Write(Value[i]);
        }

        public override object Clone()
        {
            return new IntArrayTag((int[])Value.Clone());
        }
    }
}