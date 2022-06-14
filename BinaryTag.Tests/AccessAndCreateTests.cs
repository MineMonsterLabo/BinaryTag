using BinaryTag.Tags;

namespace BinaryTag.Tests;

public class AccessAndCreateTests
{
    [Test]
    public void Create()
    {
        MapTag _ = new MapTag
        {
            { "byte", new ByteTag(253) },
            { "short", new ShortTag(6543) },
            { "int", new IntTag(654321) },
            { "long", new LongTag(987654321) },
            { "float", new FloatTag(6543.1234f) },
            { "double", new DoubleTag(6543.123456) },
            { "char", new CharTag('c') },
            { "string", new StringTag("hello bin") },
            {
                "list", new ListTag(TagType.Int)
                {
                    new IntTag(1234),
                    new IntTag(5678)
                }
            },
            {
                "map", new MapTag
                {
                    {
                        "list", new ListTag(TagType.List)
                        {
                            new ListTag(TagType.Short),
                            new ListTag(TagType.Int),
                            new ListTag(TagType.List)
                        }
                    },
                    {
                        "map", new MapTag()
                        {
                            {
                                "map", new MapTag()
                                {
                                    { "test", new StringTag("map in map in map") }
                                }
                            }
                        }
                    }
                }
            }
        };
    }

    [Test]
    public void Load()
    {
        MapTag mapTag = new MapTag
        {
            { "byte", new ByteTag(253) },
            { "short", new ShortTag(6543) },
            { "int", new IntTag(654321) },
            { "long", new LongTag(987654321) },
            { "float", new FloatTag(6543.1234f) },
            { "double", new DoubleTag(6543.123456) },
            { "char", new CharTag('c') },
            { "string", new StringTag("hello bin") },
            {
                "list", new ListTag(TagType.Int)
                {
                    new IntTag(1234),
                    new IntTag(5678)
                }
            },
            {
                "map", new MapTag
                {
                    {
                        "list", new ListTag(TagType.List)
                        {
                            new ListTag(TagType.Short),
                            new ListTag(TagType.Int)
                            {
                                new IntTag(12)
                            },
                            new ListTag(TagType.List)
                        }
                    },
                    {
                        "map", new MapTag()
                        {
                            {
                                "map", new MapTag()
                                {
                                    { "test", new StringTag("map in map in map") }
                                }
                            }
                        }
                    }
                }
            }
        };

        using var ms = new MemoryStream();
        BinaryWriter stream = new BinaryWriter(ms);
        mapTag.Write(stream);

        ms.Position = 0;

        BinaryReader readStream = new BinaryReader(ms);
        MapTag readMapTag = new MapTag();
        readMapTag.Read(readStream);

        MapTag mapTagA = readMapTag["map"] as MapTag;
        Assert.NotNull(mapTagA);

        MapTag mapTagB = mapTagA["map"] as MapTag;
        Assert.NotNull(mapTagB);

        MapTag mapTagC = mapTagB["map"] as MapTag;
        Assert.NotNull(mapTagC);

        StringTag stringTag = mapTagC["test"] as StringTag;
        Assert.NotNull(stringTag);

        Assert.That(stringTag.Value, Is.EqualTo("map in map in map"));
        Assert.That(stringTag.Value, Is.Not.EqualTo("map in map"));

        ListTag listTagA = mapTagA["list"] as ListTag;
        Assert.NotNull(listTagA);

        ListTag listTagB = listTagA[1] as ListTag;
        Assert.NotNull(listTagB);

        Assert.That(new IntTag(12), Is.EqualTo(listTagB[0] as IntTag));
        Assert.That(new IntTag(0), Is.Not.EqualTo(listTagB[0] as IntTag));
    }

