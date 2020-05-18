using BinaryTag.Tags;

namespace BinaryTag
{
    public interface ITagConverter
    {
        MapTag ToTag();
        void FromTag(MapTag mapTag);
    }
}