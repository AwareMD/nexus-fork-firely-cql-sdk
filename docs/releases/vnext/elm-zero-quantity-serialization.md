## Fixes

- **ELM JSON serialization:** a property paired with an `xxxSpecified` flag is written whenever its
  flag is set, including when its value equals the type default. A `Quantity` of zero, as in
  `0 'cm' > 1 'cm'`, was serialized as `{"type":"Quantity","unit":"cm"}` with no `value`, which a
  reader other than this SDK sees as a quantity without a value; the reference translator writes
  `"value": 0`. This SDK reads its own output back unchanged, so no evaluation result moves, and
  there is no public API or `GeneratorToolVersion` change.
