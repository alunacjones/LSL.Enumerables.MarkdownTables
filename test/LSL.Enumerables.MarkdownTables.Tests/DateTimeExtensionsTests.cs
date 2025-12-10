using System;
using FluentAssertions;

namespace LSL.Enumerables.MarkdownTables.Tests;

public class DateTimeExtensionsTests
{
    [TestCase(2000, 1, 2, 0, 0, 0, 0, "02/01/2000")]
    [TestCase(2000, 1, 2, 12, 0, 0, 0, "02/01/2000 12:00:00")]
    [TestCase(2000, 1, 2, 13, 0, 0, 0, "02/01/2000 13:00:00")]
    [TestCase(2000, 1, 2, 0, 13, 0, 0, "02/01/2000 00:13:00")]
    [TestCase(2000, 1, 2, 0, 0, 13, 0, "02/01/2000 00:00:13")]
    [TestCase(2000, 1, 2, 0, 0, 0, 13, "02/01/2000 00:00:00")]
    public void ToStringWithEmptyTimeDetection_ShouldReturnTheExpectedValue(
        int year,
        int month,
        int day,
        int hour,
        int minute,
        int second,
        int millisecond,
        string expectedOutput)
    {
        var date = new DateTime(year, month, day, hour, minute, second, millisecond);

        date
            .ToStringWithEmptyTimeDetection("G", "d")
            .Should()
            .Be(expectedOutput);

        new DateTimeOffset(date)
            .ToStringWithEmptyTimeDetection("G", "d")
            .Should()
            .Be(expectedOutput);            
    }
}