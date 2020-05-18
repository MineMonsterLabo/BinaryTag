using BinaryIO;

namespace BinaryTag.Tags
{
    public class FloatTag : ValueTag<float>
    {
        public override TagType Type => TagType.Float;

        public FloatTag()
        {
        }

        public FloatTag(float value) : base(value)
        {
        }

        public override void Read(BinaryStream stream)
        {
            Value = stream.ReadFloat();
        }

        public override void Write(BinaryStream stream)
        {
            stream.WriteFloat(Value);
        }

        public override object Clone()
        {
            return new FloatTag(Value);
        }
    }
}