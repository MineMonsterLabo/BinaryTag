using BinaryIO;
using BinaryTag.Tags;

namespace BinaryTag
{
    public static class IoExtensions
    {
        public static byte[] ToByteArray(this MapTag tag)
        {
            using (BinaryStream stream = new BinaryStream())
            {
                tag.Write(stream);
                return stream.ToArray();
            }
        }

        public static MapTag ToMapTag(this byte[] buf)
        {
            using (BinaryStream stream = new BinaryStream(buf))
            {
                MapTag tag = new MapTag();
                tag.Read(stream);
                return tag;
            }
        }
    }
}