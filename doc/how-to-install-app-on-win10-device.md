#How to install App on Win10 device

## Win10 PC

- Download [Release package](https://github.com/Myfreedom614/SOPerformanceTool/releases), x86/x64 based on your architecture 
- Unzip file and open Windows PowerShell as administrator
![PowerShell][openpowershell]
- Run ps script: .\Add-AppDevPackage.ps1
**Note, if you get the exception about execution policies, please see this document: [about_Execution_Policies](https://technet.microsoft.com/library/hh847748.aspx?f=255&MSPPError=-2147217396)**

Generally: `Set-ExecutionPolicy -ExecutionPolicy Unrestricted`
![Run PowerShell script][exepowershell]

If you see "Success: Your app was successfully installed", congratulations:)
![Installation successfully][exepowershellsuc]

## Win10 Mobile

Comming soon...

[openpowershell]: ../img/powershelladmin.jpg "open Windows PowerShell as administrator"
[exepowershell]: ../img/runpowershell.jpg "run PowerShell script"
[exepowershellsuc]: ../img/runpowershellsuccess.jpg "Installation successfully"
