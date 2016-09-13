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

## For Windows 10 Anniversary Update(14393) or later version

- Download .appx
- Ref **Appx package deployment** section in [here](https://msdn.microsoft.com/en-us/windows/uwp/porting/desktop-to-uwp-deploy-and-debug)
1. In File Explorer, right click an appx that you've signed with a test cert and choose **Properties** from the context menu.
2. Click or tap the **Digital Signatures** tab.
3. Click or tap on the certificate and choose **Details**.
4. Click or tap **View Certificate**.
5. Click or tap **Install Certificate**.
6. In the **Store Location** group, select **Local Machine**.
7. Click or tap **Next** and **OK** to confirm the UAC dialog.
8. In the next screen of the Certificate Import Wizard, change the selected option to **Place all certificates in the following store**.
9. Click or tap **Browse**. In the Select Certificate Store window, scroll down and select **Trusted People** and click or tap **OK**.
10. Click or tap **Next**. A new screen appears. Click or tap **Finish**.
11. A confirmation dialog should appear. If so, click **OK**. If a different dialog indicates that there is a problem with the certificate, you may need to do some certificate troubleshooting.

    Note: For Windows to trust the certificate, the certificate must be located in either the **Certificates (Local Computer) > Trusted Root Certification Authorities > Certificates** node or the **Certificates (Local Computer) > Trusted People > Certificates** node. Only certificates in these two locations can validate the certificate trust in the context of the local machine. Otherwise, an error message that resembles the following string appears:

    > "Add-AppxPackage : Deployment failed with HRESULT: 0x800B0109, A
    > certificate chain processed, but terminated in a rootcertificate which
    > is not trusted by the trust provider. (Exception from HRESULT:
    > 0x800B0109) error 0x800B0109: The root certificate of the signature in
    > the app package must be trusted."
- Double-click .appx to install



## Win10 Mobile

Comming soon...

[openpowershell]: ../img/powershelladmin.jpg "open Windows PowerShell as administrator"
[exepowershell]: ../img/runpowershell.jpg "run PowerShell script"
[exepowershellsuc]: ../img/runpowershellsuccess.jpg "Installation successfully"
[installcer1]: ../img/installcer1.jpg "Step 1"
[installcer2]: ../img/installcer2.jpg "Step 2"