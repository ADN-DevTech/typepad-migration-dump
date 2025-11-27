---
layout: "post"
title: "Import and save non-native files"
date: "2015-11-25 13:15:57"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/11/import-and-save-non-native-files.html "
typepad_basename: "import-and-save-non-native-files"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>When you import a non-native file then you are using a translator add-in to do the import. Each of them might have different options which can affect how you implement things.</p>
<p>As shown in this blog post you can easily find out all the options available for a translator add-in:<br /><a href="http://adndevblog.typepad.com/manufacturing/2014/02/get-option-names-and-values-supported-by-inventor-translator-addins-via-api.html" target="_self" title="">http://adndevblog.typepad.com/manufacturing/2014/02/get-option-names-and-values-supported-by-inventor-translator-addins-via-api.html</a>&#0160;</p>
<p>For <strong>SolidWorks</strong>&#0160;translator add-in&#39;s open options you&#39;d get something like this in<strong> Inventor 2016</strong>:</p>
<pre>SaveComponentDuringLoad
 True
SaveLocationIndex
  1 
ComponentDestFolder
 C:\Users\adamnagy\Documents\Inventor\SW\
AssemDestFolder
 C:\Users\adamnagy\Documents\Inventor\SW\
SaveAssemSeperateFolder
 False
AddFilenamePrefix
 False
FilenamePrefix
 
AddFilenameSuffix
 False
FilenameSuffix
 
EmbedInDocument
 True
SaveToDisk
 False
ImportSolid
 True
ImportSurface
 True
ImportWire
 True
CreateIFO
 False
ImportAASP
 False
ImportAASPIndex
  0 
CreateSurfIndex
  1 
GroupName
 
GroupNameIndex
  0 
ExplodeMSB2Assm
 False
ImportUnit
  0 
CheckDuringLoad
 False
AutoStitchAndPromote
 True
AdvanceHealing
 False
EdgeSplitAndMergeDisabled
 False
FaceSplitAndMergeDisabled
 False
AssociativeImport
 False
Selective Import
 False
Link Visibility
 True</pre>
<p>E.g. if you want to import a complete <strong>SolidWorks</strong> assembly and save the created <strong>Inventor</strong> files, then the translator has an option for that. You can specify where the created files should be placed and also if they should be saved during the creation of those files, so you don&#39;t even have to iterate through them and save them - actually if you save the main assembly and use <br /><strong>Application.SilentOperation</strong> = <strong>True</strong>, that should do the trick too. &#0160;</p>
<p>The following code can automate the whole import and save part:</p>
<pre>Sub ImportAndSaveSolidWorksFiles()
  Dim oAddIns As ApplicationAddIns
  Set oAddIns = ThisApplication.ApplicationAddIns

  &#39; SolidWorks translator addin
  Dim oTA As TranslatorAddIn
  Set oTA = oAddIns.ItemById(&quot;{402BE503-725D-41CB-B746-D557AB83BAF1}&quot;)
  
  &#39; Activate if needed
  If Not oTA.Activated Then oTA.Activate
  
  Dim oTO As TransientObjects
  Set oTO = ThisApplication.TransientObjects
  
  Dim oDM As DataMedium
  Set oDM = oTO.CreateDataMedium
  oDM.FileName = &quot;C:\Gears\gears.SLDASM&quot;
 
  Dim oTC As TranslationContext
  Set oTC = oTO.CreateTranslationContext
  oTC.Type = kFileBrowseIOMechanism
 
  Dim oNVM As NameValueMap
  Set oNVM = oTO.CreateNameValueMap
 
  &#39; Show the options dialog if you want
  &#39; Call oTA.ShowOpenOptions(oDM, oTC, oNVM)
  &#39; and print out the available options
  &#39; Call PrintInfo(oNVM, 1)
  
  &#39; Set the options we need
  Call oNVM.Add(&quot;SaveComponentDuringLoad&quot;, True)
  Call oNVM.Add(&quot;SaveLocationIndex&quot;, 1)
  Call oNVM.Add(&quot;ComponentDestFolder&quot;, &quot;C:\Users\adamnagy\Documents\Inventor\SW\&quot;)
  Call oNVM.Add(&quot;SaveAssemSeperateFolder&quot;, False)
  
  Dim oDoc As Document
  Call oTA.Open(oDM, oTC, oNVM, oDoc)
End Sub

Sub PrintInfo(v As Variant, indent As Integer)
  If TypeOf v Is NameValueMap Then
    Dim nvm As NameValueMap
    Set nvm = v
    Dim i As Integer
    For i = 1 To nvm.Count
      Debug.Print Tab(indent); nvm.Name(i)
      Call PrintInfo(nvm.Value(nvm.Name(i)), indent + 1)
    Next
  Else
    Debug.Print Tab(indent); v
  End If
End Sub</pre>
<p>Result:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d17b2272970c-pi" style="display: inline;"><img alt="SWimport" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d17b2272970c image-full img-responsive" src="/assets/image_05b135.jpg" title="SWimport" /></a></p>
<p>&#0160;</p>
