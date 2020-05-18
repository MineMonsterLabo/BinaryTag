using BinaryIO;

namespace BinaryTag.Tags
{
    public class ByteTag : ValueTag<byte>
    {
        public override TagType Type => TagType.Byte;

        public ByteTag()
        {
        }

        public ByteTag(byte value) : base(value)
        {
        }

        public override void Read(BinaryStream stream)
        {
            Value = stream.ReadByte();
        }

        public override void Write(BinaryStream stream)
        {
            stream.WriteByte(Value);
        }

        public override object Clone()
        {
            return new ByteTag(Value);
        }
    }
}