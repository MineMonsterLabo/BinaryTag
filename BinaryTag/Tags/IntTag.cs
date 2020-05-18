using BinaryIO;

namespace BinaryTag.Tags
{
    public class IntTag : ValueTag<int>
    {
        public override TagType Type => TagType.Int;

        public IntTag()
        {
        }

        public IntTag(int value) : base(value)
        {
        }

        public override void Read(BinaryStream stream)
        {
            Value = stream.ReadInt();
        }

        public override void Write(BinaryStream stream)
        {
            stream.WriteInt(Value);
        }

        public override object Clone()
        {
            return new IntTag(Value);
        }
    }
}