using BinaryIO;

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

        public override void Read(BinaryStream stream)
        {
            int len = stream.ReadInt();
            Value = new string[len];
            for (int i = 0; i < len; i++)
                Value[i] = stream.ReadStringUtf8();
        }

        public override void Write(BinaryStream stream)
        {
            stream.WriteInt(Value.Length);
            for (int i = 0; i < Value.Length; i++)
                stream.WriteStringUtf8(Value[i]);
        }

        public override object Clone()
        {
            return new StringArrayTag((string[]) Value.Clone());
        }
    }
}