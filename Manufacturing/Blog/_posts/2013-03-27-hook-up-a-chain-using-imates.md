---
layout: "post"
title: "Hook up a chain using iMate's"
date: "2013-03-27 09:38:10"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/03/hook-up-a-chain-using-imates.html "
typepad_basename: "hook-up-a-chain-using-imates"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>When constraining parts together in an assembly, if you organized things well then with a bit of programming you can automate things nicely. </p>
<p>You could use iMate&#39;s to specify how the parts should be contrained to each other - this would be just a one off thing so could be done in the user interface as well. Then you could programmatically&#0160;insert multiple instances of them (same number of each) and use the iMate&#39;s to join them.</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee9c88a09970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Chains" class="asset  asset-image at-xid-6a0167607c2431970b017ee9c88a09970d" src="/assets/image_317dbe.jpg" style="width: 450px;" title="Chains" /></a></p>
<p>In this case we have two parts of a chain assembly. Each have the same iMate&#39;s: iMate:1 and iInsert:1 on one end of the part, and iMate:2 and iInsert:2 on the other end of the part. We could pair them like this:</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d425113ec970c-pi" style="display: inline;"><img alt="Chain" border="0" class="asset  asset-image at-xid-6a0167607c2431970b017d425113ec970c image-full" src="/assets/image_422baf.jpg" title="Chain" /></a></p>
<p>This code assumes that there are the same number of occurrences of each part in the assembly already and none of the iMate&#39;s have been used:</p>
<pre>Function GetiMate(name As String, occ As ComponentOccurrence) _
As iMateDefinition
    Dim imate As iMateDefinition
    For Each imate In occ.iMateDefinitions
        If imate.name = name Then
            Set GetiMate = imate
            Exit Function
        End If
    Next
End Function

&#39; This hooks &quot;chain1&quot; parts to &quot;chain2&quot; parts
&#39; In this assembly we only have these two parts
&#39; so checking if the part documents are not the
&#39; same is enough
Sub HookTheChain()
    Dim doc As AssemblyDocument
    Set doc = ThisApplication.ActiveDocument
    
    Dim cd As AssemblyComponentDefinition
    Set cd = doc.ComponentDefinition
    
    &#39; Here we only have two referenced files
    Dim desc1 As DocumentDescriptor
    Set desc1 = doc.ReferencedDocumentDescriptors(1)
    Dim occs1 As ComponentOccurrencesEnumerator
    Set occs1 = cd.Occurrences.AllReferencedOccurrences(desc1)
    
    Dim desc2 As DocumentDescriptor
    Set desc2 = doc.ReferencedDocumentDescriptors(2)
    Dim occs2 As ComponentOccurrencesEnumerator
    Set occs2 = cd.Occurrences.AllReferencedOccurrences(desc2)
    
    Dim i As Integer
    For i = 1 To occs1.Count
        &#39; Set occ1 iMate:1/iInsert:1 to
        &#39; occ2 iMate:1/iInsert:1
        Dim imate1 As iMateDefinition
        Set imate1 = GetiMate(&quot;iMate:1&quot;, occs1(i))
        Dim imate2 As iMateDefinition
        Set imate2 = GetiMate(&quot;iMate:1&quot;, occs2(i))
        
        Call cd.iMateResults.AddByTwoiMates(imate1, imate2)
        
        Set imate1 = GetiMate(&quot;iInsert:1&quot;, occs1(i))
        Set imate2 = GetiMate(&quot;iInsert:1&quot;, occs2(i))
        
        Call cd.iMateResults.AddByTwoiMates(imate1, imate2)
        
        &#39; Set occ1 iMate:2/iInsert:2 to
        &#39; another occ2 iMate:2/iInsert:2
        Dim ip1 As Integer
        ip1 = i Mod occs2.Count + 1
        
        Set imate1 = GetiMate(&quot;iMate:2&quot;, occs1(i))
        Set imate2 = GetiMate(&quot;iMate:2&quot;, occs2(ip1))
        
        Call cd.iMateResults.AddByTwoiMates(imate1, imate2)
        
        Set imate1 = GetiMate(&quot;iInsert:2&quot;, occs1(i))
        Set imate2 = GetiMate(&quot;iInsert:2&quot;, occs2(ip1))
        
        Call cd.iMateResults.AddByTwoiMates(imate1, imate2)
    Next
End Sub</pre>
<p>Once you run the code the parts might end up on top of each other, but when you drag them using the mouse then you&#39;ll see that they can be moved and are hooked up as intended:</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee9c50685970d-pi" style="display: inline;"><img alt="Chain2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b017ee9c50685970d image-full" src="/assets/image_50d2f5.jpg" title="Chain2" /></a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee9c50685970d-pi" style="display: inline;"></a>Parts and assembly files are attached in the zip - 
<span class="asset  asset-generic at-xid-6a0167607c2431970b017c3821f7f0970b"><a href="http://adndevblog.typepad.com/files/chain.zip">Download Chain</a></span>. You can just open up the assembly and run the above code. &#0160;</p>
