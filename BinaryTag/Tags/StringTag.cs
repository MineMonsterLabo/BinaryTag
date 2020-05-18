using BinaryIO;

namespace BinaryTag.Tags
{
    public class StringTag : ValueTag<string>
    {
        public override TagType Type => TagType.String;

        public StringTag()
        {
            Value = string.Empty;
        }

        public StringTag(string value) : base(value)
        {
            if (string.IsNullOrWhiteSpace(value))
                Value = string.Empty;
        }

        public override void Read(BinaryStream stream)
        {
            Value = stream.ReadStringUtf8();
        }

        public override void Write(BinaryStream stream)
        {
            stream.WriteStringUtf8(Value);
        }

        public override object Clone()
        {
            return new StringTag((string) Value.Clone());
        }
    }
}