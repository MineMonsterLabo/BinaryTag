using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace BinaryTag.Tags
{
    public class ListTag : ITag, IList<ITag>, IEquatable<ListTag>
    {
        private readonly IList<ITag> _list = new List<ITag>();

        public TagType Type => TagType.List;

        [Category("データ"), Description("アイテムの数")]
        public TagType ElementTagType { get; private set; }

        [Category("データ"), Description("アイテムの数")]
        public int Count => _list.Count;

        [Browsable(false)] public bool IsReadOnly => false;

        public ListTag(TagType type = TagType.Byte)
        {
            ElementTagType = type;
        }

        public IEnumerator<ITag> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(ITag item)
        {
            ThrowIfNotMatchType(item);

            _list.Add(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(ITag item)
        {
            ThrowIfNotMatchType(item);

            return _list.Contains(item);
        }

        public void CopyTo(ITag[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool Remove(ITag item)
        {
            ThrowIfNotMatchType(item);

            return _list.Remove(item);
        }

        public int IndexOf(ITag item)
        {
            ThrowIfNotMatchType(item);

            return _list.IndexOf(item);
        }

        public void Insert(int index, ITag item)
        {
            ThrowIfNotMatchType(item);

            _list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        public ITag this[int index]
        {
            get => _list[index];
            set
            {
                ThrowIfNotMatchType(value);

                _list[index] = value;
            }
        }

        public T Get<T>(int index) where T : ITag
        {
            return (T)_list[index];
        }

        public T GetOrNull<T>(int index) where T : class, ITag
        {
            return _list[index] as T;
        }

        public void Read(BinaryReader reader)
        {
            int len = reader.Read7BitEncodedInt();
            ElementTagType = (TagType)reader.ReadByte();
            for (int i = 0; i < len; i++)
            {
                ITag tag = TagFactory.CreateTag(ElementTagType);
                tag.Read(reader);

                Add(tag);
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write7BitEncodedInt(Count);
            writer.Write((byte)ElementTagType);
            foreach (ITag tag in _list)
            {
                tag.Write(writer);
            }
        }

        private void ThrowIfNotMatchType(ITag tag)
        {
            if (tag.Type != ElementTagType)
                throw new InvalidOperationException();
        }

        public object Clone()
        {
            ListTag tag = new ListTag(ElementTagType);
            foreach (ITag tag1 in _list)
            {
                tag.Add((ITag)tag1.Clone());
            }

            return tag;
        }

        public bool Equals(ListTag other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _list.SequenceEqual(other._list) && ElementTagType == other.ElementTagType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ListTag)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_list != null ? _list.GetHashCode() : 0) * 397) ^ (int)ElementTagType;
            }
        }

        public static bool operator ==(ListTag left, ListTag right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ListTag left, ListTag right)
        {
            return !Equals(left, right);
        }
    }
}