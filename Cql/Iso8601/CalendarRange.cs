/*
 * Copyright (c) 2026, Firely, NCQA and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-cql-sdk/main/LICENSE
 */

#nullable enable

namespace Hl7.Cql.Iso8601
{
    /// <summary>
    /// The ranges of date, time and offset components a <see cref="DateTimeOffset"/> can represent.
    /// A parser checks these before constructing one, so that an out-of-range component is reported
    /// by its caller rather than raised as an exception.
    /// </summary>
    internal static class CalendarRange
    {
        /// <summary>The largest offset from UTC a <see cref="DateTimeOffset"/> accepts.</summary>
        private const int MaximumOffsetInMinutes = 14 * 60;

        internal static bool IsRepresentableDate(int year, int? month, int? day) =>
            year is >= 1 and <= 9999
            && month is null or (>= 1 and <= 12)
            && (day is null
                || (month is { } m && m is >= 1 and <= 12 && day >= 1 && day <= DateTime.DaysInMonth(year, m)));

        internal static bool IsRepresentableTime(int? hour, int? minute, int? second) =>
            hour is null or (>= 0 and <= 23)
            && minute is null or (>= 0 and <= 59)
            && second is null or (>= 0 and <= 59);

        /// <summary>
        /// Whether an offset of <paramref name="offsetHour"/> hours and <paramref name="offsetMinute"/>
        /// minutes, each already carrying the offset's sign, is within the representable range.
        /// </summary>
        internal static bool IsRepresentableOffset(int? offsetHour, int? offsetMinute)
        {
            var totalMinutes = ((offsetHour ?? 0) * 60) + (offsetMinute ?? 0);
            return totalMinutes >= -MaximumOffsetInMinutes && totalMinutes <= MaximumOffsetInMinutes;
        }

        /// <summary>
        /// Whether a local date and time, once its offset is applied, is an instant a
        /// <see cref="DateTimeOffset"/> can hold. The first and last representable days fail this with
        /// an offset that moves them past the ends of the range.
        /// </summary>
        internal static bool IsRepresentableInstant(DateTime local, int? offsetHour, int? offsetMinute)
        {
            var offset = TimeSpan.FromMinutes(((offsetHour ?? 0) * 60) + (offsetMinute ?? 0));
            return local - DateTime.MinValue >= offset && DateTime.MaxValue - local >= -offset;
        }
    }
}
