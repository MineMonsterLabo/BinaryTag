using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace BinaryTag.Tags
{
    public class MapTag : ITag, IDictionary<string, ITag>, IEquatable<MapTag>
    {
        private readonly Dictionary<string, ITag> _tags = new Dictionary<string, ITag>();

        public TagType Type => TagType.Map;

        [Category("データ"), Description("アイテムの数")]
        public int Count => _tags.Count;

        [Browsable(false)] public bool IsReadOnly => false;

        [Browsable(false)] public ICollection<string> Keys => _tags.Keys;
        [Browsable(false)] public ICollection<ITag> Values => _tags.Values;

        public IEnumerator<KeyValuePair<string, ITag>> GetEnumerator()
        {
            return _tags.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(string key, ITag value)
        {
            _tags.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return _tags.ContainsKey(key);
        }

        public bool Remove(string key)
        {
            return _tags.Remove(key);
        }

        public bool TryGetValue(string key, out ITag value)
        {
            return _tags.TryGetValue(key, out value);
        }

        public ITag this[string key]
        {
            get => _tags[key];
            set => _tags[key] = value;
        }

        public void Add(KeyValuePair<string, ITag> item)
        {
            _tags.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _tags.Clear();
        }

        public bool Contains(KeyValuePair<string, ITag> item)
        {
            return _tags.ContainsKey(item.Key) || _tags.ContainsValue(item.Value);
        }

        public void CopyTo(KeyValuePair<string, ITag>[] array, int arrayIndex)
        {
            _tags.ToArray().CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, ITag> item)
        {
            return _tags.Remove(item.Key);
        }

        public T Get<T>(string key) where T : ITag
        {
            return (T)_tags[key];
        }

        public T GetOrNull<T>(string key) where T : class, ITag
        {
            if (ContainsKey(key))
                return _tags[key] as T;

            return null;
        }

        public T GetOrEmpty<T>(string key) where T : class, ITag, new()
        {
            if (ContainsKey(key))
                return _tags[key] as T;

            return new T();
        }

        public T GetOrDefault<T>(string key, T defaultValue) where T : class, ITag
        {
            if (ContainsKey(key))
                return _tags[key] as T;

            return defaultValue;
        }

        public void Read(BinaryReader reader)
        {
            int len = reader.ReadInt32();
            for (int i = 0; i < len; i++)
            {
                TagType type = (TagType)reader.ReadByte();
                string name = reader.ReadString();
                ITag tag = TagFactory.CreateTag(type);
                tag.Read(reader);

                Add(name, tag);
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Count);
            foreach (KeyValuePair<string, ITag> tag in _tags)
            {
                writer.Write((byte)tag.Value.Type);
                writer.Write(tag.Key);
                tag.Value.Write(writer);
            }
        }

        public object Clone()
        {
            MapTag mapTag = new MapTag();
            foreach (KeyValuePair<string, ITag> pair in _tags)
            {
                mapTag[pair.Key] = (ITag)pair.Value.Clone();
            }

            return mapTag;
        }

        public bool Equals(MapTag other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _tags.SequenceEqual(other._tags);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MapTag)obj);
        }

        public override int GetHashCode()
        {
            return (_tags != null ? _tags.GetHashCode() : 0);
        }

        public static bool operator ==(MapTag left, MapTag right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MapTag left, MapTag right)
        {
            return !Equals(left, right);
        }
    }
}