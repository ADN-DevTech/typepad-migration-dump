---
layout: "post"
title: "Demote OccurrencePattern"
date: "2016-06-22 11:04:12"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/06/demote-occurrencepattern.html "
typepad_basename: "demote-occurrencepattern"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>There is a sample in the <strong>API Help</strong> about demoting an occurrence using the&#0160;<strong>BrowserPane.Reorder</strong> function&#0160;- search for &quot;<span class="s1"><strong>Demote</strong></span><strong> occurrence API Sample</strong>&quot;<br />However, it does not work in case of an <strong>OccurrencePattern</strong>, because that is not supported by the product either:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0915712a970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="OccurrenceVsPattern" class="asset  asset-image at-xid-6a0167607c2431970b01bb0915712a970d img-responsive" src="/assets/image_613f6d.jpg" title="OccurrenceVsPattern" /></a><br />If you think this would be something useful to have in the product then please log it on the <strong>IdeaStation</strong>:<br /><a href="http://forums.autodesk.com/t5/inventor-ideas/idb-p/v1232/tab/most-recent">http://forums.autodesk.com/t5/inventor-ideas/idb-p/v1232/tab/most-recent</a></p>
<p>There is another way to demote a component however: use&#0160;the &quot;<strong>AssemblyDemoteCmd</strong>&quot; control definition.<br />Just make sure that you are selecting the correct component - if you select <strong>Occurrences(1)</strong> that will only return an <strong>item inside the pattern</strong> and not the <strong>pattern object</strong> itself:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb091571d3970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="OccurrencePattern" class="asset  asset-image at-xid-6a0167607c2431970b01bb091571d3970d img-responsive" src="/assets/image_bb98d6.jpg" title="OccurrencePattern" /></a></p>
<pre>Sub DemoteWithCommand()

Dim oDoc As AssemblyDocument
Set oDoc = ThisApplication.ActiveDocument

Dim oDef As AssemblyComponentDefinition
Set oDef = oDoc.ComponentDefinition

&#39;Set a reference to the first occurrence
Dim oOcc As ComponentOccurrence
Set oOcc = oDef.Occurrences.Item(1)

Dim oItemToMove As Object
If oOcc.IsPatternElement Then
    Set oItemToMove = oOcc.PatternElement.Parent
Else
    Set oItemToMove = oOcc
End If

&#39;Clear select set
oDoc.SelectSet.Clear

&#39;Add the occurrence to demote to the select set
oDoc.SelectSet.Select oItemToMove

ThisApplication.SilentOperation = True

Dim oCM As CommandManager
Set oCM = ThisApplication.CommandManager

&#39; Prepopulate the file name of new component in
&#39; the &#39;Create Component&#39; dialog
oCM.PostPrivateEvent kFileNameEvent, &quot;C:\temp\test\demoted.iam&quot;

Dim oControlDef As ControlDefinition
Set oControlDef = oCM.ControlDefinitions(&quot;AssemblyDemoteCmd&quot;)

&#39; Execute the demote command
oControlDef.Execute

ThisApplication.SilentOperation = False

End Sub</pre>
<p>&#0160;</p>
