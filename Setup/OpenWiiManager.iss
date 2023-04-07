#define MyAppName "Open Wii Manager"
#define MyAppVersion GetStringFileInfo("..\OpenWiiManager\bin\Release\net6.0-windows\OpenWiiManager.exe", PRODUCT_VERSION)
#define MyAppPublisher "Jonas Kohl"
#define MyAppURL "https://owm.jonaskohl.de/"
#define MyAppExeName "OpenWiiManager.exe"

[Setup]
AppId={{C13CFC23-8F43-4B00-9A75-9B44601549E5}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\Jonas Kohl\Open Wii Manager
DefaultGroupName={#MyAppName}
LicenseFile=D:\Dev\repos\OpenWiiManager\COPYING
OutputBaseFilename=owm-setup
Compression=lzma
SolidCompression=yes
WizardStyle=modern
WizardSizePercent=100,100
WindowResizable=no
DisableWelcomePage=False

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "..\OpenWiiManager\bin\Release\net6.0-windows\OpenWiiManager.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\OpenWiiManager\bin\Release\net6.0-windows\AeroWizard.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\OpenWiiManager\bin\Release\net6.0-windows\Flurl.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\OpenWiiManager\bin\Release\net6.0-windows\Flurl.Http.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\OpenWiiManager\bin\Release\net6.0-windows\LICENSE.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\OpenWiiManager\bin\Release\net6.0-windows\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\OpenWiiManager\bin\Release\net6.0-windows\Ookii.Dialogs.WinForms.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\OpenWiiManager\bin\Release\net6.0-windows\OpenWiiManager.deps.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\OpenWiiManager\bin\Release\net6.0-windows\OpenWiiManager.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\OpenWiiManager\bin\Release\net6.0-windows\OpenWiiManager.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\OpenWiiManager\bin\Release\net6.0-windows\OpenWiiManager.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\OpenWiiManager\bin\Release\net6.0-windows\System.ServiceModel.Syndication.dll"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:ProgramOnTheWeb,{#MyAppName}}"; Filename: "{#MyAppURL}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Code]

function IsDotNetInstalled(DotNetName: string): Boolean;
var
  Cmd, Args: string;
  FileName: string;
  Output: AnsiString;
  Command: string;
  ResultCode: Integer;
begin
  FileName := ExpandConstant('{tmp}\dotnet.txt');
  Cmd := ExpandConstant('{cmd}');
  Command := 'dotnet --list-runtimes';
  Args := '/C ' + Command + ' > "' + FileName + '" 2>&1';
  if Exec(Cmd, Args, '', SW_HIDE, ewWaitUntilTerminated, ResultCode) and
     (ResultCode = 0) then
  begin
    if LoadStringFromFile(FileName, Output) then
    begin
      if Pos(DotNetName, Output) > 0 then
      begin
        Log('"' + DotNetName + '" found in output of "' + Command + '"');
        Result := True;
      end
        else
      begin
        Log('"' + DotNetName + '" not found in output of "' + Command + '"');
        Result := False;
      end;
    end
      else
    begin
      Log('Failed to read output of "' + Command + '"');
    end;
  end
    else
  begin
    Log('Failed to execute "' + Command + '"');
    Result := False;
  end;
  DeleteFile(FileName);
end;

function InitializeSetup(): Boolean;
var
  ErrCode: integer;
begin
  if not IsDotNetInstalled('Microsoft.WindowsDesktop.App 6.0.') then begin
    if MsgBox('This application requires Microsoft .NET Desktop Runtime 6, which is not installed on your system. '#
      'Please click "Yes" or visit'#13#13
      'https://aka.ms/dotnet/6.0/windowsdesktop-runtime-win-x64.exe'#13#13
      'to download it. Re-run this setup program afterwards.', mbCriticalError, MB_YESNO) = IDYES then
    begin
      ShellExec('open', 'https://aka.ms/dotnet-core-applaunch?framework=Microsoft.WindowsDesktop.App&framework_version=6.0.0', '', '', SW_SHOW, ewNoWait, ErrCode);
    end;
    result := false;
  end else
    result := true;
end;
