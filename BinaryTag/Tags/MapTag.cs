using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BinaryIO;

namespace BinaryTag.Tags
{
    public class MapTag : ITag, IDictionary<string, ITag>
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
            return (T) _tags[key];
        }

        public T GetOrNull<T>(string key) where T : class, ITag
        {
            if (ContainsKey(key))
                return _tags[key] as T;

            return null;
        }

        public T GetOrNEmpty<T>(string key) where T : class, ITag, new()
        {
            if (ContainsKey(key))
                return _tags[key] as T;

            return new T();
        }

        public void Read(BinaryStream stream)
        {
            int len = stream.ReadInt();
            for (int i = 0; i < len; i++)
            {
                TagType type = (TagType) stream.ReadByte();
                string name = stream.ReadStringUtf8();
                ITag tag = TagFactory.CreateTag(type);
                tag.Read(stream);

                Add(name, tag);
            }
        }

        public void Write(BinaryStream stream)
        {
            stream.WriteInt(Count);
            foreach (KeyValuePair<string, ITag> tag in _tags)
            {
                stream.WriteByte((byte) tag.Value.Type);
                stream.WriteStringUtf8(tag.Key);
                tag.Value.Write(stream);
            }
        }

        public object Clone()
        {
            MapTag mapTag = new MapTag();
            foreach (KeyValuePair<string, ITag> pair in _tags)
            {
                mapTag[pair.Key] = (ITag) pair.Value.Clone();
            }

            return mapTag;
        }
    }
}