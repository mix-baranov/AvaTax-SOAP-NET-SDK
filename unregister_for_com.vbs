Set wshShell = CreateObject("WScript.Shell") 
curPath = Property("CustomActionData")

If not IsNull(wshShell) Then
	wshShell.CurrentDirectory = curPath
	wshShell.Run "unregister_for_com.bat", 0, true	
Else
	MsgBox "Error occured while unregistering Avalara.AvaTax.Adapter.dll.  You can run unregister_for_com.bat file from [" & curPath & "].", vbOKOnly & vbInformation, "Unregistration Failed"
End If

Set wshShell = Nothing