---
layout: "post"
title: "iMates based shelving unit"
date: "2020-05-11 10:34:30"
author: "Adam Nagy"
categories:
  - "Adam"
  - "iLogic"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2020/05/imates-based-shelving-unit.html "
typepad_basename: "imates-based-shelving-unit"
typepad_status: "Publish"
---

<p>I wrote about using <strong>iMates</strong> a few times already, but I thought it could still be worth sharing this model as well that I&#39;m using for a <a href="https://forge.autodesk.com/en/docs/design-automation/v3/overview/">Design Automation API for Inventor</a> sample talked about <a href="https://forge.autodesk.com/blog/faster-configuration-results">here</a>.</p>
<p>I have <strong>90cm</strong> and <strong>180cm</strong> <strong>corner posts</strong> and <strong>mid-section posts</strong>, and <strong>90cm</strong> and <strong>120cm</strong> wide <strong>shelves</strong> to select from:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340263e8616d61200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="ShelvingParts" class="asset  asset-image at-xid-6a00e553fcbfc688340263e8616d61200d img-responsive" src="/assets/image_92562.jpg" title="ShelvingParts" /></a></p>
<p>I have the following <strong>iMates</strong> in the models:&#0160;&#0160;</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340263e8616e5f200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="IMates" class="asset  asset-image at-xid-6a00e553fcbfc688340263e8616e5f200d img-responsive" src="/assets/image_316142.jpg" title="IMates" /></a></p>
<p>The corner posts have an <strong>a</strong> and <strong>b</strong> <strong>iMate</strong> at each level named (a1, b1, a2, b2, etc), the mid-section posts have an additional <strong>c</strong> and <strong>d</strong> <strong>iMates</strong> as well at each level (<strong>c1</strong>, <strong>d1</strong>, etc)&#0160;&#0160;<br />The selves have <strong>a</strong> to <strong>d</strong> <strong>iMates</strong> on the two ends (<strong>1a</strong> - <strong>1d</strong> and <strong>2a</strong> - <strong>2d</strong>)<br />Most of the time the correct letters match up between the <strong>post</strong> and the <strong>shelf</strong>, apart from two corner posts where <strong>a</strong> is matched with <strong>c</strong> and <strong>b</strong> is matched with <strong>d</strong>.</p>
<p>All <strong>iMates</strong> use the same <strong>Constraint</strong> type: <strong>Insert</strong> &amp; <strong>Opposed</strong></p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340263e8623cfa200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="ShelfiMate" class="asset  asset-image at-xid-6a00e553fcbfc688340263e8623cfa200d img-responsive" src="/assets/image_371419.jpg" title="ShelfiMate" /></a></p>
<p>The 3 parameters driving the model are: <strong>Column</strong>, <strong>Height</strong> and <strong>ShelfWidth</strong><br />When the rule is run it has three main parts:<br />1) start<br />- add corner post and ground it<br />- add the shelves<br />- add the other corner post</p>
<p>2) middle (repeated depending on how many columns we have)<br />- add the two mid-section posts<br />- add the shelves</p>
<p>3) end<br />- add the two corner posts</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340263ec1835c7200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Shelving" class="asset  asset-image at-xid-6a00e553fcbfc688340263ec1835c7200c img-responsive" src="/assets/image_916276.jpg" title="Shelving" /></a></p>
<p>Instead of the main parameter changes kicking off the <strong>iLogic</strong> rule, I added an <strong>iTrigger</strong> parameter to do it.</p>
<pre>Sub Main()
  Trace.WriteLine(&quot;Main,  starting&quot;)
  trigger = iTrigger0	
	
  Dim height As String = Parameter(&quot;Height&quot;)  &#39;Height
  Dim width As String = Parameter(&quot;ShelfWidth&quot;) &#39; ShelfWidth 
  Dim columns As Integer = Parameter(&quot;Columns&quot;) &#39; Columns 
  Dim rows = 4 
  If height = &quot;90&quot; Then rows = 2
	  
  Trace.TraceInformation(&quot;Main, Values: height = {0}, width = {1}, columns = {2}, rows = {3}&quot;, height, width, columns, rows) 	  

  Dim asm As AssemblyDocument
  asm = ThisDoc.Document 
  
  Dim acd As AssemblyComponentDefinition
  acd = asm.ComponentDefinition
  
  Dim tg As TransientGeometry
  tg = ThisServer.TransientGeometry
  
  Dim path As String
  path = ThisDoc.Path + &quot;\&quot;
  Trace.WriteLine(&quot;Main, path = &quot; + path)

  &#39; START
  
  Dim post As ComponentOccurrence
  Dim bottomShelf As ComponentOccurrence
  
  &#39; add first corner (a,b)
  post = acd.Occurrences.Add(path + &quot;corner&quot; + height + &quot;.ipt&quot;, tg.CreateMatrix)
  post.Grounded = True
  
  &#39; add shelves (1a,1b)
  bottomShelf = AddShelves(post, path + &quot;shelf&quot; + width + &quot;.ipt&quot;, rows)
  
  &#39; add second corner (1c,1d) // enough to mate the first level
  Call AddPost(bottomShelf, path + &quot;corner&quot; + height + &quot;.ipt&quot;, &quot;1&quot;, &quot;c&quot;, &quot;a&quot;, &quot;d&quot;, &quot;b&quot;)
  
  &#39; MIDDLE
  Dim Column As Integer
  For Column = 2 To columns
    &#39; add first mid (2a,2b) // enough to mate the first level
    Call AddPost(bottomShelf, path + &quot;mid&quot; + height + &quot;.ipt&quot;, &quot;2&quot;, &quot;a&quot;, &quot;a&quot;, &quot;b&quot;, &quot;b&quot;)
  
    &#39; add second mid (2c,2d) // enough to mate the first level
    post = AddPost(bottomShelf, path + &quot;mid&quot; + height + &quot;.ipt&quot;, &quot;2&quot;, &quot;c&quot;, &quot;c&quot;, &quot;d&quot;, &quot;d&quot;)
  
    &#39; add shelves ()
    bottomShelf = AddShelves(post, path + &quot;shelf&quot; + width + &quot;.ipt&quot;, rows)
    
    &#39; repeat
  Next
  
  &#39; END
  
  &#39; add third corner (2a,2b) // enough to mate the first level
  Call AddPost(bottomShelf, path + &quot;corner&quot; + height + &quot;.ipt&quot;, &quot;2&quot;, &quot;a&quot;, &quot;a&quot;, &quot;b&quot;, &quot;b&quot;)
  
  &#39; add fourth corner (2c,2d) // enough to mate the first level
  Call AddPost(bottomShelf, path + &quot;corner&quot; + height + &quot;.ipt&quot;, &quot;2&quot;, &quot;c&quot;, &quot;a&quot;, &quot;d&quot;, &quot;b&quot;)
