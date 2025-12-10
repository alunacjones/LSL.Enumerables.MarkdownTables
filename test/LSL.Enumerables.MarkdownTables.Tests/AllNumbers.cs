using System;
using System.Numerics;

namespace LSL.Enumerables.MarkdownTables.Tests;

public class AllNumbers
{
#pragma warning disable CA1822 // Mark members as static
    public int IntValue => 123;
    public byte ByteValue => 11;
    public short ShortValue => 12345;
    public long LongValue => 123456789012345;
    public BigInteger BigInteger => new(12345678901234567);
    public float Float => 123.456F;
    public decimal? NullDecimal => null;

    [TimeDetectingDateTimeValueTransformer("dd-MM-yyyy", "dd-MM-yyyy")]
    public DateTime ADate => new(2010, 10, 1);
#pragma warning restore CA1822 // Mark members as static
}