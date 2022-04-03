using System.IO;

namespace BinaryTag.Tags
{
    public class StringArrayTag : ArrayValueTag<string>
    {
        public override TagType Type => TagType.StringArray;

        public StringArrayTag()
        {
        }

        public StringArrayTag(string[] value) : base(value)
        {
        }

        public override void Read(BinaryReader reader)
        {
            int len = reader.ReadInt32();
            Value = new string[len];
            for (int i = 0; i < len; i++)
                Value[i] = reader.ReadString();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Value.Length);
            for (int i = 0; i < Value.Length; i++)
                writer.Write(Value[i]);
        }

        public override object Clone()
        {
            return new StringArrayTag((string[])Value.Clone());
        }
    }
}