# Changelog

## 2.0.0 (2026-11-08)

AHOY! 2.0 is a modernized, free and open-source release. Notes from AHOY! 1.x are kept: the first time 2.0 opens a 1.x Pool, it asks for your current password, has you create a new one and saves the Pool in the new, encrypted format. See [Upgrading from AHOY! 1.x](README.md#upgrading-from-ahoy-1x).

### Downloads

| File | |
|---|---|
| `Ahoy-2.0.0-setup-win-x64.exe` | Installer (about 50 MB), .NET included. Installs without administrator rights. |
| `Ahoy-2.0.0-portable-win-x64.zip` | Portable (about 1 MB). Needs the .NET 10 Desktop Runtime. |
| `SHA256SUMS.txt` | Checksums of the files. |

The files are not code signed, so Microsoft Defender SmartScreen may show *"Windows protected your PC"*. See [AHOY! is not code signed](README.md#ahoy-is-not-code-signed) for how to check the files and run them.

### Security
- The Pool is encrypted with AES-256-GCM, with the key derived from your password (PBKDF2-HMAC-SHA256, 600,000 iterations). AHOY! 1.x used 3DES with a key built into the program.
- The password is never stored, and there is no password reset: the security question is gone.
- Passwords have at least 8 characters. Passphrases with spaces and letters such as æ, ø and å are allowed.
- Message encryption (encrypt a selected text to share it) uses AES-256-GCM too; messages from 1.x can still be decrypted.
- The lock screen can't be bypassed by opening another Pool from it.
- The event log no longer records titles, search texts or command arguments, and the crash log records only error types and code locations.
- Generated passwords are kept out of Windows clipboard history.
- Links in notes open only web addresses (http/https), never programs.

### New
- **Export to Marbles** (*File -> Export to Marbles*): writes your notes as a data file for [Marbles](https://github.com/artstisen/marbles), the cross-platform terminal successor to AHOY!.
- Installer with .NET included, and a small portable version.
- New splash screen.
- Unexpected errors are shown and logged instead of AHOY! closing silently. Starting AHOY! while it is already running brings the open window to the front.

### Changed
- Runs on .NET 10 (previously .NET Framework 4.8).
- Plain ASCII instead of extended ASCII and box-drawing characters: the category tree in the pinned list uses `|`, `-`, `/` and `\`, text decorations are simpler, and the home screen and animations are drawn. The old characters showed up wrong with some fonts and on East Asian Windows.
- Three color themes: Dark, Light and Blue, with custom colors.
- Saving is safer: files are written to a temporary file first and then replaced, and a failed save is reported and tried again.

### Removed
Typewriter, Typermax, notifications, the Utility/PLOP Editor, calculators, Roman numerals, random numbers, the ANSI editor, Magic Screen Blocker, the Prism effect, templates, the Labyrinth home screen, the update checker and 17 color themes.

### Fixed
- Japanese and Chinese IME input in live search.
- Sticky notes reopen on the right monitor.
- Emoji are no longer lost when saving.
- Layout of the main window on .NET 10.
- Opening folders and web links, and choosing the remote backup folder.
- Numbers in settings files saved with a comma decimal separator (for example on Danish Windows) are read correctly.
- Many smaller fixes from a full code review.

## Earlier versions

The changes in AHOY! 1.5.0.3 to 1.6.1.1 (2019-2022) are in [docs/CHANGELOG-1.x.txt](docs/CHANGELOG-1.x.txt).
