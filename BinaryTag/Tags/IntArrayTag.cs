using BinaryIO;

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

        public override void Read(BinaryStream stream)
        {
            int len = stream.ReadInt();
            Value = new int[len];
            for (int i = 0; i < len; i++)
                Value[i] = stream.ReadInt();
        }

        public override void Write(BinaryStream stream)
        {
            stream.WriteInt(Value.Length);
            for (int i = 0; i < Value.Length; i++)
                stream.WriteInt(Value[i]);
        }

        public override object Clone()
        {
            return new IntArrayTag((int[]) Value.Clone());
        }
    }
}