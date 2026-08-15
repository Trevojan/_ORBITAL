# CHANGELOG

**Owner:** DV · **Updated in:** each module's phase F6.

Format: Keep a Changelog. Versioning: SemVer.

```
MAJOR.MINOR.PATCH
  MAJOR  incompatible change - breaks a contract with a consumer
  MINOR  backward-compatible capability
  PATCH  fix or rule adjustment with no contract change
```

While the version is `0.y.z`, SemVer promises no contract stability: an incompatible change
raises the MINOR, not the MAJOR. Say so rather than pretending otherwise — `0.x` is the
honest label for a system whose shape is still moving.

The version lives in `Directory.Build.props`. Keep the two in step.

## Who reads this

Developers, managers and clients. Write in **business language**: what changed and why it
mattered, not which method was renamed. An entry that only makes sense next to the diff
belongs in the commit message instead.

An entry states the observable difference. "Fixed `CalculateTotal`" says nothing; "a total
that included cancelled items no longer does, so the monthly figure stops disagreeing with
the report" says what someone will notice.

---

## [Unreleased]

### Added

### Changed

### Fixed

---

*(numbered versions are added below as they are released, newest first)*
