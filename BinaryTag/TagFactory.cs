using System;
using BinaryTag.Tags;

namespace BinaryTag
{
    public static class TagFactory
    {
        public static ITag CreateTag(TagType type)
        {
            switch (type)
            {
                case TagType.Byte:
                    return new ByteTag();

                case TagType.Char:
                    return new CharTag();

                case TagType.Double:
                    return new DoubleTag();

                case TagType.Float:
                    return new FloatTag();

                case TagType.Int:
                    return new IntTag();

                case TagType.List:
                    return new ListTag();

                case TagType.Long:
                    return new LongTag();

                case TagType.Map:
                    return new MapTag();

                case TagType.Short:
                    return new ShortTag();

                case TagType.String:
                    return new StringTag();

                case TagType.ByteArray:
                    return new ByteArrayTag();

                case TagType.IntArray:
                    return new IntArrayTag();

                case TagType.LongArray:
                    return new LongArrayTag();

                case TagType.StringArray:
                    return new StringArrayTag();

                default:
                    throw new NotSupportedException(type.ToString());
            }
        }
    }
}