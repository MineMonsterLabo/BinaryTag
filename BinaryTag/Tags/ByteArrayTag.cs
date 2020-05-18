using BinaryIO;

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

        public override void Read(BinaryStream stream)
        {
            Value = stream.ReadBytes(stream.ReadInt());
        }

        public override void Write(BinaryStream stream)
        {
            stream.WriteInt(Value.Length);
            stream.WriteBytes(Value);
        }

        public override object Clone()
        {
            return new ByteArrayTag((byte[]) Value.Clone());
        }
    }
}