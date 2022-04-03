using System;
using System.Collections.Generic;
using System.IO;

namespace BinaryTag.Tags
{
    public abstract class ValueTag<T> : ITag, IEquatable<ValueTag<T>>
    {
        public abstract TagType Type { get; }

        public T Value { get; set; }

        public ValueTag()
        {
        }

        public ValueTag(T value)
        {
            Value = value;
        }

        public abstract void Read(BinaryReader reader);
        public abstract void Write(BinaryWriter write);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ValueTag<T>)obj);
        }

        public bool Equals(ValueTag<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public abstract object Clone();

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(Value);
        }

        public static bool operator ==(ValueTag<T> left, ValueTag<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ValueTag<T> left, ValueTag<T> right)
        {
            return !Equals(left, right);
        }

        public static implicit operator T(ValueTag<T> tag)
        {
            return tag.Value;
        }
    }
}