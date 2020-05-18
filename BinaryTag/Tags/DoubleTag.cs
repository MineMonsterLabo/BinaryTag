using BinaryIO;

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

        public override void Read(BinaryStream stream)
        {
            Value = stream.ReadDouble();
        }

        public override void Write(BinaryStream stream)
        {
            stream.WriteDouble(Value);
        }

        public override object Clone()
        {
            return new DoubleTag(Value);
        }
    }
}