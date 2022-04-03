using System.IO;

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

        public override void Read(BinaryReader reader)
        {
            Value = reader.ReadSingle();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Value);
        }

        public override object Clone()
        {
            return new FloatTag(Value);
        }
    }
}