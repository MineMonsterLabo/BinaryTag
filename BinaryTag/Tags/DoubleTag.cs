using System.IO;

namespace BinaryTag.Tags
{
    public class DoubleTag : ValueTag<double>
    {
        public override TagType Type => TagType.Double;

        public DoubleTag()
        {
        }

        public DoubleTag(double value) : base(value)
        {
        }

        public override void Read(BinaryReader reader)
        {
            Value = reader.ReadDouble();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Value);
        }

        public override object Clone()
        {
            return new DoubleTag(Value);
        }
    }
}