using System.IO;

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

        public override void Read(BinaryReader reader)
        {
            Value = reader.ReadString();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Value);
        }

        public override object Clone()
        {
            return new StringTag((string)Value.Clone());
        }
    }
}