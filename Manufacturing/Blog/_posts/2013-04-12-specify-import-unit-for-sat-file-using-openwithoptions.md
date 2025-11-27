---
layout: "post"
title: "Specify Import Unit for SAT file using OpenWithOptions"
date: "2013-04-12 05:14:38"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/04/specify-import-unit-for-sat-file-using-openwithoptions.html "
typepad_basename: "specify-import-unit-for-sat-file-using-openwithoptions"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>The 2014 version of the Inventor API Help file provides better description of the available options of <strong>OpenWithOptions</strong>:</p>
<p><span style="color: #0000bf;"><strong>Documents.OpenWithOptions Method</strong></span></p>
<p><span style="color: #0000bf;">Parent Object: Documents&#0160;</span></p>
<p><span style="color: #0000bf;"><strong>Description&#0160;</strong></span></p>
<p><span style="color: #0000bf;">Method that opens the specified Inventor document.&#0160;</span></p>
<p><span style="color: #0000bf;"><strong>Syntax&#0160;</strong></span></p>
<p><span style="color: #0000bf;">Documents.<strong>OpenWithOptions</strong>( <strong><em>FullDocumentName</em></strong> As String, <strong><em>Options</em></strong> As NameValueMap, [<strong><em>OpenVisible</em></strong>] As Boolean ) As Document&#0160;</span></p>
<p><span style="color: #0000bf;"><strong>Parameters&#0160;</strong></span></p>
<table border="1" cellpadding="1" cellspacing="0">
<tbody>
<tr>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">Name&#0160;</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">Description&#0160;</span></p>
</td>
</tr>
<tr>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">FullDocumentName</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">Input String that specifies the full document name of the document to open. If only the FullFileName is specified for an assembly, the master document within the file is opened.</span></p>
</td>
</tr>
<tr>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">Options</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">Input NameValueMap object that specifies additional options for open. (An empty NameValueMap object can be provided). See Remarks section for the valid options.</span></p>
</td>
</tr>
<tr>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">OpenVisible</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">Optional input Boolean that specifies whether to open the document as visible. If not specified, the document is opened visible.</span></p>
</td>
</tr>
</tbody>
</table>
<p><span style="color: #0000bf;"><strong>Remarks&#0160;</strong></span></p>
<p><span style="color: #0000bf;">Valid values for the NameValueMap in the Options argument:&#0160;</span></p>
<table border="1" cellpadding="1" cellspacing="0">
<tbody>
<tr>
<td valign="middle">
<p><span style="color: #0000bf;"><strong>Name</strong></span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf;"><strong>Type</strong></span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf;"><strong>Valid Document Type</strong></span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf;"><strong>Notes</strong></span></p>
</td>
</tr>
<tr>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">DesignViewRepresentation</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">String</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">Part, Assembly</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">The name of the design view representation.</span></p>
</td>
</tr>
<tr>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">PositionalRepresentation</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">String</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">Assembly</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">The name of the positional representation.</span></p>
</td>
</tr>
<tr>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">LevelOfDetailRepresentation</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">String</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">Assembly</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">Typically, the LevelOfDetailRepresentation to use should be provided in the form of a FulDocumentName (first argument). But if this is provided separately, you should make sure that it does not conflict with the FullDocumentName argument by providing FullFileName as the first argument rather than a FullDocumentName.</span></p>
</td>
</tr>
<tr>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">DeferUpdates</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">Boolean</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">Drawing</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">Indicates if any pending updates for the drawing will be deferred when the drawing is opened.</span></p>
</td>
</tr>
<tr>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">FileVersionOption</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">Value from FileVersionEnum</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">All</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">Valid values for FileVersionEnum are kOpenOldVersion, kOpenCurrentVersion and kRestoreOldVersionToCurrent. If set to kOpenOldVersion, save will not be allowed on the opened document. kRestoreOldVersionToCurrent is valid only if no other versions are open and the current version is not checked out.</span></p>
</td>
</tr>
<tr>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">ImportNonInventorDWG</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">Boolean</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">Imports the DWG file to an IDW if True, Opens it into Inventor DWG if False</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">When opening non-Inventor DWG files, this method honors the application option to decide between open and import, unless an override is specified in the Options argument.</span></p>
</td>
</tr>
<tr>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">Password</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">String</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">All</span></p>
</td>
<td valign="middle">
<p>&#0160;</p>
</td>
</tr>
<tr>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">SkipAllUnresolvedFiles</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">Boolean</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">All</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">Indicates to skip all unresolved files and continue to open the document.</span></p>
</td>
</tr>
<tr>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">ExpressModeBehavior</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">String</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">Assembly</span></p>
</td>
<td valign="middle">
<p><span style="color: #0000bf; font-size: 8pt;">The following values are valid for this setting:</span><br />
<br /><span style="color: #0000bf; font-size: 8pt;">OpenExpress - Open the assembly in express mode.</span><br />
<br /><span style="color: #0000bf; font-size: 8pt;">OpenFull - Open the assembly in full mode.</span><br />
<br /><span style="color: #0000bf; font-size: 8pt;">OpenDefault - Open the assembly in the mode it was saved in.&#0160;</span></p>
</td>
</tr>
</tbody>
</table>
<p>As you can see it does not seem to have the option for Import Unit. However the <strong>SAT Translator</strong> Addin has that. So we can use that instead. The API Help File&#39;s <strong>Translator Options</strong> topic contains all the available options, including <strong>ImportUnit</strong>: SOURCE_UNITS = 0,
TEMPLATE_UNITS = 1, INCH = 2, FOOT = 3, CENTIMETER = 4, MILLIMETER = 5, METER = 6, MICRON = 7.&#0160;</p>
<p>  
You can start with e.g. the API Help File&#39;s&#0160;<strong>Open an STL file using the STL Translator Sample API Sample</strong> and modify it to use the <strong>SAT Translator</strong> instead along with the <strong>ImportUnit</strong> setting:</p>
<pre>Sub ImportFunc()
    &#39; Set SAT translator&#39;s CLSID and SAT file name.
    Dim strCLSID As String
    Dim strFileName As String
    strCLSID = &quot;{89162634-02B6-11D5-8E80-0010B541CD80}&quot;
    strFileName = &quot;C:\Sample.sat&quot;
      
    Dim oAddIns As ApplicationAddIns
    Set oAddIns = ThisApplication.ApplicationAddIns
    
    &#39; Find the SAT translator, get the CLSID and activate it.
    Dim oTransAddIn As TranslatorAddIn
    Set oTransAddIn = oAddIns.ItemById(strCLSID)
    oTransAddIn.Activate
    
    &#39; Get the transient object and take it as a factory
    &#39; to produce other objects
    Dim transientObj As TransientObjects
    Set transientObj = ThisApplication.TransientObjects
    
    &#39; Prepare the first parameter for Open(), the file name
    Dim file As DataMedium
    Set file = transientObj.CreateDataMedium
    file.FileName = strFileName
    
    &#39; Prepare the second parameter for Open(), the open type.
    Dim context As TranslationContext
    Set context = transientObj.CreateTranslationContext
    context.Type = kDataDropIOMechanism
    
    &#39; Prepare the 3rd parameter for Open(), the options.
    Dim options As NameValueMap
    Set options = transientObj.CreateNameValueMap
    
    &#39; Have a look at the API Help File&#39;s &#39;Translator Options&#39;
    options.Value(&quot;ImportUnit&quot;) = 4 &#39;CENTIMETER
    
    &#39; Prepare the fourth parameter for Open(), the final document.
    Dim sourceObj As Object
    
    &#39; Open the SAT file.
    oTransAddIn.Open file, context, options, sourceObj
End Sub</pre>
