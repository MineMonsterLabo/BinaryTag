using System.IO;
using BinaryTag.Tags;

namespace BinaryTag
{
    public static class IoExtensions
    {
        public static byte[] ToByteArray(this MapTag tag)
        {
            using (var stream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                tag.Write(writer);
                return stream.ToArray();
            }
        }

        public static MapTag ToMapTag(this byte[] buf)
        {
            using (var stream = new MemoryStream())
            using (BinaryReader reader = new BinaryReader(stream))
            {
                MapTag tag = new MapTag();
                tag.Read(reader);
                return tag;
            }
        }
    }
}