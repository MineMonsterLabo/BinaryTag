using System.IO;

namespace BinaryTag.Tags
{
    public class ByteArrayTag : ArrayValueTag<byte>
    {
        public override TagType Type => TagType.ByteArray;

        public ByteArrayTag()
        {
        }

        public ByteArrayTag(byte[] value) : base(value)
        {
        }

        public override void Read(BinaryReader reader)
        {
            Value = reader.ReadBytes(reader.Read7BitEncodedInt());
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write7BitEncodedInt(Value.Length);
            writer.Write(Value);
        }

        public override object Clone()
        {
            return new ByteArrayTag((byte[])Value.Clone());
        }
    }
}