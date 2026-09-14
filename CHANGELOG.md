# Changelog

All notable changes to this package are documented here. The format is based on
[Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

Release numbers are stamped by CI as part of the publish workflow, so a release's
number is assigned when that workflow runs. Work that has not been published yet
sits under **Unreleased**.

This file was introduced at version 10.0.2. Changes made before that point are not listed here —
see the commit history for those.

## [Unreleased]

### Added

- `Egov.Integrations.MNotify.Extended` package, migrated from Azure DevOps into this
  repository. It adds `IMNotifyExtendedClient`, which derives from `IMNotifyClient` and
  covers the recipient-facing MNotify API: message inbox with pagination summary, per-message
  and bulk read state, recipient contacts, and notification language. Register it with
  `services.AddMNotifyExtendedClient(...)` in place of `AddMNotifyClient`.

### Changed

- The publish workflow now packs and pushes both packages, versioned together.
