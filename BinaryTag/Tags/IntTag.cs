using System.IO;

namespace BinaryTag.Tags
{
    public class IntTag : ValueTag<int>
    {
        public override TagType Type => TagType.Int;

        public IntTag()
        {
        }

        public IntTag(int value) : base(value)
        {
        }

        public override void Read(BinaryReader reader)
        {
            Value = reader.ReadInt32();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Value);
        }

        public override object Clone()
        {
            return new IntTag(Value);
        }
    }
}