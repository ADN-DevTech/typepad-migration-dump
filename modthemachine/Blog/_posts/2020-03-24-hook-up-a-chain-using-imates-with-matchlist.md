---
layout: "post"
title: "Hook up a chain using iMate's with MatchList"
date: "2020-03-24 11:33:13"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Assemblies"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2020/03/hook-up-a-chain-using-imates-with-matchlist.html "
typepad_basename: "hook-up-a-chain-using-imates-with-matchlist"
typepad_status: "Publish"
---

<p>I already had a blog post on <a href="https://adndevblog.typepad.com/manufacturing/2013/03/hook-up-a-chain-using-imates.html">Hook up a chain using iMate&#39;s</a>, but I was recently asked about more <a href="http://help.autodesk.com/view/INVNTOR/2020/ENU/?guid=GUID-00FD3BDE-8081-4A41-94A0-9B3E1F0B9004"><strong>iMate</strong></a> related <strong>API</strong> samples, so I thought I could also demonstrate the usage of <strong>iMate</strong> <a href="http://help.autodesk.com/view/INVNTOR/2020/ENU/?guid=GUID-D25FD747-FFCB-47A9-BEC7-B94DDA0C6AC1"><strong>MatchList</strong></a>.&#0160; <br />In theory, when using a <strong>MatchList</strong>, the <strong>iMates</strong> in the various components should be matched <strong>automatically</strong> when using the &quot;<strong data-stringify-type="bold">Automatically generate iMates on place</strong>&quot; option in the &quot;<strong>Place Component</strong>&quot; dialog, based on the names provided in the list. I did not realize that they <strong>over-match</strong> 😬<br />(i.e. if there are unresolved/unpaired <strong>iMates</strong> left then they will be matched even if there is no appropriate name in the <strong>MatchList</strong>)&#0160;<br /><a href="https://forums.autodesk.com/t5/inventor-ideas/match-list-for-imates/idi-p/4634729">Inventor Idea Station: Match list for imates</a></p>
<p>This is what I thought of doing as a test:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4f5402b200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="IMates1" border="0" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4f5402b200d image-full img-responsive" src="/assets/image_202002.jpg" title="IMates1" /></a></p>
<p>We have 4 parts based on 2 types of models: <strong>a</strong> and<strong> b</strong>. There will be four connection points as shown in the above image. So we&#39;ll have an <strong>a</strong> type part with connections for points <strong>1 and 2</strong> named <strong>chain_a_1-2.ipt</strong>, one <strong>b</strong> type part with connections for point <strong>2 and 3</strong> named <strong>chain_b_2-3.ipt</strong>, etc<br /><strong>iMates</strong> will be named following this pattern: &quot;&lt;iMate type [<strong>Insert</strong> or <strong>Mate</strong>]&gt;&lt;connection point number [<strong>1</strong>..<strong>4</strong>]&gt;&lt;part type [<strong>a</strong> or <strong>b</strong>]&gt;&quot;, e.g. <strong>Insert2b</strong><br />Each <strong>iMate</strong> will have a <strong>MatchList</strong> containing the same <strong>iMate</strong> type for the same connection point but for the opposite part type:</p>
<p><strong>Insert1a</strong> will be matching <strong>Insert1b</strong>, and <strong>Insert1b</strong> will be matching <strong>nothing</strong>, etc&#0160;</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a519ea76200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="IMates4b" border="0" class="asset  asset-image at-xid-6a00e553fcbfc688340240a519ea76200b image-full img-responsive" src="/assets/image_716850.jpg" title="IMates4b" /></a></p>
<p>Unfortunately, as I mentioned above, simply relying on the &quot;<strong data-stringify-type="bold">Automatically generate iMates on place</strong>&quot; option in the &quot;<strong>Place Component</strong>&quot; dialog will not work as it will also match <strong>Insert1a</strong> to <strong>Insert3b</strong> and <strong>Mate1a</strong> to <strong>Mate3b</strong> when adding the second part to the assembly.</p>
<p>Instead, we can just place all the components in the assembly at the same time <strong>without</strong> automatic <strong>iMate</strong> resolution and then use the <strong>API</strong> to match the <strong>iMates</strong>.&#0160;&#0160;</p>
<p>Here is the <strong>VBA</strong> code I used - if you want to use <strong>.NET</strong> / <strong>iLogic</strong> instead have a look at <a href="https://adndevblog.typepad.com/manufacturing/2015/11/convert-vba-to-net-ilogic.html">Convert VBA to .NET / iLogic</a></p>
<pre>Function GetAlliMates(cd As AssemblyComponentDefinition) As NameValueMap
  Dim res As NameValueMap
  Set res = ThisApplication.TransientObjects.CreateNameValueMap
  
  Dim occ As ComponentOccurrence
  For Each occ In cd.Occurrences
    Dim imate As iMateDefinition
    For Each imate In occ.iMateDefinitions
      Call res.Add(imate.name, imate)
    Next
  Next
  
  Set GetAlliMates = res
End Function

Function MatchiMate( _
cd As AssemblyComponentDefinition, _
imate As iMateDefinition, _
imates As NameValueMap, _
ByVal name As String) As Boolean
  On Error Resume Next

  Dim imateMatch As iMateDefinition
  Set imateMatch = imates.item(name)
  If Not imateMatch Is Nothing Then
    Call cd.iMateResults.AddByTwoiMates(imate, imateMatch)
    Debug.Print &quot;Matching &quot; + imate.name + &quot; with &quot; + imateMatch.name + &quot; // &quot; + Err.Description
  End If
  MatchiMate = (Err.Number = 0)
  
  On Error GoTo 0
End Function

Sub HookTheChain()
  Dim doc As AssemblyDocument
  Set doc = ThisApplication.ActiveDocument
    
  Dim cd As AssemblyComponentDefinition
  Set cd = doc.ComponentDefinition
    
  Dim imates As NameValueMap
  Set imates = GetAlliMates(cd)
    
  Dim imate As iMateDefinition
  For Each imate In imates
    Dim item As Variant
    For Each item In imate.MatchList
      If MatchiMate(cd, imate, imates, item) Then
        Exit For
      End If
    Next
  Next
End Sub</pre>
<p>When the code finishes, the parts will be on top of each other - I described the same thing in the <a href="https://adndevblog.typepad.com/manufacturing/2013/03/hook-up-a-chain-using-imates.html">other article</a>.&#0160;<br />Best thing is to <strong>ground</strong> one of the components and then you can move around the others so they come apart and end up like shown in the picture on the top.</p>
<p>The above code only works well if inside the assembly each <strong>iMate</strong> has a unique name and should only be matched with a specific other <strong>iMate</strong>.</p>
<p>Here is the <strong>Inventor 2020</strong> model I used: <span class="asset  asset-generic at-xid-6a00e553fcbfc688340240a519eae0200b img-responsive"><a href="https://modthemachine.typepad.com/files/chain_with_matchlist-1.zip">Download Chain_with_MatchList</a></span></p>
