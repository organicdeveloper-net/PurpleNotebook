[Setup]
AppName=Purple Notebook
AppVersion=1.0
DefaultDirName={localappdata}\PurpleNotebook
DefaultGroupName=Purple Notebook
OutputDir=H:\Purple Notebook PC\Purple Notebook\Installer\Output
OutputBaseFilename=PurpleNotebookInstaller
Compression=lzma
SolidCompression=yes
PrivilegesRequired=lowest
DirExistsWarning=no
LicenseFile=H:\Purple Notebook PC\Purple Notebook\Installer\LICENSE.txt

[Files]
; Main executable
Source: "H:\Purple Notebook PC\Purple Notebook\bin\Release\net8.0-windows10.0.19041.0\PurpleNotebook.exe"; DestDir: "{app}"; Flags: ignoreversion

; Assets folder
Source: "H:\Purple Notebook PC\Purple Notebook\assets\*"; DestDir: "{app}\assets"; Flags: ignoreversion recursesubdirs createallsubdirs

; DLLs and JSON configs
Source: "H:\Purple Notebook PC\Purple Notebook\bin\Release\net8.0-windows10.0.19041.0\*.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "H:\Purple Notebook PC\Purple Notebook\bin\Release\net8.0-windows10.0.19041.0\*.json"; DestDir: "{app}"; Flags: ignoreversion

; Theme and background files
Source: "H:\Purple Notebook PC\Purple Notebook\bin\Release\net8.0-windows10.0.19041.0\theme.txt"; DestDir: "{localappdata}\PurpleNotebook"; Flags: ignoreversion
Source: "H:\Purple Notebook PC\Purple Notebook\bin\Release\net8.0-windows10.0.19041.0\background.txt"; DestDir: "{localappdata}\PurpleNotebook"; Flags: ignoreversion

[Icons]
Name: "{group}\Purple Notebook"; Filename: "{app}\PurpleNotebook.exe"
Name: "{userdesktop}\Purple Notebook"; Filename: "{app}\PurpleNotebook.exe"; Tasks: desktopicon
Name: "{userdesktop}\Edit Purple Theme"; Filename: "notepad.exe"; Parameters: "{localappdata}\PurpleNotebook\theme.txt"

[Tasks]
Name: "desktopicon"; Description: "Create a desktop shortcut"; GroupDescription: "Additional icons:"
