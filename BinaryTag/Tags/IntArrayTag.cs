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
            int len = reader.ReadInt32();
            Value = new int[len];
            for (int i = 0; i < len; i++)
                Value[i] = reader.ReadInt32();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Value.Length);
            for (int i = 0; i < Value.Length; i++)
                writer.Write(Value[i]);
        }

        public override object Clone()
        {
            return new IntArrayTag((int[])Value.Clone());
        }
    }
}