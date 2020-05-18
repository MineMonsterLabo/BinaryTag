using System;
using BinaryIO;

namespace BinaryTag.Tags
{
    public abstract class ArrayValueTag<T> : ITag, IEquatable<ArrayValueTag<T>>
    {
        public abstract TagType Type { get; }

        public T[] Value { get; set; }

        public ArrayValueTag()
        {
        }

        public ArrayValueTag(T[] value)
        {
            Value = value;
        }

        public abstract void Read(BinaryStream stream);
        public abstract void Write(BinaryStream stream);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ArrayValueTag<T>) obj);
        }

        public bool Equals(ArrayValueTag<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Value, other.Value);
        }

        public abstract object Clone();

        public override int GetHashCode()
        {
            return Value != null ? Value.GetHashCode() : 0;
        }

        public static bool operator ==(ArrayValueTag<T> left, ArrayValueTag<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ArrayValueTag<T> left, ArrayValueTag<T> right)
        {
            return !Equals(left, right);
        }

        public static implicit operator T[](ArrayValueTag<T> tag)
        {
            return tag.Value;
        }
    }
}