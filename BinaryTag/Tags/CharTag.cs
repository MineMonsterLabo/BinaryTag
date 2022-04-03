using System.IO;

namespace BinaryTag.Tags
{
    public class CharTag : ValueTag<char>
    {
        public override TagType Type => TagType.Char;

        public CharTag()
        {
        }

        public CharTag(char value) : base(value)
        {
        }

        public override void Read(BinaryReader reader)
        {
            Value = reader.ReadChar();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Value);
        }

        public override object Clone()
        {
            return new CharTag(Value);
        }
    }
}