---
layout: "post"
title: "Keep selection between rule executions "
date: "2016-01-25 05:01:07"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/01/keep-selection-between-rule-executions.html "
typepad_basename: "keep-selection-between-rule-executions"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>There is a <a href="http://adndevblog.typepad.com/manufacturing/2013/11/do-selection-from-ilogic.html">blog post</a> on selecting multiple components interactively from an <strong>iLogic</strong> <strong>Rule</strong>, but it does not highlight the selected objects while the <strong>Rule</strong> is running in the <strong>Inventor</strong> versions I tested it in - something might have changed in the meantime. However, using a&#0160;<strong>HighlightSet</strong>&#0160;seems to work fine:</p>
<pre>Dim oSet As HighlightSet

&#39; Check to make sure the active document is a part.
If ThisApplication.ActiveDocumentType &lt;&gt; 
DocumentTypeEnum.kPartDocumentObject Then	
  MsgBox(&quot;A part document must be open.&quot;)
  Exit Sub
End If

&#39; Set a reference to the active part document.
Dim oDoc As PartDocument
oDoc = ThisApplication.ActiveDocument

oSet = oDoc.CreateHighlightSet

While True
  Dim oFace As Object
    oFace = ThisApplication.CommandManager.Pick(
      SelectionFilterEnum.kPartFaceFilter, 
      &quot;Select a face&quot;) 
	
    &#39; If nothing gets selected then we&#39;re done	
    If IsNothing(oFace) Then Exit While
	
    oSet.AddItem(oFace)
End While

oSet.Clear()</pre>
<p>When you run an <strong>iLogic</strong> <strong>Rule</strong> it behaves as starting a command, which also means that the current selection will be cancelled. So we would need to store which entities were already selected inside our <strong>Rule</strong> and reselect them. In order to keep a list we would need to have a <strong>global static</strong> variable that could hold on to those entities. Even if we just kept them in a <strong>HighlightSet</strong>&#0160;we would still need a <strong>global static</strong> object&#0160;to keep a reference to the <strong>HighlightSet</strong>, because as soon as that goes out of scope it will be deleted and then highlighting will be gone.&#0160;</p>
<p>In case of <strong>VB.NET</strong> the way to create a <strong>global</strong>&#0160;<strong>static</strong> variable is to use the <strong>Shared</strong> keyword. It is possible to add <strong>global variables</strong> to an <strong>iLogic</strong> <strong>Rule</strong>&#0160;by implementing the whole <strong>Rule</strong> class as shown in this article:<br /><a href="https://www.cadlinecommunity.co.uk/hc/en-us/articles/203491091-Inventor-2016-iLogic-Using-Global-Variables">https://www.cadlinecommunity.co.uk/hc/en-us/articles/203491091-Inventor-2016-iLogic-Using-Global-Variables</a></p>
<p>Actually, because you can define the whole <strong>Rule</strong> class, you can also handle events in it if you want. That means you could even use the <strong>InteractionEvents</strong> class which enables you to do multi and window selections. However programmatically selecting entities inside a rule using <strong>SelectSet.Select</strong> or <strong>CommandManager.DoSelect</strong> does not seem to work well: the entities only get highlighted once the rule finished.</p>
<p>If we stick to&#0160;<strong>HighlightSet</strong> then we could use it like this to do selection and re-selection of previously selected entities:</p>
<pre>Class ThisRule

  &#39; Keep track of selected entities
  Shared oSelectedEnts As ObjectCollection  

  Sub Main()
    Dim oSet As HighlightSet

    &#39; Check to make sure the active document is a part.
    If ThisApplication.ActiveDocumentType &lt;&gt; 
    DocumentTypeEnum.kPartDocumentObject Then	
      MsgBox(&quot;A part document must be open.&quot;)
      Exit Sub
    End If
	
    &#39; Initialize the entity collection
    &#39; If you want to keep track of the previously 
    &#39; selected entitites then only initialize this variable
    &#39; if it has not been initialized before
    If oSelectedEnts Is Nothing Then
      oSelectedEnts = ThisApplication.TransientObjects.CreateObjectCollection()
    End If  

    oDoc = ThisDoc.Document
    oSet = oDoc.CreateHighlightSet

    &#39; Show the previously selected entities
    For Each ent In oSelectedEnts
      oSet.AddItem(ent)
    Next
	
    While True
      Dim oFace As Object
      oFace = ThisApplication.CommandManager.Pick(
        SelectionFilterEnum.kPartFaceFilter, 
        &quot;Select a face&quot;) 
		
      &#39; If nothing gets selected then we&#39;re done	
      If IsNothing(oFace) Then Exit While
		
      oSet.AddItem(oFace)
      oSelectedEnts.Add(oFace)
    End While
	
    oSet.Clear()
  End Sub
End Class</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c80c4891970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="HighlightSet" class="asset  asset-image at-xid-6a0167607c2431970b01b7c80c4891970b img-responsive" src="/assets/image_4f0bb6.jpg" title="HighlightSet" /></a></p>
<p>&#0160;</p>
