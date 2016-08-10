# Description
A small tool to monitor the Windows Event Log for malicious events. If an event occurs a short message shows up. If a (hard) configured threshold is reached, the tool shows an apparent error. Showing a message is not the best action but suitable for a use case.

This PoC shows a extrem simple approach to detect PowerShell events in a windows eventlog. The idea behind is, to feed a av-scanner with this additional input.
As example the av-scanner recognize a event an can log the executed command. A memdump from the PowerShell process can be also additionally created.

Idea was originated from Nikhil Mittals talk at BlackHat USA 2016 - "AMSI: How Windows 10 Plans to Stop Script-Based Attacks and
How Well It Does It".
Link: https://www.blackhat.com/docs/us-16/materials/us-16-Mittal-AMSI-How-Windows-10-Plans-To-Stop-Script-Based-Attacks-And-How-Well-It-Does-It.pdf

# Status
Beta - it is a PoC and the project is optimized for Visual Studio

# Requirements
.Net 4.5 - Windows 7 or later

# Author
Stephan Traub @sbidy

# License
MIT (see License.md)
