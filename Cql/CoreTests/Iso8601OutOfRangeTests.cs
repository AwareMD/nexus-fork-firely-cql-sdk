/*
 * Copyright (c) 2026, Firely, NCQA and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-cql-sdk/main/LICENSE
 */

#nullable enable

using Hl7.Cql.Iso8601;

namespace CoreTests;

[TestClass]
[TestCategory("UnitTest")]
public class Iso8601OutOfRangeTests
{
    [TestMethod]
    [DataRow("2026-13-45", DisplayName = "month and day out of range")]
    [DataRow("2026-13", DisplayName = "month out of range")]
    [DataRow("2026-00-01", DisplayName = "month zero")]
    [DataRow("2026-01-00", DisplayName = "day zero")]
    [DataRow("2026-02-31", DisplayName = "day beyond the month")]
    [DataRow("2026-02-29", DisplayName = "29 February in a common year")]
    [DataRow("0000-01-01", DisplayName = "year before the representable range")]
    public void DateIso8601_TryParse_OutOfRange_ReturnsFalse(string value)
    {
        Assert.IsFalse(DateIso8601.TryParse(value, out var parsed));
        Assert.IsNull(parsed);
    }

    [TestMethod]
    [DataRow("2024-02-29", DisplayName = "29 February in a leap year")]
    [DataRow("0001-01-01", DisplayName = "the first representable day")]
    [DataRow("9999-12-31", DisplayName = "the last representable day")]
    [DataRow("2026-01", DisplayName = "month precision")]
    [DataRow("2026", DisplayName = "year precision")]
    public void DateIso8601_TryParse_InRange_ReturnsTrue(string value)
    {
        Assert.IsTrue(DateIso8601.TryParse(value, out var parsed));
        Assert.AreEqual(value, parsed!.ToString());
    }

    [TestMethod]
    [DataRow("2026-13-45T10:00:00.000Z", DisplayName = "month and day out of range")]
    [DataRow("2026-02-31T10:00:00.000Z", DisplayName = "day beyond the month")]
    [DataRow("2026-01-01T25:00:00.000Z", DisplayName = "hour out of range")]
    [DataRow("2026-01-01T10:61:00.000Z", DisplayName = "minute out of range")]
    [DataRow("2026-01-01T10:00:61.000Z", DisplayName = "second out of range")]
    [DataRow("2026-01-01T10:00:00.000+15:00", DisplayName = "offset beyond plus 14 hours")]
    [DataRow("2026-01-01T10:00:00.000-99:99", DisplayName = "offset far beyond the range")]
    [DataRow("0001-01-01T01:00:00.000+02:00", DisplayName = "offset moves the first day before the range")]
    [DataRow("9999-12-31T23:00:00.000-01:00", DisplayName = "offset moves the last day past the range")]
    public void DateTimeIso8601_TryParse_OutOfRange_ReturnsFalse(string value)
    {
        Assert.IsFalse(DateTimeIso8601.TryParse(value, out var parsed));
        Assert.IsNull(parsed);
    }

    [TestMethod]
    [DataRow("2026-01-01T10:00:00.000Z", DisplayName = "a representable instant")]
    [DataRow("2026-01-01T10:00:00.000+05:30", DisplayName = "an offset that is not a whole hour")]
    [DataRow("2026-01-01T10:00:00.000+14:00", DisplayName = "the largest offset")]
    [DataRow("2026-01-01T10:00:00.000-14:00", DisplayName = "the smallest offset")]
    [DataRow("0001-01-01T00:00:00.000Z", DisplayName = "the first representable instant")]
    [DataRow("9999-12-31T23:59:59.999Z", DisplayName = "the last representable instant")]
    [DataRow("0001-01-01T00:00:00.000-14:00", DisplayName = "the first day behind UTC")]
    [DataRow("9999-12-31T23:59:59.999+14:00", DisplayName = "the last day ahead of UTC")]
    public void DateTimeIso8601_TryParse_InRange_ReturnsTrue(string value)
    {
        Assert.IsTrue(DateTimeIso8601.TryParse(value, out var parsed));
        Assert.AreEqual(value, parsed!.ToString());
    }

    [TestMethod]
    [DataRow("10:00:00.000+15:00", DisplayName = "offset beyond plus 14 hours")]
    [DataRow("10:00:00.000-15:00", DisplayName = "offset beyond minus 14 hours")]
    public void TimeIso8601_TryParse_OffsetOutOfRange_ReturnsFalse(string value)
    {
        Assert.IsFalse(TimeIso8601.TryParse(value, out var parsed));
        Assert.IsNull(parsed);
    }

    [TestMethod]
    [DataRow("10:00:00.000+14:00", DisplayName = "the largest offset")]
    [DataRow("10:00:00.000-14:00", DisplayName = "the smallest offset")]
    public void TimeIso8601_TryParse_OffsetInRange_ReturnsTrue(string value)
    {
        Assert.IsTrue(TimeIso8601.TryParse(value, out var parsed));
        Assert.AreEqual(value, parsed!.ToString());
    }
}
