using System.IO;

namespace BinaryTag.Tags
{
    public class StringArrayTag : ArrayValueTag<string>
    {
        public override TagType Type => TagType.StringArray;

        public StringArrayTag()
        {
        }

        public StringArrayTag(string[] value) : base(value)
        {
        }

        public override void Read(BinaryReader reader)
        {
#if NETSTANDARD
            var len = reader.Read7BitEncodedIntPolyfill();
#else
            int len = reader.Read7BitEncodedInt();
#endif
            Value = new string[len];
            for (int i = 0; i < len; i++)
                Value[i] = reader.ReadString();
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
            return new StringArrayTag((string[])Value.Clone());
        }
    }
}