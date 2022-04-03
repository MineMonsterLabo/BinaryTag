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
            return new ByteTag(value ? (byte)1 : (byte)0);
        }

        public static bool ToBoolValue(this ITag tag)
        {
            if (tag is ByteTag t)
            {
                return t.Value == 0;
            }

            throw new InvalidCastException();
        }

        public static ByteTag ToTag(this byte value)
        {
            return new ByteTag(value);
        }

        public static byte ToByteValue(this ITag tag)
        {
            if (tag is ByteTag t)
            {
                return t.Value;
            }

            throw new InvalidCastException();
        }

        public static ShortTag ToTag(this short value)
        {
            return new ShortTag(value);
        }

        public static short ToShortValue(this ITag tag)
        {
            if (tag is ShortTag t)
            {
                return t.Value;
            }

            throw new InvalidCastException();
        }

        public static IntTag ToTag(this int value)
        {
            return new IntTag(value);
        }

        public static int ToIntValue(this ITag tag)
        {
            if (tag is IntTag t)
            {
                return t.Value;
            }

            throw new InvalidCastException();
        }

        public static LongTag ToTag(this long value)
        {
            return new LongTag(value);
        }

        public static long ToLongValue(this ITag tag)
        {
            if (tag is LongTag t)
            {
                return t.Value;
            }

            throw new InvalidCastException();
        }

        public static FloatTag ToTag(this float value)
        {
            return new FloatTag(value);
        }

        public static float ToIFloatalue(this ITag tag)
        {
            if (tag is FloatTag t)
            {
                return t.Value;
            }

            throw new InvalidCastException();
        }

        public static DoubleTag ToTag(this double value)
        {
            return new DoubleTag(value);
        }

        public static double ToDoubleValue(this ITag tag)
        {
            if (tag is DoubleTag t)
            {
                return t.Value;
            }

            throw new InvalidCastException();
        }

        public static CharTag ToTag(this char value)
        {
            return new CharTag(value);
        }

        public static char ToCharValue(this ITag tag)
        {
            if (tag is CharTag t)
            {
                return t.Value;
            }

            throw new InvalidCastException();
        }

        public static StringTag ToTag(this string value)
        {
            return new StringTag(value);
        }

        public static string ToStringValue(this ITag tag)
        {
            if (tag is StringTag t)
            {
                return t.Value;
            }

            throw new InvalidCastException();
        }

        public static StringTag ToTag(this Guid value)
        {
            return new StringTag(value.ToString());
        }

        public static Guid ToGuidValue(this ITag tag)
        {
            if (tag is StringTag t)
            {
                return Guid.Parse(t.Value);
            }

            throw new InvalidCastException();
        }

        public static ByteArrayTag ToTag(this byte[] value)
        {
            return new ByteArrayTag(value);
        }

        public static byte[] ToByteArrayValue(this ITag tag)
        {
            if (tag is ByteArrayTag t)
            {
                return t.Value;
            }

            throw new InvalidCastException();
        }

        public static ByteArrayTag ToTag(this IEnumerable<byte> value)
        {
            return new ByteArrayTag(value.ToArray());
        }

        public static IEnumerable<byte> ToByteEnumerableValue(this ITag tag)
        {
            if (tag is ByteArrayTag t)
            {
                return t.Value;
            }

            throw new InvalidCastException();
        }

        public static IntArrayTag ToTag(this int[] value)
        {
            return new IntArrayTag(value);
        }

        public static int[] ToIntArrayValue(this ITag tag)
        {
            if (tag is IntArrayTag t)
            {
                return t.Value;
            }

            throw new InvalidCastException();
        }

        public static IntArrayTag ToTag(this IEnumerable<int> value)
        {
            return new IntArrayTag(value.ToArray());
        }

        public static IEnumerable<int> ToIntEnumerableValue(this ITag tag)
        {
            if (tag is IntArrayTag t)
            {
                return t.Value;
            }

            throw new InvalidCastException();
        }

        public static LongArrayTag ToTag(this long[] value)
        {
            return new LongArrayTag(value);
        }

        public static long[] ToLongArrayValue(this ITag tag)
        {
            if (tag is LongArrayTag t)
            {
                return t.Value;
            }

            throw new InvalidCastException();
        }

        public static LongArrayTag ToTag(this IEnumerable<long> value)
        {
            return new LongArrayTag(value.ToArray());
        }

        public static IEnumerable<long> ToLongEnumerableValue(this ITag tag)
        {
            if (tag is LongArrayTag t)
            {
                return t.Value;
            }

            throw new InvalidCastException();
        }

        public static StringArrayTag ToTag(this string[] value)
        {
            return new StringArrayTag(value);
        }

        public static string[] ToStringArrayValue(this ITag tag)
        {
            if (tag is StringArrayTag t)
            {
                return t.Value;
            }

            throw new InvalidCastException();
        }

        public static StringArrayTag ToTag(this IEnumerable<string> value)
        {
            return new StringArrayTag(value.ToArray());
        }

        public static IEnumerable<string> ToStringEnumerableValue(this ITag tag)
        {
            if (tag is StringArrayTag t)
            {
                return t.Value;
            }

            throw new InvalidCastException();
        }

        public static ITag ToTag<TEnum>(this TEnum value) where TEnum : Enum
        {
            Type type = value.GetType().GetEnumUnderlyingType();
            if (type == typeof(byte))
                return new ByteTag((byte)Convert.ChangeType(value, TypeCode.Byte));
            else if (type == typeof(short))
                return new ShortTag((short)Convert.ChangeType(value, TypeCode.Int16));
            else if (type == typeof(int))
                return new IntTag((int)Convert.ChangeType(value, TypeCode.Int32));
            else if (type == typeof(long))
                return new LongTag((long)Convert.ChangeType(value, TypeCode.Int64));

            throw new NotSupportedException();
        }

        public static TEnum ToEnumValue<TEnum>(this ITag tag) where TEnum : Enum
        {
            if (tag is ByteTag byteTag)
                return (TEnum)Convert.ChangeType(byteTag.Value, typeof(TEnum));
            else if (tag is ShortTag shortTag)
                return (TEnum)Convert.ChangeType(shortTag.Value, typeof(TEnum));
            else if (tag is IntTag intTag)
                return (TEnum)Convert.ChangeType(intTag.Value, typeof(TEnum));
            else if (tag is LongTag longTag)
                return (TEnum)Convert.ChangeType(longTag.Value, typeof(TEnum));

            throw new NotSupportedException();
        }
    }
}