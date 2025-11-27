---
layout: "post"
title: "Suppress folder in assembly"
date: "2015-05-08 05:30:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/05/suppress-folder-in-assembly.html "
typepad_basename: "suppress-folder-in-assembly"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Inside an assembly you can create a folder in the browser and place multiple components in it. You can then control visibility and suppression of all those elements through the folder.</p>
<p>When you right-click on a folder you get a context menu with the &quot;<strong>Suppress</strong>&quot; menu item in it. If you click it <strong>Inventor</strong> will go through all the elements in the folder and suppress them. We can do the same thing through the <strong>API</strong>: iterate through the folder items and suppress them.<br />Note: the <strong>BrowserFolder</strong> object has no <strong>Suppress</strong> functionality in the <strong>API</strong> that could make the below code much simpler</p>
<p>In case of suppressing an occurrence pattern we have to go though each element, iterate through the occurrences they reference and suppress those, as shown in this blog post: <br /><a href="http://adndevblog.typepad.com/manufacturing/2012/05/how-can-i-suppress-the-circular-pattern-component-in-an-assembly-file.html" target="_self" title="">http://adndevblog.typepad.com/manufacturing/2012/05/how-can-i-suppress-the-circular-pattern-component-in-an-assembly-file.html</a>&#0160;</p>
<p>Here is the <strong>VBA</strong> code that suppresses the contents of the &quot;<strong>MyFolder</strong>&quot; browser folder:</p>
<pre>Sub SuppressFolderTest()
  Dim oDoc As AssemblyDocument
  Set oDoc = ThisApplication.ActiveDocument
    
  Dim oPane As BrowserPane
  Set oPane = oDoc.BrowserPanes(&quot;Model&quot;)
  
  Dim oTopNode As BrowserNode
  Set oTopNode = oPane.TopNode
    
  Dim oFolder As BrowserFolder
  Set oFolder = oTopNode.BrowserFolders.Item(&quot;MyFolder&quot;)
  
  Call SuppressFolder(oFolder)
End Sub

&#39; Recursive function as there could be subfolders too
Sub SuppressFolder(oFolder As BrowserFolder)
  Dim oItem As BrowserNode
  For Each oItem In oFolder.BrowserNode.BrowserNodes
    Dim oObj As Object
    Set oObj = oItem.NativeObject
    
    If TypeOf oObj Is BrowserFolder Then
      Call SuppressFolder(oObj)
    ElseIf TypeOf oObj Is ComponentOccurrence Then
      Dim oOcc As ComponentOccurrence
      Set oOcc = oObj
      
      If Not oOcc.Suppressed Then Call oOcc.Suppress
    ElseIf TypeOf oObj Is OccurrencePattern Then
      Dim oPatt As OccurrencePattern
      Set oPatt = oObj
      
      Dim oElem As OccurrencePatternElement
      For Each oElem In oPatt.OccurrencePatternElements
        For Each oOcc In oElem.Occurrences
          If Not oOcc.Suppressed Then Call oOcc.Suppress
        Next
      Next
    End If
  Next
End Sub</pre>
<p>Here is the same in <strong>iLogic</strong>:</p>
<pre>Sub Main()
  Dim oDoc As AssemblyDocument
  oDoc = ThisApplication.ActiveDocument
    
  Dim oPane As BrowserPane
  oPane = oDoc.BrowserPanes(&quot;Model&quot;)
  
  Dim oTopNode As BrowserNode
  oTopNode = oPane.TopNode
    
  Dim oFolder As BrowserFolder
  oFolder = oTopNode.BrowserFolders.Item(&quot;MyFolder&quot;)
  
  Call SuppressFolder(oFolder)
End Sub

&#39; Recursive function as there could be subfolders too
Sub SuppressFolder(oFolder As BrowserFolder)
  Dim oItem As BrowserNode
  For Each oItem In oFolder.BrowserNode.BrowserNodes
    Dim oObj As Object
    oObj = oItem.NativeObject
    
    If TypeOf oObj Is BrowserFolder Then
      Call SuppressFolder(oObj)
    ElseIf TypeOf oObj Is ComponentOccurrence Then
      Dim oOcc As ComponentOccurrence
      oOcc = oObj
      
      If Not oOcc.Suppressed Then Call oOcc.Suppress
    ElseIf TypeOf oObj Is OccurrencePattern Then
      Dim oPatt As OccurrencePattern
      oPatt = oObj
      
      Dim oElem As OccurrencePatternElement
      For Each oElem In oPatt.OccurrencePatternElements
        For Each oOcc In oElem.Occurrences
          If Not oOcc.Suppressed Then Call oOcc.Suppress
        Next
      Next
    End If
  Next
End Sub</pre>
<p>And here is the code in action. As you can see we get exactly the same result as if you clicked the &quot;<strong>Suppress</strong>&quot; context menu item of the folder in the UI, and the &quot;<strong>Suppress</strong>&quot; item will be ticked:&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb082becc6970d-pi" style="display: inline;"><img alt="Suppressfolder" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb082becc6970d image-full img-responsive" src="/assets/image_7d8e74.jpg" title="Suppressfolder" /></a></p>
<p>&#0160;</p>