End Sub

Function GetimateDefinition(defs As iMateDefinitionsEnumerator, name As String) As iMateDefinition
  Dim def As iMateDefinition
  For Each def In defs
    If def.Name = name Then
      GetimateDefinition = def
      Exit For
    End If
  Next
End Function

Function AddShelves(post As ComponentOccurrence, fullPath As String, rows As Integer) As ComponentOccurrence
  Trace.WriteLine(&quot;AddShelves, fullPath = &quot; + fullPath)
  
  Dim tg As TransientGeometry
  tg = ThisServer.TransientGeometry
  
  Dim acd As AssemblyComponentDefinition
  acd = post.Parent
  
  Dim occs As ComponentOccurrences
  occs = acd.Occurrences
  
  Dim postMate As iMateDefinition
  Dim shelfMate As iMateDefinition
  Dim shelf As ComponentOccurrence
  Dim i As Integer
  For i = 1 To rows
    shelf = occs.Add(fullPath, tg.CreateMatrix)
    If AddShelves Is Nothing Then
      AddShelves = shelf
    End If
    
    postMate = GetimateDefinition(post.iMateDefinitions, &quot;a&quot; + CStr(i))
    shelfMate = GetimateDefinition(shelf.iMateDefinitions, &quot;1a&quot;)
    Call acd.iMateResults.AddByTwoiMates(postMate, shelfMate)
    
    postMate = GetimateDefinition(post.iMateDefinitions, &quot;b&quot; + CStr(i))
    shelfMate = GetimateDefinition(shelf.iMateDefinitions, &quot;1b&quot;)
    Call acd.iMateResults.AddByTwoiMates(postMate, shelfMate)
  Next
End Function

Function AddPost(shelf As ComponentOccurrence, fullPath As String, shelfSide As String, shelfMate1 As String, postMate1 As String, shelfMate2 As String, postMate2 As String) As ComponentOccurrence
  Trace.WriteLine(&quot;AddPost, fullPath = &quot; + fullPath)
  Dim tg As TransientGeometry
  tg = ThisServer.TransientGeometry
  
  Dim acd As AssemblyComponentDefinition
  acd = shelf.Parent
  
  Dim occs As ComponentOccurrences
  occs = acd.Occurrences
  
  Dim postMate As iMateDefinition
  Dim shelfMate As iMateDefinition
  AddPost = occs.Add(fullPath, tg.CreateMatrix)
  
  postMate = GetimateDefinition(AddPost.iMateDefinitions, postMate1 + &quot;1&quot;)
  shelfMate = GetimateDefinition(shelf.iMateDefinitions, shelfSide + shelfMate1)
  Call acd.iMateResults.AddByTwoiMates(postMate, shelfMate)
  
  postMate = GetimateDefinition(AddPost.iMateDefinitions, postMate2 + &quot;1&quot;)
  shelfMate = GetimateDefinition(shelf.iMateDefinitions, shelfSide + shelfMate2)
  Call acd.iMateResults.AddByTwoiMates(postMate, shelfMate)
End Function</pre>
<p>The model can be found here: <a href="https://github.com/adamenagy/QuickerAssemblerDA/tree/master/forgesample/wwwroot/files">https://github.com/adamenagy/QuickerAssemblerDA/tree/master/forgesample/wwwroot/files</a></p>
<p>-Adam</p>
