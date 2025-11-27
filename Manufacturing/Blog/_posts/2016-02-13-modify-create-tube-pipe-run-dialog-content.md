---
layout: "post"
title: "Modify \"Create Tube & Pipe Run\" dialog content"
date: "2016-02-13 08:38:21"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/02/modify-create-tube-pipe-run-dialog-content.html "
typepad_basename: "modify-create-tube-pipe-run-dialog-content"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>When you switch to &quot;<strong>Tube &amp; Pipe</strong>&quot; environment ...</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08b9df47970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Tube&amp;Pipe1" class="asset  asset-image at-xid-6a0167607c2431970b01bb08b9df47970d img-responsive" src="/assets/image_abb513.jpg" title="Tube&amp;Pipe1" /></a></p>
<p>... then before the &quot;<strong>Create Tube &amp; Pipe Run</strong>&quot; dialog pops up, the <strong>OnPopulateFileMetadata</strong>&#0160;event of the <strong>FileUIEvents</strong> object will be called, which we can handle. To handle events we need a <strong>class</strong>, so I created one called &quot;<strong>clsEvents</strong>&quot; in <strong>VBA</strong>:&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08bb7f06970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="VBAclass" class="asset  asset-image at-xid-6a0167607c2431970b01bb08bb7f06970d img-responsive" src="/assets/image_610a3f.jpg" title="VBAclass" /></a></p>
<pre>&#39; Code of clsEvents Class
Dim WithEvents oFE As FileUIEvents

Private Sub Class_Initialize()
  Set oFE = ThisApplication.FileUIEvents
End Sub

Private Sub oFE_OnPopulateFileMetadata( _
ByVal FileMetadataObjects As ObjectsEnumerator, _
ByVal Formulae As String, _
ByVal Context As NameValueMap, _
HandlingCode As HandlingCodeEnum)
  
  &#39; Unfortunately the Context is not filled with info
  &#39; in case of the Tube &amp; Pipe dialog
  &#39; so we can only figure out if this event was
  &#39; fired by it through checking the metadata
  
  &#39; If not the correct amount of data, we&#39;re done
  If FileMetadataObjects.Count &lt;&gt; 2 Then Exit Sub
  
  Dim fmd1 As FileMetadata
  Set fmd1 = FileMetadataObjects(1)
  
  &#39; If the suggested assembly file name does not
  &#39; contain &quot;Tube and Pipe Runs&quot;, we&#39;re done
  If InStr(1, fmd1.FileName, &quot;Tube and Pipe Runs&quot;) &lt; 1 Then Exit Sub

  Dim fmd2 As FileMetadata
  Set fmd2 = FileMetadataObjects(2)

  fmd1.FullFileName = &quot;C:\temp\myruns\runs.iam&quot;
  fmd2.FullFileName = &quot;C:\temp\myruns\myrun1\run1.iam&quot;

  HandlingCode = kEventHandled
End Sub</pre>
<p>In order to start listening to this event we need to instantiate our <strong>class</strong> and keep a <strong>global reference</strong> to it. This could be done e.g. inside a <strong>VBA</strong> <strong>module</strong>. If you also want your command to automatically start the <strong>Tube &amp; Pipe</strong> environment then you can do it as shown in the below code, based on this blog post: <a href="http://modthemachine.typepad.com/my_weblog/2009/03/running-commands-using-the-api.html">http://modthemachine.typepad.com/my_weblog/2009/03/running-commands-using-the-api.html</a></p>
<pre>&#39; Global reference to the event listener 
Dim oEvents As clsEvents

Sub StartListeningToEvents()
  Set oEvents = New clsEvents
  
  &#39; Uncomment below code if you want to start the
  &#39; Tube &amp; Pipe environment
  &#39;Dim oCDs As ControlDefinitions
  &#39;Set oCDs = ThisApplication.CommandManager.ControlDefinitions
  
  &#39;Dim oCD As ControlDefinition
  &#39;Set oCD = oCDs(&quot;HSL:Piping:CreatePipeRun&quot;)
  
  &#39;Call oCD.Execute
End Sub</pre>
<p>And this change will be reflected in the dialog:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8151256970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Tube&amp;Pipe3" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8151256970b img-responsive" src="/assets/image_5243fb.jpg" title="Tube&amp;Pipe3" /></a></p>
<p>If we click <strong>OK</strong>, we end up with a model like this:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d19f36cf970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Tube&amp;Pipe4" class="asset  asset-image at-xid-6a0167607c2431970b01b8d19f36cf970c img-responsive" src="/assets/image_45c96d.jpg" title="Tube&amp;Pipe4" /></a></p>
<p>You can also hook up your <strong>VBA</strong> macro to a button in the <strong>UI</strong> following this blog post:<br /><a href="http://modthemachine.typepad.com/my_weblog/2010/03/buttons-for-vba-macros-in-the-ribbon-user-interface.html">http://modthemachine.typepad.com/my_weblog/2010/03/buttons-for-vba-macros-in-the-ribbon-user-interface.html</a></p>
