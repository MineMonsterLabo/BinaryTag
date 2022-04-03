using System;
using System.ComponentModel;
using System.IO;

namespace BinaryTag.Tags
{
    public interface ITag : ICloneable
    {
        [Category("データ"), Description("タイプ")] TagType Type { get; }

        void Read(BinaryReader reader);
        void Write(BinaryWriter writer);
    }
}