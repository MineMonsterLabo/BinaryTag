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
#if NETSTANDARD
            var len = reader.Read7BitEncodedIntPolyfill();
#else
            int len = reader.Read7BitEncodedInt();
#endif
            Value = reader.ReadBytes(len);
        }

        public override void Write(BinaryWriter writer)
        {
#if NETSTANDARD
            writer.Write7BitEncodedIntPolyfill(Value.Length);
#else
            writer.Write7BitEncodedInt(Value.Length);
#endif
            writer.Write(Value);
        }

        public override object Clone()
        {
            return new ByteArrayTag((byte[])Value.Clone());
        }
    }
}