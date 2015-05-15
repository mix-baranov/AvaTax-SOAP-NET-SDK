Set wshShell = CreateObject("WScript.Shell") 
curPath = Property("CustomActionData")

If not IsNull(wshShell) Then
	wshShell.CurrentDirectory = curPath
	wshShell.Run "register_for_com.bat", 0	
Else
	MsgBox "Error occured while registering Avalara.AvaTax.Adapter.dll for COM component.  You can run register_for_com.bat file from [" & curPath & "].", vbOKOnly & vbInformation, "Registration Failed"
End If

Set wshShell = Nothing													
  		
  		
  												  