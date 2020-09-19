using System;
using System.Collections.Generic;
using System.Linq;
using BinaryTag.Tags;

namespace BinaryTag
{
    public static class TagExtensions
    {
        public static ByteTag ToTag(this bool value)
        {
            return new ByteTag(value ? (byte) 1 : (byte) 0);
        }

        public static ByteTag ToTag(this byte value)
        {
            return new ByteTag(value);
        }

        public static ShortTag ToTag(this short value)
        {
            return new ShortTag(value);
        }

        public static IntTag ToTag(this int value)
        {
            return new IntTag(value);
        }

        public static LongTag ToTag(this long value)
        {
            return new LongTag(value);
        }

        public static FloatTag ToTag(this float value)
        {
            return new FloatTag(value);
        }

        public static DoubleTag ToTag(this double value)
        {
            return new DoubleTag(value);
        }

        public static CharTag ToTag(this char value)
        {
            return new CharTag(value);
        }

        public static StringTag ToTag(this string value)
        {
            return new StringTag(value);
        }

        public static StringTag ToTag(this Guid value)
        {
            return new StringTag(value.ToString());
        }

        public static ByteArrayTag ToTag(this byte[] value)
        {
            return new ByteArrayTag(value);
        }

        public static ByteArrayTag ToTag(this IEnumerable<byte> value)
        {
            return new ByteArrayTag(value.ToArray());
        }

        public static IntArrayTag ToTag(this int[] value)
        {
            return new IntArrayTag(value);
        }

        public static IntArrayTag ToTag(this IEnumerable<int> value)
        {
            return new IntArrayTag(value.ToArray());
        }

        public static LongArrayTag ToTag(this long[] value)
        {
            return new LongArrayTag(value);
        }

        public static LongArrayTag ToTag(this IEnumerable<long> value)
        {
            return new LongArrayTag(value.ToArray());
        }

        public static StringArrayTag ToTag(this string[] value)
        {
            return new StringArrayTag(value);
        }

        public static StringArrayTag ToTag(this IEnumerable<string> value)
        {
            return new StringArrayTag(value.ToArray());
        }

        public static ITag ToTag<TEnum>(this TEnum value) where TEnum : Enum
        {
            Type type = value.GetType().GetEnumUnderlyingType();
            if (type == typeof(byte))
                return new ByteTag((byte) Convert.ChangeType(value, TypeCode.Byte));
            else if (type == typeof(short))
                return new ShortTag((short) Convert.ChangeType(value, TypeCode.Int16));
            else if (type == typeof(int))
                return new IntTag((int) Convert.ChangeType(value, TypeCode.Int32));
            else if (type == typeof(long))
                return new LongTag((long) Convert.ChangeType(value, TypeCode.Int64));

            throw new NotSupportedException();
        }
    }
}