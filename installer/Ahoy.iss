; Inno Setup script for AHOY! (https://jrsoftware.org/isinfo.php)
; Built by build-release.ps1, which passes the version and the folders:
;   ISCC.exe /DAppVersion=2.0.0 /DSourceDir=..\publish\selfcontained /DOutputDir=..\publish installer\Ahoy.iss
; The application is self-contained (.NET included), so nothing else needs to be installed.
; Notes are kept in %APPDATA%\Ahoy and are never removed by the uninstaller.

#ifndef AppVersion
  #define AppVersion "2.0.0"
#endif
#ifndef SourceDir
  #define SourceDir "..\publish\selfcontained"
#endif
#ifndef OutputDir
  #define OutputDir "..\publish"
#endif

[Setup]
AppId={{D281771A-454A-4382-8B12-3D1519A9BF13}
AppName=AHOY!
AppVersion={#AppVersion}
AppVerName=AHOY! {#AppVersion}
AppPublisher=Lars Brandt Stisen
AppPublisherURL=https://github.com/artstisen/AHOY
AppSupportURL=https://github.com/artstisen/AHOY/issues
AppUpdatesURL=https://github.com/artstisen/AHOY/releases
AppCopyright=Copyright (C) 2019-2026 Lars Brandt Stisen
DefaultDirName={autopf}\AHOY
DefaultGroupName=AHOY!
DisableProgramGroupPage=yes
LicenseFile=..\LICENSE
OutputDir={#OutputDir}
OutputBaseFilename=Ahoy-{#AppVersion}-setup-win-x64
SetupIconFile=..\AhoyBrowser\Ahoy.ico
UninstallDisplayIcon={app}\Ahoy.exe
UninstallDisplayName=AHOY! {#AppVersion}
Compression=lzma2/max
SolidCompression=yes
WizardStyle=modern
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
MinVersion=10.0
; Installs for the current user without administrator rights; "for all users" can be chosen in the wizard
PrivilegesRequired=lowest
PrivilegesRequiredOverridesAllowed=dialog
; AHOY! allows one running instance: ask to close it before files are replaced
CloseApplications=yes
RestartApplications=no
VersionInfoVersion={#AppVersion}
VersionInfoProductName=AHOY!
VersionInfoDescription=AHOY! setup

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "{#SourceDir}\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "..\LICENSE"; DestDir: "{app}"; DestName: "LICENSE.txt"; Flags: ignoreversion

[Icons]
Name: "{autoprograms}\AHOY!"; Filename: "{app}\Ahoy.exe"
Name: "{autodesktop}\AHOY!"; Filename: "{app}\Ahoy.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\Ahoy.exe"; Description: "{cm:LaunchProgram,AHOY!}"; Flags: nowait postinstall skipifsilent
