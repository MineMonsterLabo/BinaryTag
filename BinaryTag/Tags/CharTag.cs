using BinaryIO;

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

        public override void Read(BinaryStream stream)
        {
            Value = (char) stream.ReadByte();
        }

        public override void Write(BinaryStream stream)
        {
            stream.WriteByte((byte) Value);
        }

        public override object Clone()
        {
            return new CharTag(Value);
        }
    }
}