## Fixes

- **CQL-to-ELM translator:** a library-qualified identifier resolves when it is used as a query source
  or as retrieve terminology, as it already did everywhere else. `CQMCommon."Inpatient Encounter" E`
  threw `InvalidOperationException`, and `[Task: QICoreCommon."Fulfill"]` reported
  `Type  has no members.`; they now translate to an `ExpressionRef`, `ValueSetRef` or `CodeRef` in
  the named library, as the reference translator does.

- **CQL-to-ELM translator:** a query source that cannot be used reports an error instead of throwing
  out of translation: a misspelled definition reports `Could not resolve identifier NoSuchDefine in
  the current library.`, a library alias `Identifier FH is a library and cannot be used as an
  expression.`, and a model alias `A reference to a model library is unexpected at this point.` A
  library or model alias used as retrieve terminology, which translated without an error, now
  reports the same.

- **CQL-to-ELM translator:** a function or system operator named through a library alias without
  being called, such as `Global."Fn"` or `Global.Today`, reports `Could not resolve identifier Fn in
  library Global.`, as the reference translator does. `Global."Fn"` threw `NotSupportedException`,
  and `Global.Today` translated to a reference to a function `Global` does not define.

  A library that used a library-qualified identifier as a query source or retrieve terminology
  could not be translated and now can. A library that used `Global.Today` or an alias as retrieve
  terminology translated to ELM that could not be evaluated, and now reports an error instead. No
  library that evaluated before evaluates differently, so no evaluation result moves and there is
  no `GeneratorToolVersion` change.
