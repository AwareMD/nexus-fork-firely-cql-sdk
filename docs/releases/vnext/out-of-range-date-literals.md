## Fixes

- **Iso8601:** `DateIso8601.TryParse` and `DateTimeIso8601.TryParse` return `false` for a date or
  time that does not exist, such as `2026-13-45` or 29 February in a common year, and
  `DateTimeIso8601.TryParse` and `TimeIso8601.TryParse` return `false` for a UTC offset beyond 14
  hours, instead of throwing `ArgumentOutOfRangeException` or `ArgumentException`. The same holds
  for a date and time whose offset moves it outside the representable range, such as
  `0001-01-01T01:00:00.000+02:00`. `CqlDate.TryParse`, `CqlDateTime.TryParse` and
  `CqlTime.TryParse` delegate to these methods and change with them. No exception type is added;
  these two no longer escape the methods. Well-formed values parse exactly as before.

- **CQL-to-ELM translator:** a date or date/time literal that does not exist, such as `@2026-13-45`,
  is reported as `Unparseable date literal '2026-13-45'.` instead of throwing out of translation.

- **Runtime:** a FHIR `date` that does not exist, or a FHIR `time` whose offset is beyond 14 hours,
  converts to `null` instead of failing evaluation. A Patient whose `birthDate` is `2026-02-29` threw
  out of `Patient.birthDate < @2000-01-01`; the comparison now yields `null`. `ToDateTime` on a
  string that does not represent a valid DateTime, and `ToTime` on a time whose offset is beyond 14
  hours, return `null` as the specification requires, where evaluation threw.

  **This changes CQL evaluation results**: an expression over such a value now yields `null` where
  evaluation previously failed with an exception, and a host that caught that exception now sees
  `null`. Well-formed values evaluate exactly as before. No `GeneratorToolVersion` change: the
  emitted C# is identical and the fix is entirely in the parsers the runtime calls.
