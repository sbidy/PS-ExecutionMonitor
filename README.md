# Description
A small tool to monitor the Windows Event Log for malicious events. If an event occurs a short message shows up. If a (hard) configured threshold is reached, the tool shows an apparent error. Showing a message is not the best action but suitable for a use case.

This PoC shows a extrem simple approach to detect PowerShell events in a Windows eventlog. The idea behind is, to feed a av-scanner with this additional input.
As example the av-scanner recognize a event an can log the executed PS command. A memdump from the PowerShell process can be also additionally created.

The tool scanns the event log in a 2 sec. loop.

## Suspend mode
If the suspend mode is checked the tool suspends all PS processes. To release these click "release processes".

## Paranoid mode
The paranoid mode enables a strict detection of all PowerShell interactions. Also the command prompt.

# Usage

https://github.com/sbidy/PoMs/releases

Here you can find the compiled "PoMs.exe" (x64 and x86).
After you start the tool, a small tray icon lives in your icon bar. To close or configure the PS-ExecutionMonitor, click right on the icon.

# Credits
Idea was originated from Nikhil Mittals talk at BlackHat USA 2016 - "AMSI: How Windows 10 Plans to Stop Script-Based Attacks and
How Well It Does It".
Link: http://www.slideshare.net/nikhil_mittal/amsi-how-windows-10-plans-to-stop-scriptbased-attacks-and-how-well-it-does-it

# Status
Beta - it is a PoC and the project is optimized for Visual Studio

# Requirements
.Net 3.5 - Windows 7 x64/x86 or later

# Author
Stephan Traub @sbidy

# License
MIT (see License.md)