    [Test]
    public void Equal()
    {
        MapTag m1 = new MapTag
        {
            { "byte", new ByteTag(253) },
            { "short", new ShortTag(6543) },
        };
        MapTag m2 = new MapTag
        {
            { "byte", new ByteTag(253) },
            { "short", new ShortTag(6543) },
        };
        MapTag m3 = new MapTag
        {
            { "byte", new ByteTag(255) },
            { "short", new ShortTag(9999) },
        };
        MapTag m4 = new MapTag
        {
            { "byte", new ByteTag(255) },
            { "short", new IntTag(3333) },
        };
        MapTag m5 = new MapTag
        {
            { "byte", new ByteTag(255) },
            { "int", new IntTag(3333) },
        };

        Assert.That(m2, Is.EqualTo(m1));
        Assert.That(m3, Is.Not.EqualTo(m1));
        Assert.That(m4, Is.Not.EqualTo(m1));
        Assert.That(m5, Is.Not.EqualTo(m1));

        MapTag mm1 = new MapTag
        {
            { "byte", new ByteTag(253) },
            { "short", new ShortTag(6543) },
            {
                "map", new MapTag
                {
                    { "s", new StringTag("abc") },
                    { "def", new LongTag(123456789) }
                }
            }
        };
        MapTag mm2 = new MapTag
        {
            { "byte", new ByteTag(253) },
            { "short", new ShortTag(6543) },
            {
                "map", new MapTag
                {
                    { "s", new StringTag("abc") },
                    { "def", new LongTag(123456789) }
                }
            }
        };
        MapTag mm3 = new MapTag
        {
            { "byte", new ByteTag(253) },
            { "short", new ShortTag(6543) },
            {
                "map", new MapTag
                {
                    { "s", new StringTag("test") },
                    { "def", new LongTag(123456789) }
                }
            }
        };
        MapTag mm4 = new MapTag
        {
            { "byte", new ByteTag(253) },
            { "short", new IntTag(6543) },
            {
                "map", new MapTag
                {
                    { "def", new LongTag(123456789) }
                }
            }
        };

        Assert.That(mm2, Is.EqualTo(mm1));
        Assert.That(mm3, Is.Not.EqualTo(mm1));
        Assert.That(mm4, Is.Not.EqualTo(mm1));

        MapTag ma1 = new MapTag
        {
            { "byte", new ByteTag(253) },
            { "short", new ShortTag(6543) },
            { "array", new IntArrayTag(new[] { 1, 3, 5, 7, 9 }) },
            { "sar", new StringArrayTag(new[] { "abc", "hello", "apple" }) }
        };
        MapTag ma2 = new MapTag
        {
            { "byte", new ByteTag(253) },
            { "short", new ShortTag(6543) },
            { "array", new IntArrayTag(new[] { 1, 3, 5, 7, 9 }) },
            { "sar", new StringArrayTag(new[] { "abc", "hello", "apple" }) }
        };
        MapTag ma3 = new MapTag
        {
            { "byte", new ByteTag(253) },
            { "short", new ShortTag(6543) },
            { "array", new IntArrayTag(new[] { 1, 3, 5, 7, 9 }) },
            { "sar", new StringArrayTag(new[] { "abc", "hello", "chip" }) }
        };
        MapTag ma4 = new MapTag
        {
            { "byte", new ByteTag(253) },
            { "short", new IntTag(6543) },
            { "array", new IntArrayTag(new[] { 1, 3, 5, 7, 9 }) },
            { "sar", new StringArrayTag(new[] { "abc", "hello", "choco" }) }
        };

        Assert.That(ma2, Is.EqualTo(ma1));
        Assert.That(ma3, Is.Not.EqualTo(ma1));
        Assert.That(ma4, Is.Not.EqualTo(ma1));

        MapTag ml1 = new MapTag
        {
            { "byte", new ByteTag(253) },
            { "short", new ShortTag(6543) },
            {
                "list", new ListTag(TagType.Int)
                {
                    new IntTag(22),
                    new IntTag(33),
                    new IntTag(44),
                    new IntTag(1234)
                }
            },
            {
                "mlist", new ListTag(TagType.Map)
                {
                    new MapTag
                    {
                        { "byte", new ByteTag(253) },
                        { "short", new IntTag(6543) }
                    }
                }
            },
            {
                "aList", new ListTag(TagType.StringArray)
                {
                    new StringArrayTag(new[] { "abc", "def", "ghi" }),
                    new StringArrayTag(new[] { "1234", "ad", "apple" })
                }
            }
        };
        MapTag ml2 = new MapTag
        {
            { "byte", new ByteTag(253) },
            { "short", new ShortTag(6543) },
            {
                "list", new ListTag(TagType.Int)
                {
                    new IntTag(22),
                    new IntTag(33),
                    new IntTag(44),
                    new IntTag(1234)
                }
            },
            {
                "mlist", new ListTag(TagType.Map)
                {
                    new MapTag
                    {
                        { "byte", new ByteTag(253) },
                        { "short", new IntTag(6543) }
                    }
                }
            },
            {
                "aList", new ListTag(TagType.StringArray)
                {
                    new StringArrayTag(new[] { "abc", "def", "ghi" }),
                    new StringArrayTag(new[] { "1234", "ad", "apple" })
                }
            }
        };
        MapTag ml3 = new MapTag
        {
            { "byte", new ByteTag(253) },
            { "short", new ShortTag(6543) },
            {
                "list", new ListTag(TagType.Int)
                {
                    new IntTag(44),
                    new IntTag(3553),
                    new IntTag(44),
                    new IntTag(1234)
                }
            },
            {
                "mlist", new ListTag(TagType.Map)
                {
                    new MapTag
                    {
                        { "byte", new ByteTag(253) },
                        { "short", new IntTag(6543) }
                    }
                }
            },
            {
                "aList", new ListTag(TagType.StringArray)
                {
                    new StringArrayTag(new[] { "abc", "def", "ghi" }),
                    new StringArrayTag(new[] { "1234", "ad", "apple" })
                }
            }
        };
        MapTag ml4 = new MapTag
        {
            { "byte", new ByteTag(253) },
            { "short", new IntTag(6543) },
            {
                "list", new ListTag(TagType.Int)
                {
                    new IntTag(22),
                    new IntTag(33),
                    new IntTag(44),
                    new IntTag(1234)
                }
            },
            {
                "mlist", new ListTag(TagType.Map)
                {
                    new MapTag
                    {
                        { "byte", new ByteTag(253) },
                        { "short", new IntTag(6543) }
                    }
                }
            },
            {
                "aList", new ListTag(TagType.StringArray)
                {
                    new StringArrayTag(new[] { "abc", "def", "ghi" }),
                    new StringArrayTag(new[] { "1234", "ad", "apple" })
                }
            }
        };
        MapTag ml5 = new MapTag
        {
            { "byte", new ByteTag(253) },
            { "short", new ShortTag(6543) },
            {
                "list", new ListTag(TagType.Int)
                {
                    new IntTag(22),
                    new IntTag(33),
                    new IntTag(44),
                    new IntTag(1234)
                }
            },
            {
                "mlist", new ListTag(TagType.Map)
                {
                    new MapTag
                    {
                        { "byte", new ByteTag(253) },
                        { "short", new IntTag(6543) }
                    }
                }
            },
            {
                "aList", new ListTag(TagType.StringArray)
                {
                    new StringArrayTag(new[] { "1234", "aaaaa", "333" }),
                    new StringArrayTag(new[] { "1234", "fff", "apple" })
                }
            }
        };
        MapTag ml6 = new MapTag
        {
            { "byte", new ByteTag(253) },
            { "short", new ShortTag(6543) },
            {
                "list", new ListTag(TagType.Int)
                {
                    new IntTag(22),
                    new IntTag(33),
                    new IntTag(44),
                    new IntTag(1234)
                }
            },
            {
                "mlist", new ListTag(TagType.Map)
                {
                    new MapTag
                    {
                        { "byte", new ByteTag(253) },
                        { "short", new IntTag(6543) }
                    },
                    new MapTag
                    {
                        { "byte", new ByteTag(253) },
                        { "short", new IntTag(6543) }
                    }
                }
            },
            {
                "aList", new ListTag(TagType.StringArray)
                {
                    new StringArrayTag(new[] { "abc", "def", "ghi" }),
                    new StringArrayTag(new[] { "1234", "ad", "apple" })
                }
            }
        };

        Assert.That(ml2, Is.EqualTo(ml1));
        Assert.That(ml3, Is.Not.EqualTo(ml1));
        Assert.That(ml4, Is.Not.EqualTo(ml1));
        Assert.That(ml5, Is.Not.EqualTo(ml1));
        Assert.That(ml6, Is.Not.EqualTo(ml1));
    }
}