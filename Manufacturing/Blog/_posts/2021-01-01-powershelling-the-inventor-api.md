---
layout: "post"
title: "Power(Shell)ing the Inventor API!"
date: "2021-01-01 00:54:57"
author: "Sajith Subramanian"
categories:
  - "Inventor"
  - "Sajith Subramanian"
original_url: "https://adndevblog.typepad.com/manufacturing/2021/01/powershelling-the-inventor-api.html "
typepad_basename: "powershelling-the-inventor-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/sajith-subramanian.html" target="_self">Sajith Subramanian</a></p><p><br></p><p>If you are more comfortable using PowerShell and have an Inventor API workflow that you want to quickly test, you can just combine both!</p><p>To help demonstrate this, below is a Inventor API sample in PowerShell that exports a drawing file as PDF.</p><p>Once you have your Windows PowerShell Integrated Scripting Environment (ISE) up, the first line of code would be to add the Inventor reference DLL as below:</p><pre><p>Add-Type -Path ("C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\Autodesk.Inventor.Interop\v4.0_25.0.0.0__d84147f8b4276564\Autodesk.Inventor.Interop.dll")<br></p></pre><p><br><p>The next step would be to create an instance of Inventor (or get an existing instance if it is already open). To do so, you can add the below lines:<pre><p>try
{
$m_inventorApp = [Runtime.Interopservices.Marshal]::GetActiveObject('Inventor.Application')
$m_quitInventor = $false
echo 'Getting existing instance...'
}
catch
{<br>&nbsp;&nbsp;&nbsp;&nbsp; echo 'Creating new instance...'<br>&nbsp;&nbsp;&nbsp;&nbsp; $inventorAppType = [System.Type]::GetTypeFromProgID("Inventor.Application");<br>&nbsp;&nbsp;&nbsp;&nbsp; $m_inventorApp = [System.Activator]::CreateInstance($inventorAppType) <br>&nbsp;&nbsp;&nbsp;&nbsp; $m_quitInventor = $true</p><p>}</p></pre><p><br></p><p>Now, we open our input drawing document with the “Open as Visible” flag set to False, and use this document object as input to the PublishPdf function.</p><pre><p>$document = $m_inventorApp.Documents.Open("C:\TEMP\Hairdryer.idw", $false)
PublishPdf($document)</p></pre><p><br></p><p>Now, in our PublishPdf function we add the below lines:</p><pre><p>function PublishPdf {
</p><p>&nbsp;&nbsp; param ($doc)
</p><p>&nbsp;&nbsp; $PDFAddIn = $m_inventorApp.ApplicationAddIns.ItemById("{0AC6FD96-2F4D-42CE-8BE0-8AEA580399E4}")<br>&nbsp;&nbsp; $oContext = $m_inventorApp.TransientObjects.CreateTranslationContext() <br>&nbsp;&nbsp; $oContext.Type = [Inventor.IOMechanismEnum]::kFileBrowseIOMechanism</p><p>&nbsp;&nbsp; $oOptions = $m_inventorApp.TransientObjects.CreateNameValueMap()</p><p>&nbsp;&nbsp; $oDataMedium = $m_inventorApp.TransientObjects.CreateDataMedium()</p><p>&nbsp;&nbsp; If ($PDFAddIn.HasSaveCopyAsOptions($doc, $oContext, $oOptions)) <br>&nbsp;&nbsp;&nbsp; {<br>&nbsp;&nbsp;&nbsp;&nbsp; <br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; $oOptions.Value("All_Color_AS_Black") = 0<br>&nbsp;&nbsp;&nbsp; }<br>&nbsp;&nbsp;&nbsp; <br>&nbsp;&nbsp; $oDataMedium.FileName = "c:\temp\test.pdf"<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br>&nbsp;&nbsp; $PDFAddIn.SaveCopyAs($doc, $oContext, $oOptions, $oDataMedium)
}</p></pre><p><br></p><p>And finally, close the created Inventor instance:</p><pre>if ($m_inventorApp -ne $null -and $m_quitInventor -eq $true)
{
 $m_inventorApp.Quit()
 echo 'Quitting Inventor...'
} 
</pre><p><br></p><p>In the above example, while I have hard-coded the input file path, you can also choose to pass this as an input in a command line argument.</p>
