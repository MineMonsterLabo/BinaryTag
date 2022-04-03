using System.IO;

namespace BinaryTag.Tags
{
    public class ShortTag : ValueTag<short>
    {
        public override TagType Type => TagType.Short;

        public ShortTag()
        {
        }

        public ShortTag(short value) : base(value)
        {
        }

        public override void Read(BinaryReader reader)
        {
            Value = reader.ReadInt16();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Value);
        }

        public override object Clone()
        {
            return new ShortTag(Value);
        }
    }
}