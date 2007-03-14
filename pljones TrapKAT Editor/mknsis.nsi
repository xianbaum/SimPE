XPStyle on
SetCompressor /SOLID LZMA

Var wasInUse

InstallDir $PROGRAMFILES\TrapKATEditor
InstallDirRegKey HKLM Software\pljones\TrapKATEditor InstallDir
Name "pljones TrapKAT Editor"
!include outfile.txt

LicenseData "LICENSE.txt"

Page license
Page directory
Page instfiles

Section
  SetOutPath $INSTDIR
  Call CheckExists
  IfErrors 0 +2
  Abort "Cannot continue to install with TrapKATEditor running."
  WriteRegStr HKLM Software\pljones\TrapKATEditor InstallDir $INSTDIR
  File /r "TrapKATEditor\Program Files\TrapKATEditor\*.*"
  WriteUninstaller uninstall.exe
SectionEnd

Section "Uninstall"
  Delete $INSTDIR\CHANGES.txt
  Delete $INSTDIR\LICENSE.txt
  Delete $INSTDIR\TrapKATEditor.exe
  Delete $INSTDIR\uninstall.exe
  Delete $INSTDIR\version.txt
  RMDir $INSTDIR
  DeleteRegKey HKCU Software\pljones\TrapKATEditor
  DeleteRegKey HKLM Software\pljones\TrapKATEditor
SectionEnd

Function .onInstSuccess
  StrCmp $wasInUse 1 WasInUse
  Return
WasInUse:
  Exec "$INSTDIR\TrapKATEditor.exe"
FunctionEnd


Function CheckExists
  StrCpy $wasInUse 0

  IfFileExists "$INSTDIR\TrapKATEditor.exe" Exists
  Return
Exists:
  FileOpen $0 "$INSTDIR\TrapKATEditor.exe" a
  IfErrors InUse
  FileClose $0
  Return
InUse:
  StrCpy $wasInUse 1

  MessageBox MB_RETRYCANCEL \
    "TrapKATEditor is running.$\r$\nPlease close it and retry.$\r$\nIt will be restarted after the install." \
    IDRETRY Exists
  SetErrors
FunctionEnd