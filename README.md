# AHOY!

**Encrypted notes for Windows.** AHOY! keeps all your notes in one password-encrypted file, the *Pool*, with fast search, a pinned list with colors and categories, sticky notes on the desktop and automatic backups. Free and open source since 2019.

![AHOY! main window with search results, pinned list, categories and context menus](docs/images/ahoy-screenshot.png)

<sub>Screenshot from an earlier release. Some features and decorations shown have since been removed or simplified.</sub>

## Specifications

| | |
|---|---|
| Version | 2.0.0 |
| Platform | Windows 10 and 11 (x64) |
| Runtime | .NET 10 (Windows Desktop) |
| Encryption | AES-256-GCM, key derived from your password with PBKDF2-HMAC-SHA256 (600,000 iterations) |
| Storage | One encrypted Pool file plus encrypted backups, in `%APPDATA%\Ahoy` (or a folder you choose) |
| Network | None: AHOY! never connects to the Internet |
| Themes | Dark, Light and Blue, with custom colors |
| License | MIT |

## Features

**Notes**
- Unlimited entries in one encrypted Pool, saved automatically
- Title and content search with live search as you type, plus search and replace inside an entry
- History (back/forward), two Hold buttons for switching between entries, bookmarks and global undo
- A memo field next to the pinned list

**Organizing**
- Pinned list with 8 quick colors, 20 color samples and custom colors
- Categories: group entries under a category entry and expand or collapse them in the pinned list
- Sorting, duplicating and marking entries for export

**Writing tools**
- Spell checker (uses the languages installed in Windows)
- Paragraph dividers, timestamps, text operations (sort, case, whitespace) and simple decorations (underline, bullets)
- Word and WPM counter
- Password generator and message encryption (encrypt a selected text to share it)

**Desktop**
- Sticky notes, saved in the Pool and restored on the right monitor
- Auto-lock after a period of inactivity, read-only mode, full screen and adjustable opacity
- Command prompt (Ctrl+Alt+A) and keyboard shortcuts for nearly everything; see Help -> Quick Guide

**Safety**
- Rotating local backups and an optional remote backup folder (for example a cloud-synced folder)
- Backup recovery, import/export of selected entries
- Unexpected errors are reported and logged instead of closing silently

## Security

Your password is the key to the Pool:

- The password is never stored. It is only used to derive the 256-bit encryption key (PBKDF2-HMAC-SHA256, 600,000 iterations, random 128-bit salt per Pool).
- The Pool is encrypted with AES-256-GCM, with a new random nonce on every save. The authentication tag covers the whole file, so any change or corruption is detected.
- **There is no password recovery.** If you forget your password, nobody can decrypt your notes, including the developer. Keep the password somewhere safe, for example in a password manager.

What is and isn't encrypted:

- **Encrypted:** the Pool and its backups, including entries, memo, sticky notes and the event log.
- **Not encrypted:** `Settings.xml` (window size, theme and similar preferences), entries you export, and `Crash.log` (error details only, never note content).

### Upgrading from AHOY! 1.x

AHOY! 1.x encrypted the Pool with a key built into the program, so the password did not protect the file itself. The first time AHOY! 2.0 opens a 1.x Pool it will:

1. ask for your current password,
2. ask you to create a new password,
3. save the Pool in the new format and verify it,
4. offer to delete old-format backups, which anyone with AHOY! 1.x could read.

If you cancel, the old Pool is left unchanged. Old copies kept elsewhere (cloud history, USB drives, the Recycle Bin) are not touched and should be deleted manually.

## Download and run

Download the latest version from [Releases](../../releases). If Windows SmartScreen or Smart App Control blocks it, compare the file's checksum with the one on the release page, then choose *More info -> Run anyway* (or allow it in Windows Security).

AHOY! stores its data in `%APPDATA%\Ahoy`. To use another folder, for example to try a copy of your Pool, start it with:

```
Ahoy.exe --data-dir "C:\Path\To\Folder"
```

## Build from source

Requirements: Windows and the [.NET 10 SDK](https://dotnet.microsoft.com/download).

```
dotnet build -c Release
dotnet test
```

Publish a small build that needs the .NET 10 Desktop Runtime installed:

```
dotnet publish AhoyBrowser -c Release -r win-x64 --self-contained false
```

Or a single self-contained `Ahoy.exe` that needs no installed runtime (about 170 MB):

```
dotnet publish AhoyBrowser -c Release -r win-x64 --self-contained -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true
```

| Folder | Contents |
|---|---|
| `AhoyBrowser/` | The Windows Forms application |
| `AhoyBrowser/Crypto/` | Pool and message encryption, and the read-only reader for AHOY! 1.x Pools |
| `AhoyBrowser.Tests/` | Tests for encryption, 1.x compatibility and saving, loading and upgrading Pools |

## What's new in 2.0

- Runs on .NET 10 (previously .NET Framework 4.8)
- Password-based AES-256-GCM encryption replaces 3DES with a built-in key
- Security question and password reset removed (they can't work with real encryption)
- Leaner: Typewriter, Typermax, notifications, Utility/PLOP Editor, calculators, Roman numerals, random numbers, ANSI editor, Magic Screen Blocker, Prism effect, templates, the update checker and 17 color themes were removed
- Plain ASCII and drawn graphics instead of special characters, so AHOY! looks right with any font and on East Asian systems
- Fixes for Japanese/Chinese IME input in live search, sticky note positions on multiple monitors, and emoji being lost when saving

## License

[MIT](LICENSE) © 2019-2026 LBS
