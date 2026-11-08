# Contributing to AHOY!

AHOY! is in **maintenance mode**. My focus is now on [Marbles](https://github.com/artstisen/marbles) and [Blyant](https://blyant-editor.com/), so AHOY! gets bug fixes but no new features.

## Reporting a bug

Bug reports are welcome under [Issues](../../issues). Please include:

- the AHOY! version (shown on the splash screen) and whether you use the installer or the portable version
- your Windows version
- what you did, what you expected and what happened instead
- the crash log, if there is one: `%APPDATA%\Ahoy\Crash.log`. It records only error types and code locations, never your notes.

**Never attach your Pool file, backups or notes, and never post your password.** No one needs them to fix a bug.

For security problems, please use [private vulnerability reporting](../../security/advisories/new) instead of a public issue.

## Pull requests

Pull requests that fix bugs are welcome. Please:

- keep each pull request to one fix
- run the tests before you send it: `dotnet test AhoyBrowser.Tests`
- leave the Pool file format alone, so existing Pools keep opening

New features are not planned. Pull requests that add them will usually not be merged.

## Forks

You are welcome to fork AHOY! and build your own version with the features you want. It is MIT licensed: you can change it and share it, as long as you keep the copyright notice and the license (see [LICENSE](LICENSE)). Please give your version its own name, so that people can tell it apart from AHOY!.
