[Setup]
AppName=C0BR1NHADA
AppVersion=1.0
AppPublisher=PLASMA STUDIOS
DefaultDirName={pf}\C0BR1NHADA
DefaultGroupName=C0BR1NHADA
OutputBaseFilename=C0BR1NHADA_Setup_v1.0
SetupIconFile=C:\Users\amand\Desktop\cobrinhada\instalador\cobrinhada.ico
Compression=lzma
SolidCompression=yes

[Files]
Source: "C:\Users\amand\Desktop\cobrinhada\c0br1nhada\c0br1nhada\bin\Release\c0br1nhada.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\amand\Desktop\cobrinhada\c0br1nhada\c0br1nhada\bin\Release\c0br1nhada.exe.config"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\C0BR1NHADA"; Filename: "{app}\c0br1nhada.exe"
Name: "{group}\Desinstalar C0BR1NHADA"; Filename: "{uninstallexe}"

[Run]
Filename: "{app}\c0br1nhada.exe"; Description: "Iniciar C0BR1NHADA"; Flags: nowait postinstall skipifsilent
