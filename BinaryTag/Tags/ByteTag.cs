using System.IO;

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

        public override void Read(BinaryReader reader)
        {
            Value = reader.ReadByte();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Value);
        }

        public override object Clone()
        {
            return new ByteTag(Value);
        }
    }
}