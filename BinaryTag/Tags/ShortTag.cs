using BinaryIO;

namespace BinaryTag.Tags
{
    public class ShortTag : ValueTag<short>
    {
        public override TagType Type => TagType.Short;

        public ShortTag()
        {
        }

        public ShortTag(short value) : base(value)
        {
        }

        public override void Read(BinaryStream stream)
        {
            Value = stream.ReadShort();
        }

        public override void Write(BinaryStream stream)
        {
            stream.WriteShort(Value);
        }

        public override object Clone()
        {
            return new ShortTag(Value);
        }
    }
}