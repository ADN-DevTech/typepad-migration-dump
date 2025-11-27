---
layout: "post"
title: "Batch remove FEA links"
date: "2017-12-19 19:44:57"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2017/12/batch-remove-fea-links.html "
typepad_basename: "batch-remove-fea-links"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>In the same time when I tested the code in the <a href="http://adndevblog.typepad.com/manufacturing/2017/12/remove-ole-links-using-referencedolefiledescriptor.html">other blog</a>, my colleague <a href="https://www.linkedin.com/in/henry-huang-40795624/">Henry Huang</a> in product support team shared a code that can batch delete FEA OLE links. The code is provided by the engineer <a href="https://www.linkedin.com/in/paul-yongting-zeng-61212079/">Paul Zeng</a>. The code is well written. I believe it will be help many other customers.&#0160;</p>
<p>Thank you Paul, Henry!</p>
<pre><code>
Sub DeleteFEALinksForFolder()
  
    &#39;add search paths
    Dim DicList
    Set DicList = CreateObject(&quot;Scripting.Dictionary&quot;)
    
    Const SearchPath = &quot;C:\Users\zengp\Desktop\Inv_11229\&quot; &#39;add more paths if you have
    DicList.Add SearchPath, &quot;&quot;
      
    &#39;collect all folders
    Dim FileList, I
    Set FileList = CreateObject(&quot;Scripting.Dictionary&quot;)
    
    I = 0
    Do While I &lt; DicList.Count
        Key = DicList.keys
        NowDic = Dir(Key(I), vbDirectory) &#39;start search folder
        Do While NowDic &lt;&gt; &quot;&quot;
            If (NowDic &lt;&gt; &quot;.&quot;) And (NowDic &lt;&gt; &quot;..&quot;) And (NowDic &lt;&gt; &quot;OldVersions&quot;) Then
                If (GetAttr(Key(I) &amp; NowDic) And vbDirectory) = vbDirectory Then &#39;find child folder
                    DicList.Add Key(I) &amp; NowDic &amp; &quot;\&quot;, &quot;&quot;
                End If
            End If
            NowDic = Dir() &#39;continue searching
        Loop
        I = I + 1
    Loop
    
    &#39;collect all part/assembly files
    For Each Key In DicList.keys
       NowFile = Dir(Key &amp; &quot;*.ipt&quot;)
       Do While NowFile &lt;&gt; &quot;&quot;
            FileList.Add Key &amp; NowFile, &quot;&quot; &#39;Add(Key,Item)  FileList.Key=file name,FileList.Item=file path
            NowFile = Dir()
       Loop
    Next
    
    For Each Key In DicList.keys
       NowFile = Dir(Key &amp; &quot;*.iam&quot;)
       Do While NowFile &lt;&gt; &quot;&quot;
            FileList.Add Key &amp; NowFile, &quot;&quot; &#39;Add(Key,Item)  FileList.Key=file name,FileList.Item=file path
            NowFile = Dir()
       Loop
    Next

    &#39;delete FEA file links
    ThisApplication.SilentOperation = True
    
    For Each strFileName In FileList.keys
        DeleteFEALinksForSingleFile (strFileName)
    Next

    ThisApplication.SilentOperation = False
    
End Sub

Sub DeleteFEALinksForSingleFile(strFullFileName As String)

    Dim oDoc As Document
    Set oDoc = ThisApplication.Documents.Open(strFullFileName)
    
    Dim bHasFEALinksDeleted As Boolean
    bHasFEALinksDeleted = DeleteFEALinks(oDoc)
    If bHasFEALinksDeleted Then
        Call oDoc.Save
        Debug.Print strFullFileName
    End If
    
    Call oDoc.Close
End Sub


Function DeleteFEALinks(oDoc As Document) As Boolean &#39;if delete links
    DeleteFEALinks = False
    
    Dim oReferencedOLEFileDescriptors As ReferencedOLEFileDescriptors
    Set oReferencedOLEFileDescriptors = oDoc.ReferencedOLEFileDescriptors
    
    Dim oOLEFileDescriptor As ReferencedOLEFileDescriptor
    For Each oOLEFileDescriptor In oReferencedOLEFileDescriptors
        Dim strFileName As String
        strFileName = oOLEFileDescriptor.FullFileName
              
        If (IsFEAOLELinkFile(strFileName)) Then
          &#39;Debug.Print &quot;Break FEA OLE file link: &quot; + strFileName
          DeleteFEALinks = True
          Call oOLEFileDescriptor.Delete
        End If
    Next
    
End Function

Function IsFEAOLELinkFile(strFileName As String) As Boolean
    IsFEAOLELinkFile = False

    Dim FEAFileExtArr() As Variant
    FEAFileExtArr = Array(&quot;.fins&quot;, &quot;.fsat&quot;, &quot;.ftes&quot;, &quot;.fwiz&quot;, &quot;.fmsh&quot;, &quot;.fres&quot;)
    
    For Each strFileExt In FEAFileExtArr
      If (InStr(strFileName, strFileExt) &gt; 0) Then
        IsFEAOLELinkFile = True
        Exit For
      End If
    Next

End Function
</code></pre>
