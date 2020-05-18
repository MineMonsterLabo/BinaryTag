using System;
using System.ComponentModel;
using BinaryIO;

namespace BinaryTag.Tags
{
    public interface ITag : ICloneable
    {
        [Category("データ"), Description("タイプ")] TagType Type { get; }

        void Read(BinaryStream stream);
        void Write(BinaryStream stream);
    }
}